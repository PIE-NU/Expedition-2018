﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    public PhysicsTD directionComponent;
    public GameObject player;

    public Vector3 upPosition;
    public Vector3 downPosition;
    public Vector3 leftPosition;
    public Vector3 rightPosition;

    private Quaternion upRotation, downRotation, leftRotation, rightRotation;

    private Direction dir;

    // Use this for initialization
    void Start () {
        if (directionComponent == null)
            directionComponent = GetComponent<PhysicsTD>();

		upRotation = Quaternion.Euler(-15f, 0f, 0f);
		downRotation = Quaternion.Euler(105f, 0f, 0f);
		leftRotation = Quaternion.Euler(20f, -70f, 90f);
		rightRotation = Quaternion.Euler(20f, 70f, -90f);


        dir = directionComponent.Dir;
        ChooseDirection();
    }
	void OnEnable()
    {
        ChooseDirection();
    }
	// Update is called once per frame
	void Update () {

        Direction d = directionComponent.Dir;
        if (d != dir)
        {
            ChooseDirection();
        }
    }

    void ChooseDirection()
    {
        Direction d = directionComponent.Dir;
        switch (d)
        {
            //Add position to player pos
            //Quaternion rotation to current direction
            case Direction.UP:
                transform.position = player.transform.position + upPosition;
                transform.rotation = upRotation;
                break;
            case Direction.DOWN:
                transform.position = player.transform.position + downPosition;
                transform.rotation = downRotation;
                break;
            case Direction.LEFT:
                transform.position = player.transform.position + leftPosition;
                transform.rotation = leftRotation;
                break;
            case Direction.RIGHT:
                transform.position = player.transform.position + rightPosition;
                transform.rotation = rightRotation;
                break;
        }
        dir = d;
    }
}
