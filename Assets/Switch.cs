using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    [SerializeField] public ObjectSpotlight mySpotlight;

    public int pressCount = 1;

    //public Direction currDirection = global::Direction.DOWN;

    void Start()
    {
        //Direction currDirection = global::Direction.DOWN;
    }

    void Update()
    {
        if (press_trigger)
        {
            if (mySpotlight != null)
            {
                pressCount++;
                pressCount = pressCount % 4;
                //currDirection += 1;
                Debug.Log("Pressed the switch trigger");
                mySpotlight.ChangeDirection(pressCount);
                press_trigger = false;
            }
        }
    }
}
