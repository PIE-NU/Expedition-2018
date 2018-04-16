/*
 * Singleton audio manager intended to be persistent across all scenes.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private AudioSource m_musicSource;

	// Use this for initialization
	void Start () {
		m_musicSource = GameObject.Find("GameMusic");
		if (m_musicSource == null) {
			//panic
			Debug.Log("Audio manager unable to find GameMusic");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
