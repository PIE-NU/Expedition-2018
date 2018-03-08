﻿using System;
using System.Collections.Generic;
using UnityEngine;

public enum AttackState { STARTUP, ATTACK, RECOVERY, INACTIVE };
public delegate void AttackProgress(AttackState attackState);

public class AttackInfo : MonoBehaviour
{
	private AttackState m_progress;
	private float m_timeSinceStart = 0.0f;
	private Dictionary<AttackState, float> m_progressEndTimes;
	private Dictionary<AttackState, Action> m_progressCalls;

	public AttackState CurrentProgress { get { return m_progress; } }
	private event AttackProgress ProgressEvent = delegate {};

	public Vector2 HitboxScale = new Vector2 (1.0f, 1.0f);
	public Vector2 HitboxOffset = new Vector2(0f,0f);
	public float Damage = 10.0f;
	public float Stun = 0.3f;
	public float HitboxDuration = 0.5f;
	public Vector2 Knockback = new Vector2(10.0f,10.0f);

	public float AnimSpeed = 1f;
	public float StartUpTime = 0.0f;
	public float AttackTime = 0.3f;
	public float RecoveryTime = 0.5f;

	public string AttackName = "default";
	public string HitType = "melee";
	public string StartUpAnimation = "none";
	public string RecoveryAnimation = "none";

	public GameObject AttackFX;
	public AudioClip StartupSoundFX;
	public AudioClip AttackSoundFX;

	protected PhysicsTD m_physics;
	protected HitboxMaker m_hitboxMaker;

	internal void Awake()
	{
		m_physics = GetComponent<PhysicsTD>();
		m_hitboxMaker = GetComponent<HitboxMaker>();

		m_progress = AttackState.INACTIVE;
		m_progressEndTimes = new Dictionary<AttackState, float>()
		{
			{ AttackState.STARTUP, StartUpTime },
			{ AttackState.ATTACK, StartUpTime + AttackTime },
			{ AttackState.RECOVERY, StartUpTime + AttackTime + RecoveryTime },
			{ AttackState.INACTIVE, 0 }
		};
		m_progressCalls = new Dictionary<AttackState, Action>()
		{
			{ AttackState.STARTUP, OnAttack },
			{ AttackState.ATTACK, OnRecovery },
			{ AttackState.RECOVERY, OnConclude },
			{ AttackState.INACTIVE, OnStartUp }
		};
	}

	public void AddListener(AttackProgress ap)
	{
		ProgressEvent += ap;
	}

	public void Progress()
	{
		m_timeSinceStart += Time.deltaTime;
		if (m_timeSinceStart < m_progressEndTimes [m_progress])
			return;
		m_progressCalls[NextInProgression()]();
		ProgressEvent.Invoke(m_progress);
	}

	private AttackState NextInProgression()
	{
		m_progress = (m_progress == AttackState.INACTIVE) ? AttackState.STARTUP : m_progress + 1;
		return m_progress;
	}

	public void ResetAndProgress()
	{
		m_timeSinceStart = 0;
		Progress();
	}

	public virtual void OnHitConfirm(GameObject other) {}

	public virtual void OnInterrupt(float stunTime, bool successfulHit, Hitbox hb)
	{
		m_hitboxMaker.ClearHitTypes();
	}

	protected virtual void OnStartUp()
	{
	}

	protected virtual void OnAttack()
	{
		CreateHitbox();
	}

	protected virtual void OnRecovery()
	{
	}

	protected virtual void OnConclude()
	{
	}

	private void CreateHitbox()
	{
		m_hitboxMaker.AddHitType(HitType);
		Vector2 offset = m_physics.OrientVectorToDirection(HitboxOffset);
		m_hitboxMaker.CreateHitbox(HitboxScale, offset, Damage, Stun, HitboxDuration, Knockback, true, true);
	}
}

