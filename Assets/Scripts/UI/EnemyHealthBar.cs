using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : HealthBar
{
	public Image healthBar;
	public float currentHealth;
	private Camera m_camera;

	protected override void Start()
	{
		base.Start();
		m_camera = Object.FindObjectOfType<Camera>();
	}

	protected override void Update()
	{
		base.Update();
		if (!HealthSlider) {
			Debug.Log (gameObject.transform.parent.gameObject);
			return;
		}
		HealthSlider.transform.position = m_camera.WorldToScreenPoint(Target.transform.position);
	}
}