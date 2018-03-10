using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : HealthBar {
	private Image m_damageImage; //flashes color red across screen when player takes damage
	private float m_flashSpeed = 10f;
	private Color m_flashColor = new Color(1f, 1f, 1f);

	protected override void Start()
	{
		base.Start();
		m_damageImage = GameObject.Find("HealthIcon").GetComponentInChildren<Image>();
		foreach (BasicMovement playerObj in Object.FindObjectsOfType<BasicMovement>())
		{
			if (playerObj.IsCurrentPlayer)
				Target = playerObj.GetComponent<Attackable>();
		}
	}
	 
	protected override void Update () {
		base.Update();
		if (Damaged)
			m_damageImage.color = m_flashColor;
		else
			m_damageImage.color = Color.Lerp (m_damageImage.color, Color.clear, m_flashSpeed*Time.deltaTime);
		Damaged = false;
	}
}
