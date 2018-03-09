using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    //This script is intended for all pushable/draggable objects
    private Interactable trigger_area;
    private bool first_trigger;
    Vector2 offset;
    Vector3 char_offset;
    public Direction FacingDirection;
    BasicMovement m_basicMovement;
    public bool draggable;

    // Use this for initialization
    void Start() {
        //trigger_area = gameObject.GetComponentInChildren<Interactable>();
        first_trigger = true;
        
	}
	
	// Update is called once per frame
	void Update () {

        foreach (Interactable area in gameObject.GetComponentsInChildren<Interactable>())
        {
            if (area.press_trigger)
            {
                //If one of the trigger areas is activated by the interactor assign it to trigger_area
                trigger_area = area;
				m_basicMovement = trigger_area.actor.gameObject.GetComponent<BasicMovement>();
            }
        }

        // Assign the right direction 
        if (trigger_area.gameObject.GetComponent<BoxCollider2D>().offset.y > 0)
			FacingDirection = Direction.DOWN;
		if (trigger_area.gameObject.GetComponent<BoxCollider2D>().offset.y < 0)
			FacingDirection = Direction.UP;
        if (trigger_area.gameObject.GetComponent<BoxCollider2D>().offset.x > 0)
			FacingDirection = Direction.LEFT;
        if (trigger_area.gameObject.GetComponent<BoxCollider2D>().offset.x < 0)
			FacingDirection = Direction.RIGHT;

        //Update the basic movement script
		m_basicMovement.DirPush = FacingDirection;
		m_basicMovement.CanDrag = draggable;
		m_basicMovement.IsDragging = true;

        //Run when the player presses the interaction key
        if (trigger_area.press_trigger && first_trigger)
        {

            offset = trigger_area.gameObject.GetComponent<Collider2D>().offset;
            //Because of the perspective, the character has to be slightly futher away when pushing the block down
            if(offset.y > 0)
            {
                char_offset = new Vector3(offset.x * 2.5f, offset.y * 2.7f);
            }
            else
            {
                char_offset = new Vector3(offset.x * 2.5f, offset.y * 1f);
            }
            
            
            trigger_area.actor.transform.position = transform.position + char_offset;
            first_trigger = false;
        }
        if (trigger_area.press_trigger)
        {
            transform.position = -char_offset + trigger_area.actor.transform.position;
        }
        if (!trigger_area.press_trigger)
        {
            first_trigger = true;
			m_basicMovement.IsDragging = false;
        }
	}
}
