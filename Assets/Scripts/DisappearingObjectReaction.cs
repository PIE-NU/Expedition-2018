using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingObjectReaction : LightDetection {

    private SpriteRenderer _spr;
    private Collider2D _collider;
    private Collider2D _lightColliderOne;
    private Collider2D _lightColliderTwo;

    void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        //I horribly hacked this together just to get some stuff working
        _lightColliderOne = GameObject.FindGameObjectWithTag("Cart Light").GetComponentInChildren<Collider2D>();
        _lightColliderTwo = GameObject.FindGameObjectWithTag("Spotlight").GetComponentInChildren<Collider2D>();

        Debug.Log(GameObject.FindGameObjectWithTag("Cart Light"));
        Debug.Log(GameObject.FindGameObjectWithTag("Spotlight"));

        Debug.Log("LightColliderOne is " + _lightColliderOne);
        Debug.Log("LightColliderTwo is " + _lightColliderTwo);
    }

    public void Update()
    {
        //_collider.IsTouchingLayers(10)
        if (!_collider.IsTouching(_lightColliderOne) || !_collider.IsTouching(_lightColliderTwo))
        {
            StopReact();
            Debug.Log("I am colliding with the wall");
        }
            
    }

    public override void React()
    {
        _collider.isTrigger = true;
        _spr.enabled = false;
    }

    public override void StopReact()
    {
        _collider.isTrigger = false;
        _spr.enabled = true;
    }
}
