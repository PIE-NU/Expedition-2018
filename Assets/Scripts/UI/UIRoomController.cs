using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoomController : MonoBehaviour
{
	public GameObject EnemyHealthSlider;
	private GameObject m_enemyHealthHolder;

	internal void Start()
	{
		m_enemyHealthHolder = new GameObject("EnemyHealth");
		m_enemyHealthHolder.transform.SetParent(gameObject.transform);
		var aiController = Object.FindObjectOfType<AIRoomController>();
		foreach (AIFighter fighter in aiController.AiFighters)
		{
			var enemyHealth = GameObject.Instantiate(EnemyHealthSlider);
			enemyHealth.transform.SetParent(m_enemyHealthHolder.transform);
			var healthBar = enemyHealth.GetComponent<EnemyHealthBar>();
			healthBar.Target = fighter.Attackable;
		}
	}
}
