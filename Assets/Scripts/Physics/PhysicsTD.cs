using UnityEngine;
using System.Collections.Generic;

public enum Direction { LEFT, UP, RIGHT, DOWN }

[RequireComponent (typeof (BoxCollider2D))]
public class PhysicsTD : MonoBehaviour
{
	private const float VELOCITY_MINIMUM_THRESHOLD = 0.3f;
		
	public Direction Dir;
	public LayerMask CollisionMask;

	// Used to decrease / scale velocity vector
	public float DecelerationRatio = 1.0f;
	public float Y_SpeedRatio = 0.5f;

	// Collision detection to adjust velocity vector 
	private BoxCollider2D m_boxCollider;
	private RaycastOrigins m_raycastOrigins;
	private CollisionInfo m_collisions;
	private const float m_skinWidth = .015f;
	private int m_horizontalRayCount = 4;
	private int m_verticalRayCount = 4;
	private float m_horizontalRaySpacing;
	private float m_verticalRaySpacing;

	// Tracking movement
	private List<ForceTD> m_forces;
	private Vector2 m_accumulatedVelocity = Vector2.zero;
	private bool m_canMove = true;
	public bool CanMove { get { return m_canMove; } set { m_canMove = value; } }
	private Vector2 m_velocity;
	public Vector2 Velocity { get { return m_velocity; } }

	// Tracking inputed movement
	private ForceTD m_inputedForce;
	private Vector2 m_inputedMove = Vector2.zero;
	public Vector2 InputedMove { get { return m_inputedMove; } }

	// Tracking spawn?
	private Vector2 m_spawnPos;
	private bool m_resetPos = false;

	// Tracking m_sprite orientation (flipping if left)...
	SpriteRenderer m_sprite;

	internal void Awake()
	{
		m_forces = new List<ForceTD>();
		m_inputedForce = new ForceTD();
		m_boxCollider = GetComponent<BoxCollider2D>();
		m_boxCollider.offset = new Vector2(m_boxCollider.offset.x, m_boxCollider.offset.y + m_skinWidth);
		m_sprite = GetComponent<SpriteRenderer>();
		m_canMove = true;
	}

	internal void Start()
	{
		CalculateRaySpacing();
		SetDirection(Dir);
		OnSpawn();
	}

	private void OnSpawn()
	{
		if (m_resetPos)
		{
			transform.position = new Vector3 (m_spawnPos.x, m_spawnPos.y, transform.position.z);
			m_accumulatedVelocity = Vector2.zero;
			m_resetPos = false;
		}
	}

	public void SetSpawnPos(Vector2 sp)
	{
		m_spawnPos = sp;
		m_resetPos = true;
	}

	internal void FixedUpdate()
	{
		DecelerateAutomatically(VELOCITY_MINIMUM_THRESHOLD);
		ProcessMovement();
	}

	private void DecelerateAutomatically(float threshold)
	{
		if (m_accumulatedVelocity.sqrMagnitude > threshold)
			m_accumulatedVelocity *= (1.0f - Time.fixedDeltaTime * DecelerationRatio * 3.0f);
		else
			m_accumulatedVelocity = Vector2.zero;
	}

	private void CheckCanMove()
	{
		if (m_canMove)
			return;
		m_inputedMove = Vector2.zero;
		m_inputedForce.Force = new Vector2(0, 0);
	}

	private void ApplyForcesToVelocity()
	{
		m_inputedForce.Force *= Time.fixedDeltaTime;
		m_velocity = m_inputedForce.Force;

		List<ForceTD> forcesToRemove = new List<ForceTD>();
		foreach (ForceTD force in m_forces)
		{
			m_velocity += force.Force * Time.fixedDeltaTime;
			force.Duration -= Time.fixedDeltaTime;
			if (force.Duration < 0f)
				forcesToRemove.Add(force);
		}

		foreach (ForceTD force in forcesToRemove)
			m_forces.Remove(force);

		m_velocity += m_accumulatedVelocity * Time.fixedDeltaTime;
	}

	private void ProcessMovement()
	{
		CheckCanMove();
		ApplyForcesToVelocity();
		UpdateRaycastOrigins();
		m_collisions.Reset();

		if (m_velocity.x != 0 || m_inputedMove.x != 0)
			HorizontalCollisions(ref m_velocity);
		if (m_velocity.y != 0 || m_inputedMove.y != 0)
			VerticalCollisions(ref m_velocity);
		
		m_velocity.y *= Y_SpeedRatio;
		transform.Translate (m_velocity);
	}

