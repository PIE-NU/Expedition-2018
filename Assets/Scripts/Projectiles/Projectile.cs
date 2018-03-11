using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private Hitbox m_hitbox;

	void Start()
	{
		m_hitbox = GetComponent<Hitbox>();
		m_hitbox.Init();
	}
}
