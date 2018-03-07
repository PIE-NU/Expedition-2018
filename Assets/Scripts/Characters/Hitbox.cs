using UnityEngine;
using System.Collections.Generic;

public class Hitbox : MonoBehaviour {

	[SerializeField]
	private float m_damage = 10.0f;
	public float Damage { get { return m_damage; } set { m_damage = value; } }

	[SerializeField]
	private float m_duration = 1.0f;
	public float Duration { get { return m_duration; } set { m_duration = value; } }

	[SerializeField]
	private bool m_hasDuration = true;

	[SerializeField]
	private bool m_isFixedKnockback = false;
	public bool IsFixedKnockback { get { return m_isFixedKnockback; } set { m_isFixedKnockback = value; } }

	[SerializeField]
	private Vector2 m_knockback = new Vector2(0.0f,40.0f);
	public Vector2 Knockback { get { return m_knockback; } set { m_knockback = value; } }

	[SerializeField]
	private float m_stun = 0.0f;
	public float Stun { get { return m_stun; } set { m_stun = value; } }

	[SerializeField]
	private bool m_isRandomKnockback = false;
	public bool IsRandomKnockback { get { return m_isRandomKnockback; } set { m_isRandomKnockback = value; } }

	private List<string> m_hitTypes;
	public List<string> HitTypes { get { return m_hitTypes; } set { m_hitTypes = value; } }

	[HideInInspector]
	public GameObject Creator { get; set; }

	[SerializeField]
	private GameObject m_followObj;

	[SerializeField]
	private Vector2 m_followOffset;

	[SerializeField]
	private List<Collider2D> m_upRightDownLeftColliders;

	private PhysicsTD m_creatorPhysics;
	private Vector4 m_knockbackRanges;
	private List<Attackable> m_collidedObjs = new List<Attackable> ();

	virtual protected void Awake() {
		if (m_isRandomKnockback)
			RandomizeKnockback ();
		m_hasDuration = m_duration > 0;
	}

	virtual protected void Start() {
		m_creatorPhysics = Creator.GetComponent<PhysicsTD> ();
	}

	virtual protected void Update() {
		SwitchActiveCollider(m_creatorPhysics.Dir);
		if (m_hasDuration)
			MaintainOrDestroyHitbox();
		if (m_followObj != null)
			transform.position = new Vector3(m_followObj.transform.position.x + m_followOffset.x, m_followObj.transform.position.y + m_followOffset.y,0);
	}

	void MaintainOrDestroyHitbox() {
		if (m_duration <= 0.0f)
			GameObject.Destroy (gameObject);
		Duration -= Time.deltaTime;
	}

	public void SetScale(Vector2 scale) {
		transform.localScale = scale;
	}

	public void SetFollow(GameObject obj, Vector2 offset) {
		m_followObj = obj;
		m_followOffset = offset;
	}

	void RandomizeKnockback () {
		m_knockback.x = Random.Range (m_knockbackRanges.x, m_knockbackRanges.y);
		m_knockback.y = Random.Range (m_knockbackRanges.z, m_knockbackRanges.w);
	}

	public void SetKnockbackRanges (float minX, float maxX,float minY, float maxY) {
		IsRandomKnockback = true;
		IsFixedKnockback = true;
		m_knockbackRanges = new Vector4 (minX, maxX, minY, maxY);
	}

	protected void OnAttackable(Attackable atkObj) {
		if (!atkObj || atkObj.gameObject == Creator || m_collidedObjs.Contains (atkObj))
			return;
		if (IsRandomKnockback)
			RandomizeKnockback();
		atkObj.takeHit (this);
		m_collidedObjs.Add (atkObj);
	}

	internal void OnTriggerEnter2D(Collider2D other) {
		OnAttackable (other.gameObject.GetComponent<Attackable> ());
	}

	internal void OnTriggerExit2D(Collider2D other) {
		/*
		 * TODO: Delay removal of collided object to avoid stuttered collisions 
		 */
		/*
		if (other.gameObject.GetComponent<Attackable> () && collidedObjs.Contains(other.gameObject.GetComponent<Attackable>())) {
			collidedObjs.Remove (other.gameObject.GetComponent<Attackable> ());
		}
		*/
	}

	void SwitchActiveCollider(Direction dir) {
		if (m_upRightDownLeftColliders.Count == 0)
			return;
		var dirIndex = ConvertDirToUpRightDownLeftIndex(dir);
		for (var i = 0; i < 4; i++) {
			m_upRightDownLeftColliders[i].enabled = i == dirIndex;
		}
	}

	int ConvertDirToUpRightDownLeftIndex(Direction dir) {
		if (dir == Direction.UP)
			return 0;
		else if (dir == Direction.RIGHT)
			return 1;
		else if (dir == Direction.DOWN)
			return 2;
		return 3;
	}
}