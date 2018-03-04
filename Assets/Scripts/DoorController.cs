using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	public string toScene;

	private GameManager gm;

	void Start(){
		gm = GameObject.Find("_GM").GetComponent<GameManager>();
		//TODO: Check if door is a HubWorld door and lock appropriately based on player progress.
	}
	
	// comment to make different from master
	

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "player") {
			gm.SwitchToSceneString (toScene);
		}
	}
}
