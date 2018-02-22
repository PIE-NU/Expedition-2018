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
//			if (m_instance == null)
//				m_instance = this;
			return m_instance;
		}
		set { m_instance = value; }
	}

	private SessionPersistentData m_data;

	void Start () {
		//Check if instance already exists
		if (m_instance == null) {
			Debug.Log ("Instantiated GM");
			m_instance = this;
		}else if(m_instance != this){
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);

		SessionPersistentData pData = new SessionPersistentData();
		string lastScene = pData.LastScene;

		if (lastScene != null) {
			Debug.Log ("Last scene was:" + lastScene);
		} else {
			Debug.Log ("No last scene");
		}
	}

	void Update () {
		
	}

	public void SwitchToSceneString(string to){
		Debug.Log ("GM was told to switch to scene " + to);
		SceneManager.LoadScene (to);
	}
}
