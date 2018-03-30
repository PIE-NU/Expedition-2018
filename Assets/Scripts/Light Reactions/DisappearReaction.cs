using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearReaction : LightDetection {

    private Collider2D _collider;
    public bool _triggered;

    void FixedUpdate()
    {
        if (!_collider)
            _collider = GetComponent<Collider2D>();

        if (_triggered && !_collider.IsTouchingLayers(10))
            StopReact();
    }

    public override void React()
    {
        
        _collider.isTrigger = true;
        _spr.enabled = false;
        foreach(Transform child in transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public override void StopReact()
    {
        
        _collider.isTrigger = false;
        _spr.enabled = true;
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
