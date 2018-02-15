/*
 * Singleton game manager intended to be persistent across all scenes.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager m_instance;
	public static GameManager Get()
	{
		if (m_instance == null)
			m_instance = new GameManager();
		return m_instance;
	}

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
