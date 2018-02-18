using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	public string toScene;

	private GameManager gm;

	void Start(){
		gm = GameObject.Find("_GM").GetComponent<GameManager>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		gm.SwitchToSceneString (toScene);
	}
}
