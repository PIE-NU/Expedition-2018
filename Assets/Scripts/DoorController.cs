using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	public string toScene;
	private bool locked;

	void Start () {
		locked = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		//stuff
		SessionPersistentData.LastScene = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(toScene);
	}
}
