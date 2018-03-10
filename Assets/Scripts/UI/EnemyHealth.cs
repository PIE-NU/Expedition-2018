using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour {

	public Image healthBar;

	Attackable enemy;
	public float currentHealth;
	//bool isDead;
	//bool damaged;

	void Start () {
		//healthSlider = GetComponentInChildren<Slider> ();
		healthBar = GameObject.Find("HealthBar").GetComponentInChildren<Image> ();
		foreach (BasicMovement playerObj in Object.FindObjectsOfType<BasicMovement>()) {
			if (playerObj.IsCurrentPlayer) {
				enemy = playerObj.GetComponent<Attackable>();
			}
		}
	}

	void Update () {
		CheckEnemyHealth ();
		UpdateHealthUI ();
	}

	void CheckEnemyHealth () {
		currentHealth = enemy.Health;
		//isDead = player.alive;
		//damaged = currentHealth < oldHealth;
	}

	void UpdateHealthUI () {
		healthBar.fillAmount = currentHealth/100f;
	}
}