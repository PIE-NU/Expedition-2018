using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	public string toScene;

	public GameManager gm;

	void Start(){
	    //This is not right. We are keeping the game manager on the main camera
        //gm = GameObject.Find("_GM").GetComponent<GameManager>();

        gm = Camera.main.GetComponent<GameManager>();
        //TODO: Check if door is a HubWorld door and lock appropriately based on player progress.
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "player") {
			gm.SwitchToSceneString (toScene);
		}
	}
}
