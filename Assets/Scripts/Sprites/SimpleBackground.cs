using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBackground : MonoBehaviour {
	void Start () {
		GetComponent<Renderer> ().receiveShadows = true;
		Vector3 pos = transform.position;
		transform.position = new Vector3 (pos.x, pos.y, 0.5f);
	}
}
