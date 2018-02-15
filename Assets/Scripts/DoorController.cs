using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	public string toScene;

	void OnTriggerEnter2D(Collider2D other) {
		SessionPersistentData.LastScene = SceneManager.GetActiveScene().name;
		//TODO: Delegatae scene transition logic to GameManager
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
