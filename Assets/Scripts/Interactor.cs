using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour {


    public Interactable prompted_interaction;
    private Collider2D col;
    private Text promt_ui;
    public string Interaction_Key;
    
	// Use this for initialization
	void Start () {
        col = gameObject.GetComponent<Collider2D>();
        promt_ui = GameObject.Find("Interaction_prompt").GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (prompted_interaction)
        {
           

           if (col.IsTouching(prompted_interaction.gameObject.GetComponent<Collider2D>()))
            {
                promt_ui.text = "Press '" + Interaction_Key + "' " + prompted_interaction.interaction_string;
                if (Input.GetKey(Interaction_Key))
                {
                    prompted_interaction.hold_trigger = true;
                    promt_ui.text = "TRIGGERED!";
                }
                else
                {
                    prompted_interaction.hold_trigger = false;
                }
                if (Input.GetKeyUp(Interaction_Key) && !prompted_interaction.press_trigger)
                {
                    prompted_interaction.press_trigger = true;
                }
                else if (Input.GetKeyUp(Interaction_Key) && prompted_interaction.press_trigger)
                {
                    prompted_interaction.press_trigger = false;
                }
            }
            else
            {
                prompted_interaction.hold_trigger = false;
                prompted_interaction.press_trigger = false;
                prompted_interaction = null;
                promt_ui.text = "";
            }



        }
	}
}
