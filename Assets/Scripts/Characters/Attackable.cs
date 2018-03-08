﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
	private float m_health = 100.0f;
	public float Health { get { return m_health; } private set { m_health = value; } }
	public float MaxHealth = 100.0f;

	public bool Alive = true;
	public float DeathTime = 0.0f;
	private float m_currDeathTime;

	public Dictionary<string,float> Resistences = new Dictionary<string,float>();
	private PhysicsTD m_movementController;
	private Fighter m_fighter;

	// public AudioClip Hit;

	internal void Awake()
	{
		m_movementController = GetComponent<PhysicsTD>();
		m_fighter = GetComponent<Fighter>();
		m_health = Mathf.Min (m_health, MaxHealth);
		m_currDeathTime = DeathTime;
	}

	private void CheckDeath()
	{
		Alive = m_health > 0;
		if (Alive)
			return;
		if (m_currDeathTime < 0.0f)
			Destroy(gameObject);
		m_currDeathTime -= Time.deltaTime;
	}

	private void CheckResistanceValidities()
	{
		List<string> keys = new List<string> (Resistences.Keys);
		foreach (string k in keys)
		{
			Resistences[k] -= Time.deltaTime;
			if (Resistences[k] <= 0.0f)
				Resistences.Remove(k);
		}
	}

	internal void Update() {
		CheckDeath();
		CheckResistanceValidities();
	}

	public void AddResistence(string attribute, float time) {
		Resistences[attribute] = time;
	}

	private bool CheckHasResistanceTo(List<string> hitTypes)
	{
		foreach (string k in Resistences.Keys) {
			if (hitTypes.Contains(k)) {
				return true;
			}
		}
		return false;
	}

	private void ApplyHitToPhysicsTD(Hitbox hb)
	{
		if (!m_movementController)
			return;

		if (hb.IsFixedKnockback)
		{
			m_movementController.AddToVelocity(hb.Knockback);
			return;
		}

		Vector3 hitVector = transform.position - hb.transform.position;
		float angle = Mathf.Atan2(hitVector.y,hitVector.x); //*180.0f / Mathf.PI;
		Vector2 force = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		force.Scale(new Vector2(hb.Knockback.magnitude, hb.Knockback.magnitude));
		float counterF = m_movementController.Velocity.y * (1 / Time.deltaTime);
		if (counterF < 0)
			force.y = force.y - counterF;
		
		m_movementController.AddToVelocity(force);
	}

	public string TakeHit(Hitbox hb)
	{
		// Debug.Log (gameObject + "is Taking hit");
		if (hb.HasHitTypes() && CheckHasResistanceTo(hb.HitTypes))
		{
			if (m_fighter)
				m_fighter.RegisterStun(hb.Stun, false, hb);
			return "block";
		}

		DamageObj(hb.Damage);
		ApplyHitToPhysicsTD(hb);

		if (hb.Stun > 0 && m_fighter)
			m_fighter.RegisterStun(hb.Stun, true, hb);
		return "hit";
	}

	public void DamageObj(float damage)
	{
		m_health = Mathf.Max(Mathf.Min(MaxHealth, m_health - damage), 0);
		Alive = (m_health > 0);
	}
}
