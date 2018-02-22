/*
 * Singleton game manager intended to be persistent across all scenes.
 * Singleton maintained by creating a static instance of the class recursively.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager m_instance;
	public static GameManager Instance
	{
		get {
			return m_instance;
		}
		set { m_instance = value; }
	}

	private SessionPersistentData m_data;

	void Start () {
		//Check if instance already exists
		if (m_instance == null) {
			//Instantiation logic should go entirely in here.
			Debug.Log ("Instantiated GM");
			m_instance = this;
			m_data = new SessionPersistentData();

			SceneManager.sceneLoaded += OnSceneLoaded;
		}else if(m_instance != this){
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		string lastScene = m_data.LastScene;

		if (lastScene != null) {
			Debug.Log ("Last scene was:" + lastScene);
		} else {
			Debug.Log ("No last scene");
		}
	}

	public void SwitchToSceneString(string to){
		m_data.LastScene = SceneManager.GetActiveScene ().name;
		SceneManager.LoadScene (to);
	}
}
