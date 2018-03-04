using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {


    private Collider2D trigger_area;
    public Interactor actor;
    public string interaction_string;
    public bool hold_trigger;
    public bool press_trigger;
    
        // Use this for initialization
	void Start () {
        trigger_area = GetComponent<Collider2D>();
        //Will become true if the interactor presses/holds the interaction key while in this interactable's area
        hold_trigger = false;
        press_trigger = false;

	}
	
	// Update is called once per frame
	void Update () {
       /* if (!trigger_area.IsTouching(actor.gameObject.GetComponent<Collider2D>()))
        {
            actor.prompted_interaction = null;
            actor = null;
        }*/
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Interactor>())
        {
            actor = collision.gameObject.GetComponent<Interactor>();
            actor.prompted_interaction = this;
        }
    }

   
}
