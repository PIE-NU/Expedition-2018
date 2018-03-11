using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

    //This script is intended for all pushable/draggable objects

    private Interactable trigger_area;
    private bool first_trigger;
    Vector2 offset;
    Vector3 char_offset;
    public string facing_direction;
    BasicMovement _bm;
    public bool draggable;
    private float restricted_direction;
    //This is the offset of the player collider
    private Vector2 character_height;

    // Use this for initialization
    void Start() {
        //trigger_area = gameObject.GetComponentInChildren<Interactable>();
        first_trigger = true;
        trigger_area = null;
	}
	
	// Update is called once per frame
	void Update () {

        foreach (Interactable area in gameObject.GetComponentsInChildren<Interactable>())
        {
            if (area.press_trigger && first_trigger)
            {
                //If one of the trigger areas is activated by the interactor assign it to trigger_area
                trigger_area = area;
                _bm = trigger_area.actor.gameObject.GetComponent<BasicMovement>();
                character_height = trigger_area.actor.gameObject.GetComponent<Collider2D>().offset;
            }
        }
        if (trigger_area)
        {
            //Assign the right direction 
            if (trigger_area.gameObject.GetComponent<BoxCollider2D>().offset.y > 0)
            {
                facing_direction = "DOWN";
            }
            if (trigger_area.gameObject.GetComponent<BoxCollider2D>().offset.y < 0)
            {
                facing_direction = "UP";
            }

            if (trigger_area.gameObject.GetComponent<BoxCollider2D>().offset.x > 0)
            {
                facing_direction = "LEFT";
            }
            if (trigger_area.gameObject.GetComponent<BoxCollider2D>().offset.x < 0)
            {
                facing_direction = "RIGHT";
            }
            //-------------------------------
            //Update the basic movement script
            _bm.push_direction = facing_direction;
            _bm.can_drag_item = draggable;
            if (trigger_area.press_trigger &&! first_trigger)
            {
                if (facing_direction == "LEFT" || facing_direction == "RIGHT")
                {
                    transform.position = -char_offset + new Vector3(trigger_area.actor.transform.position.x, restricted_direction, trigger_area.actor.transform.position.z) + (Vector3)character_height ;
                }
                if (facing_direction == "UP" || facing_direction == "DOWN")
                {
                    transform.position = -char_offset + new Vector3(restricted_direction, trigger_area.actor.transform.position.y, trigger_area.actor.transform.position.z) + (Vector3)character_height;
                }
            }
            //Run when the player presses the interaction key
            if (trigger_area.press_trigger && first_trigger)
            {

                offset = trigger_area.gameObject.GetComponent<Collider2D>().offset;
                //Because of the perspective, the character has to be slightly futher away when pushing the block down
                if (facing_direction == "RIGHT" || facing_direction == "LEFT")
                {
                    char_offset = new Vector3(offset.x * 2.5f, 0f, 0.2f);
                    restricted_direction = transform.position.y - character_height.y;
                }
                if(facing_direction == "UP")
                {
                    char_offset = new Vector3(0, offset.y * 1.2f);
                    restricted_direction = transform.position.x - character_height.x;
                }
                if (facing_direction == "DOWN")
                {
                    char_offset = new Vector3(offset.x, offset.y * 1.2f, 0.8f);
                    restricted_direction = transform.position.x - character_height.x;
                }


                trigger_area.actor.transform.position = transform.position + char_offset - (Vector3)character_height;
                first_trigger = false;
            }
            
            if (!trigger_area.press_trigger)
            {
                first_trigger = true;
                facing_direction = "";
                _bm.push_direction = "";
            }
        }
	}
}
