using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightDetection : MonoBehaviour {

    
	public void OnTriggerEnter2D(Collider2D c)
    {
        //Calls function with specific reaction
        React();
    }

    public void OnTriggerExit2D(Collider2D c)
    {
        //Calls function to stop specific reaction
        StopReact();
    }

    public abstract void React();

    public abstract void StopReact();
}
