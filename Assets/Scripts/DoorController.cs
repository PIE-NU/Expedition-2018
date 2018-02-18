using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	public string toScene;

	void OnTriggerEnter2D(Collider2D other) {
		GameManager gm = GameObject.Find("_GM").GetComponent<GameManager>();
		gm.SwitchToSceneString (toScene);
	}
}
