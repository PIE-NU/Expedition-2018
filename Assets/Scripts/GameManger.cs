using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string lastScene = SessionPersistentData.LastScene;

		if (lastScene != null) {
			Debug.Log ("Last scene was:" + lastScene);
		} else {
			Debug.Log ("No last scene");
		}
	}

	void Update () {
		
	}
}
