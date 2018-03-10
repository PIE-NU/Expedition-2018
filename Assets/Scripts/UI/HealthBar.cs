using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public Slider HealthSlider;
	public Attackable Target;

	protected float CurrentHealth;
	protected bool Damaged;

	protected virtual void Start()
	{
		HealthSlider = GetComponentInChildren<Slider>();
	}

	protected virtual void Update()
	{
		CheckTargetHealth ();
		UpdateHealthUI ();
	}

	void CheckTargetHealth()
	{
		if (!Target) return;
		float oldHealth = CurrentHealth;
		CurrentHealth = Target.Health;
		Damaged = CurrentHealth < oldHealth;
	}

	void UpdateHealthUI()
	{
		if (!HealthSlider) return;
		HealthSlider.value = CurrentHealth;
	}
}
