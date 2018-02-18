/*
 * Singleton game manager intended to be persistent across all scenes.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static SessionPersistentData m_data;
	public  SessionPersistentData PersistentData
	{
		get {
			if (m_data == null)
				m_data = new SessionPersistentData();
			return m_data;
		}
		set { m_data = value; }
	}


	private static GameManager m_instance;
	public  GameManager GM
	{
		get {
			if (m_instance == null)
				m_instance = new GameManager();
			return m_instance;
		}
		set { m_instance = value; }
	}

	void Start () {
		GM = this;
		SessionPersistentData pData = PersistentData;
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
