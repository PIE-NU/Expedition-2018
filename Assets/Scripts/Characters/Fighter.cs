﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (HitboxMaker))]
[RequireComponent (typeof (PhysicsTD))]
public class Fighter : MonoBehaviour
{
	[HideInInspector]
	public Dictionary<string,AttackInfo> Attacks = new Dictionary<string,AttackInfo>();

	public bool AutoOrientSprite = true;
	public string IdleAnimation = "idle";
	public string WalkAnimation = "walk";
	public string HurtAnimation = "hit";

	private PhysicsTD m_physics;
	private AnimatorSprite m_anim;
	private Attackable m_attackable;
	private AttackInfo m_currentAttack = null;

	private float m_animationSpeed = 2f;

	[HideInInspector]
	public AudioClip AttackSound;

	[HideInInspector]
	public float StunTime = 0.0f;

	internal void Awake()
	{
		m_anim = GetComponent<AnimatorSprite>();
		m_physics = GetComponent<PhysicsTD>();
		m_attackable = GetComponent<Attackable>();
	}

	internal void Start()
	{
		AttackInfo[] at = GetComponents<AttackInfo>();
		foreach (AttackInfo a in at)
		{
			if (!Attacks.ContainsKey(a.AttackName))
			{
				Attacks.Add(a.AttackName, a);
				a.AddListener(OnAttackProgressed);
			}
		}
	}

	internal void Update()
	{
		if (ProgressStun())
			return;
		if (ActivateStunIfDead())
			return;
		if (ProgressAttack())
			return;
		ProgressWalkOrIdleAnimation();
	}

	private bool ProgressStun()
	{
		if (StunTime <= 0.0f)
			return false;
		m_anim.Play(HurtAnimation, AutoOrientSprite);
		StunTime -= Time.deltaTime;
		if (StunTime <= 0.0f && m_attackable.Alive)
			EndStun();
		return true;
	}

	private bool ActivateStunIfDead()
	{
		if (m_attackable.Alive)
			return false;
		StartHitState(3.0f);
		return true;
	}

	private bool ProgressAttack()
	{
		if (m_currentAttack == default(AttackInfo))
			return false;
		m_currentAttack.Progress();
		return true;
	}

	public void OnAttackProgressed(AttackState state)
	{
		switch (state)
		{
			case AttackState.STARTUP:
				OnAttackStart();
				break;
			case AttackState.RECOVERY:
				OnAttackRecover();
				break;
			case AttackState.INACTIVE:
				OnAttackEnd();
				break;
		}
	}

	private void OnAttackStart()
	{
		if (m_currentAttack.StartupSoundFX != null)
			AudioSource.PlayClipAtPoint (m_currentAttack.StartupSoundFX, transform.position);
		if (m_currentAttack.AttackSoundFX != null)
			AudioSource.PlayClipAtPoint(m_currentAttack.AttackSoundFX, transform.position);
		if (AttackSound != null)
			AudioSource.PlayClipAtPoint(AttackSound, gameObject.transform.position);
		if (m_currentAttack.AttackFX)
			AddEffect(m_currentAttack.AttackFX, m_currentAttack.RecoveryTime + 0.2f);

		m_anim.Play(m_currentAttack.StartUpAnimation, true);
		m_anim.SetSpeed(m_currentAttack.AnimSpeed * m_animationSpeed);
	}

	private void OnAttackRecover()
	{
		m_anim.Play(m_currentAttack.RecoveryAnimation, true);
	}

	private void OnAttackEnd()
	{
		m_physics.CanMove = true;
		m_currentAttack = null;
		m_anim.SetSpeed(1.0f);
	}

	private void ProgressWalkOrIdleAnimation()
	{
		if (m_physics.IsAttemptingMovement())
			m_anim.Play(WalkAnimation, AutoOrientSprite);
		else
			m_anim.Play(IdleAnimation, AutoOrientSprite);
	}

	void AddEffect(GameObject attackFX, float lifeTime)
	{
		/*
		var fx = GameObject.Instantiate(attackFX, transform);
		fx.GetComponent<disappearing>().duration = m_currentAttack.RecoveryTime;
		fx.GetComponent<disappearing>().toDisappear = true;
		fx.GetComponent<Follow>().followObj = gameObject;
		fx.GetComponent<Follow>().followOffset = new Vector3 (0.0f, 0.0f, -3.0f);
		fx.GetComponent<Follow>().toFollow = true;
		if (m_physics.facingLeft)
			fx.transform.Rotate (new Vector3 (0f, 180f,0f));
		ParticleSystem [] partsys = fx.GetComponentsInChildren<ParticleSystem> ();
		foreach (ParticleSystem p in partsys) {
			ParticleSystem.MainModule mainP = p.main;
			mainP.startLifetime = lifeTime; 
		}
		*/
	}

	public bool IsAttacking()
	{
		return m_currentAttack != null && m_currentAttack.CurrentProgress != AttackState.INACTIVE;
	}

	public void RegisterStun(float st, bool defaultStun, Hitbox hb)
	{
		if (defaultStun)
			StartHitState(st);
		if (m_currentAttack != null)
			m_currentAttack.OnInterrupt(StunTime, defaultStun, hb);
	}

	void StartHitState(float st)
	{
		//Debug.Log ("Starting Hit State with Stun: "+ st);
		OnAttackEnd();
		StunTime = st;
		m_physics.CanMove = false;
	}

	public void RegisterHit(GameObject otherObj)
	{
		Debug.Log ("Collision: " + this + " " + otherObj);
		if (m_currentAttack != null)
			m_currentAttack.OnHitConfirm(otherObj);
	}

	public void EndStun()
	{
		if (m_attackable.Alive)
		{
			m_physics.CanMove = true;
			StunTime = 0.0f;
		}
	}

	public bool TryAttack(string[] attackList)
	{
		foreach (string s in attackList) {
			if (Attacks.ContainsKey(s)) {
				TryAttack(s);
				return true;
			}
		}
		return false;
	}

	public bool TryAttack(string attackName) {
		if (IsAttacking() || !Attacks.ContainsKey(attackName) || StunTime > 0.0f)
			return false;
		m_currentAttack = Attacks[attackName];
		m_physics.CanMove = false;
		m_currentAttack.ResetAndProgress();
		return true;
	}
}