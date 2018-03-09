using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBackground : MonoBehaviour {
	void Start () {
		GetComponent<Renderer> ().receiveShadows = true;
		Vector3 pos = transform.position;
		transform.position = new Vector3 (pos.x, pos.y, pos.y);
		Quaternion q = transform.rotation;
		Debug.Log (q.eulerAngles.x);
		if (q.eulerAngles.x != 45f) {
			Vector3 s = transform.localScale;
			transform.localScale = new Vector3 (s.x, s.y * 1.42f, s.z); //Approximately sqrt(2)
		}
		transform.rotation = Quaternion.Euler (45f, 0f, 0f);
	}
}
