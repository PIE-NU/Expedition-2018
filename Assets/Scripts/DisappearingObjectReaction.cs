using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingObjectReaction : LightDetection {

    private SpriteRenderer _spr;
    private Collider2D _collider;
    private Collider2D _lightCollider;

    void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _lightCollider = GameObject.Find("Flashlight").GetComponentInChildren<Collider2D>();
    }

    public void Update()
    {
        //_collider.IsTouchingLayers(10)
        if (!_collider.IsTouching(_lightCollider))
            StopReact();
        
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
