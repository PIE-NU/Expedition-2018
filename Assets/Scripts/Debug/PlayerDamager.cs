using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour {

	Attackable player;

	void Start () {
		foreach (BasicMovement playerObj in Object.FindObjectsOfType<BasicMovement>()) {
			if (playerObj.IsCurrentPlayer) {
				player = playerObj.GetComponent<Attackable>();
			}
		}
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Minus)) {
			player.damageObj (.5f);
		} else if (Input.GetKeyDown (KeyCode.Equals)) {
			player.damageObj (-.5f);
		}
	}
}
