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
		//SceneManager.LoadScene(toScene);
		StartCoroutine(LoadScene(toScene));
	}

	IEnumerator LoadScene(string to)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(to);

		//Wait until the last operation fully loads to return anything
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}