	public void AddToVelocity(Vector2 veloc)
	{
		m_accumulatedVelocity += veloc;
	}

	public void AddSelfForce(Vector2 force, float duration)
	{
		m_forces.Add(new ForceTD(force, duration));
	}

	public bool IsAttemptingMovement()
	{
		return m_inputedMove.magnitude > 0;
	}

	public void Move(Vector2 veloc, Vector2 input)
	{
		m_inputedMove = input;
		m_inputedForce.Force = veloc;
	}

	private bool ValidCollision(RaycastHit2D hit)
	{
		return hit && !hit.collider.isTrigger && hit.collider.gameObject != gameObject;
	}

	private void HorizontalCollisions(ref Vector2 velocity)
	{
		float directionX = Mathf.Sign(velocity.x == 0 ? m_inputedMove.x : m_velocity.x);
		float rayLength = Mathf.Max(0.05f, Mathf.Abs(velocity.x) + m_skinWidth);

		for (int i = 0; i < m_horizontalRayCount; ++i)
		{
			Vector2 rayOrigin = (directionX == -1) ? m_raycastOrigins.bottomLeft : m_raycastOrigins.bottomRight;
			float raySpacing = m_horizontalRaySpacing;
			if (i < m_horizontalRayCount - 1)
				raySpacing *= 0.5f;

			rayOrigin += Vector2.up * raySpacing * i;

			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, CollisionMask);
			//Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, hit ? Color.red : Color.green);
			if (ValidCollision(hit))
			{
				velocity.x = (hit.distance - m_skinWidth) * directionX;
				rayLength = hit.distance;
			}
		}

	}

	private void VerticalCollisions(ref Vector2 velocity)
	{
		// Shouldn't it be velocity.y used in the conditional? TODO: figure that out.
		float directionY = Mathf.Sign(velocity.x == 0 ? m_inputedMove.y : m_velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + m_skinWidth;

		for (int i = 0; i < m_verticalRayCount; ++i)
		{
			Vector2 rayOrigin = (directionY == -1) ? m_raycastOrigins.bottomLeft : m_raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (m_verticalRaySpacing * i + velocity.x);

			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, CollisionMask);
			if (ValidCollision(hit))
			{
				velocity.y = (hit.distance - m_skinWidth) * directionY;
				rayLength = hit.distance;
				m_collisions.below = directionY == -1;
				m_collisions.above = directionY == 1;
			}
		}
	}

	private void UpdateRaycastOrigins()
	{
		Bounds bounds = m_boxCollider.bounds;
		bounds.Expand(m_skinWidth);
		m_raycastOrigins.bottomLeft = new Vector2 (bounds.min.x , bounds.min.y);
		m_raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		m_raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		m_raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}

	private void CalculateRaySpacing()
	{
		Bounds bounds = m_boxCollider.bounds;
		bounds.Expand(m_skinWidth);
		m_horizontalRayCount = Mathf.Clamp (m_horizontalRayCount, 2, int.MaxValue);
		m_verticalRayCount = Mathf.Clamp (m_verticalRayCount, 2, int.MaxValue);
		m_horizontalRaySpacing = bounds.size.y / (m_horizontalRayCount - 1);
		m_verticalRaySpacing = bounds.size.x / (m_verticalRayCount - 1);
	}

	public void SetDirection(Direction m_dir)
	{
		Dir = m_dir;
		if (m_sprite)
			m_sprite.flipX = Dir == Direction.LEFT;
	}

	public Vector2 OrientVectorToDirection(Vector2 v)
	{
		if (Dir == Direction.RIGHT)
			return v;
		else if (Dir == Direction.LEFT)
			return new Vector2(-v.x, v.y);
		else if (Dir == Direction.UP)
			return new Vector2(v.y, v.x);
		else if (Dir == Direction.DOWN)
			return new Vector2(v.y, -v.x);
		return new Vector2();
	}

	public Vector2 OrientScaleToDirection(Vector2 s)
	{
		if (Dir == Direction.RIGHT || Dir == Direction.LEFT)
			return s;
		else if (Dir == Direction.UP || Dir == Direction.DOWN)
			return new Vector2(s.y, s.x);
		return new Vector2();
	}
}