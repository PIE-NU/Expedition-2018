using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour {

    SpriteRenderer _spr;
    GameObject player;

    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        player = GameObject.Find("player");
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void OnTriggerExit2D(Collider2D c)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void React()
    {

    }

    public void StopReact()
    {

    }

    private void Update()
    {
        if(player.GetComponent<SpriteRenderer>().sortingOrder < 30 + _spr.sortingOrder )//&& (player.transform.position.x < transform.position.x))
        {
            gameObject.layer = 9;
        }
        else
        {
            gameObject.layer = 8;
        }
    }
}
