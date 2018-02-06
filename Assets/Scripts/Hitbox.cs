﻿using UnityEngine;
using System.Collections.Generic;

public class Hitbox : MonoBehaviour {

	public List<Attackable> collidedObjs = new List<Attackable> (); 
	protected List<Attackable> overlappingControl = new List<Attackable> (); 
	public float damage = 10.0f;
	public bool fixedKnockback = false;
	public Vector2 knockback = new Vector2(0.0f,40.0f);
	public float hitboxDuration = 1.0f;
	public bool TimedHitbox = true;
	public string faction = "noFaction";
	public GameObject followObj;
	public GameObject creator;
	public GameObject hitFX;
	public GameObject blockFX;
	public bool toFollow = false;
	public bool reflect = true;
	public Vector2 followOffset;
	public float stun = 0.0f;
	public List<string> mAttr;

	protected bool randomKnockback;
	protected float minKBX;
	protected float maxKBX;
	protected float minKBY;
	protected float maxKBY;

	public bool Active = true;

	// Use this for initialization
	void Start () {
		if (randomKnockback) {
			knockback.x = Random.Range (minKBX, maxKBX);
			knockback.y = Random.Range (minKBY, maxKBY);
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateTick ();
	}
	public void updateTick() {
		if (hitboxDuration > 0.0f && TimedHitbox) {
			hitboxDuration = hitboxDuration - Time.deltaTime;
		} else if (TimedHitbox){
			GameObject.Destroy (gameObject);
		}
		if (followObj != null && toFollow) {
			transform.position = new Vector3(followObj.transform.position.x + followOffset.x, followObj.transform.position.y + followOffset.y,0);
		}
	}
	public void setKnockback(Vector2 kb) {
		knockback = kb;
	}

	public void setFixedKnockback(bool fixedKB) {
		fixedKnockback = fixedKB;
	}

	public void setDamage(float dmg) {
		damage = dmg;
	}
	public void setHitboxDuration (float time) {
		hitboxDuration = time;
	}
	public void setScale(Vector2 scale) {
		transform.localScale = scale;
	}
	public void setFaction(string fact) {
		faction = fact;
	}
	public void setFollow(GameObject obj, Vector2 offset) {
		toFollow = true;
		followObj = obj;
		followOffset = offset;
	}
	public void addAttribute(string attr) {
		mAttr.Add (attr);
	}
	void OnDrawGizmos() {
		Gizmos.color = new Color (1, 0, 0, .5f);
		Gizmos.DrawCube (transform.position, transform.localScale);
	}
	public void randomizeKnockback(float minX,float maxX,float minY, float maxY) {
		randomKnockback = true;
		fixedKnockback = true;
		minKBX = minX;
		minKBY = minY;
		maxKBX = maxX;
		maxKBY = maxY;
	}
	protected string onAttackable(Attackable atkObj) {
		string hitResult = "none";
		if (atkObj &&
			!collidedObjs.Contains (atkObj)) {
			if (faction == "noFaction" || atkObj.faction == "noFaction" ||
				faction != atkObj.faction) {
				if (randomKnockback) {
					knockback.x = Random.Range (minKBX, maxKBX);
					knockback.y = Random.Range (minKBY, maxKBY);
				}
				string hitType = atkObj.takeHit (this);
				hitResult = hitType;
				/*if (creator) {
					if (creator.GetComponent<HitboxMaker> ()) {
						creator.GetComponent<HitboxMaker> ().registerHit (atkObj.gameObject);
					} else if (creator.GetComponent<Shooter> ()) {
						creator.GetComponent<Shooter> ().registerHit (atkObj.gameObject);
					}
				}*/
			}
			collidedObjs.Add (atkObj);
		}
		return hitResult;
	}
	internal string OnTriggerEnter2D(Collider2D other)
	{
		if (Active) {
			return onAttackable (other.gameObject.GetComponent<Attackable> ());
		}
		return "none";
	}

	internal void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.GetComponent<Attackable> () 
			&& overlappingControl.Contains(other.gameObject.GetComponent<Attackable>())) {
			overlappingControl.Remove (other.gameObject.GetComponent<Attackable> ());
		}
	}
}
