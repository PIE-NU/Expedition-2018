/*
 * Singleton audio manager intended to be persistent across all scenes.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

	private GameObject m_music;

	void Awake() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GameObject newSceneMusicObj = GameObject.Find("GameMusic");

		if (!newSceneMusicObj)
		{
			Debug.Log("nothing found");
			return;
		}

		if (m_music == null) {

			Debug.Log("first music");
			//If music doesn't exist yet, replace it with new one
			m_music = newSceneMusicObj;

			//rename to avoid problems with already existing prefabs in later scenes
			m_music.name = "CurrentMusic";
			DontDestroyOnLoad(m_music);
		} else {

			Debug.Log("Checking music");
			//Else check if the new music is the same as the current scene's music
			AudioClip oldClip = m_music.GetComponent<AudioSource>().clip;
			AudioClip newClip = newSceneMusicObj.GetComponent<AudioSource>().clip;

			if(oldClip == newClip){
				Debug.Log("REWIND REWIND REWIND");
				Destroy(newSceneMusicObj);
			} else {
				Debug.Log("REPLACING");
				Destroy(m_music);
				m_music = newSceneMusicObj;
				DontDestroyOnLoad(m_music);
			}
		}
	}
}
