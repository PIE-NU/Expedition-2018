using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRotation : MonoBehaviour {

    Interactable _intr;
    ObjectSpotlight _obsl;
    private bool pressed;
    public bool ccw;

	// Use this for initialization
	void Start () {
        _intr = transform.parent.GetChild(1).gameObject.GetComponent<Interactable>();
        _obsl = gameObject.GetComponent<ObjectSpotlight>(); 
        pressed = _intr.press_trigger;
	}
	
	// Update is called once per frame
	void Update () {
        if (_intr.press_trigger != pressed)
        {
            pressed = _intr.press_trigger;

            if (!ccw)
            {
                if (_obsl.PermanentDirection == Direction.DOWN)

                {
                    _obsl.PermanentDirection = Direction.LEFT;
                }
                else
                {
                    _obsl.PermanentDirection++;
                }
            }
            else
            {
                if (_obsl.PermanentDirection == Direction.LEFT)

                {
                    _obsl.PermanentDirection = Direction.DOWN;
                }
                else
                {
                    _obsl.PermanentDirection--;
                }
            }
        }
	}
}
