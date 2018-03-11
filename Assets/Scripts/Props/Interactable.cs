using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {


    public Interactor actor;
    public string interaction_string;
    public bool hold_trigger;
    public bool press_trigger;
    
        // Use this for initialization
	void Start () {
        actor = null;
        //Will become true if the interactor presses/holds the interaction key while in this interactable's area
        hold_trigger = false;
        press_trigger = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (actor = collision.gameObject.GetComponent<Interactor>())
        {
            actor.prompted_interaction = this;
        }
    }
}
