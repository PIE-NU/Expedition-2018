using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private Hitbox m_hitbox;
	public float timealive=15f;


	void Start()
	{
		m_hitbox = GetComponent<Hitbox>();
		m_hitbox.Init();
		StartCoroutine(DeathTime());
	}

	void Update()
	{
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<Attackable>() != null) 
		{
			if (!col.gameObject.GetComponent<BasicMovement>().IsCurrentPlayer) 
			{
				col.gameObject.GetComponent<Attackable>().TakeHit(m_hitbox);
				Destroy (gameObject);
			}
				
		}
		else
			Destroy (gameObject);

	}

	private IEnumerator DeathTime()
	{
		yield return new WaitForSeconds (timealive);

		Destroy (gameObject);
	}

}
