using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (SpriteDepth))]
public class AnimatorSprite : MonoBehaviour
{
	PhysicsTD m_physics;
	Animator m_anim;
	List<string> m_states;
	string m_currentAnim = "";

	internal void Awake()
	{
		m_physics = GetComponent<PhysicsTD>();
		m_states = new List<string>();
		m_anim = GetComponent<Animator>();
	}

	public void Play(string[] stateNames)
	{
		foreach (string s in stateNames)
		{
			if (Play(s)) 
				break;
		}
	}

	public bool Play(string stateName, bool autoAlign = false)
	{
		if (m_currentAnim == stateName || m_currentAnim == "none")
			return true;

		if (autoAlign)
		{
			if (m_physics.Dir == Direction.DOWN)
				stateName += "_down";
			else if (m_physics.Dir == Direction.UP)
				stateName += "_up";
			else
				stateName += "_side";
		}

		if (m_states.Contains(stateName))
			return SetAndPlay(stateName);
		
		if (m_anim.HasState(0, Animator.StringToHash(stateName)))
		{
			m_states.Add(stateName);
			return SetAndPlay(stateName);
		}
		return false;
	}

	private bool SetAndPlay(string stateName)
	{
		m_anim.Play(stateName);
		m_currentAnim = stateName;
		return true;
	}

	public void SetSpeed(float speed)
	{
		m_anim.speed = speed;
	}
}
