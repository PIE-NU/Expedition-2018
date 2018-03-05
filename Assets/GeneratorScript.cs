using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : Interactable
{ 
    public bool buttonPressed = false;
    public ObjectSpotlight mySpotlight;

	void Update () {
	    if (press_trigger)
	    {
            Debug.Log("Generator button was pressed");
	        buttonPressed = true;
	    }
	}
}
