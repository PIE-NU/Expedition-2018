using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour {

    
	public void OnTriggerEnter2D(Collider2D c)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void OnTriggerExit2D(Collider2D c)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
