using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {

    public GameObject lantern, flashlight;
    public float lanternCharge, flashlightCharge;
    public KeyCode lanternKey, flashlightKey;
    private bool lanternOn, flashlightOn;
    private float negateLantern = -1f, negateFlash = -1f;
    public float lanternFullCharge = 15f, flashlightFullCharge = 10f;

   

    // Use this for initialization
    void Start () {
        //lanternOn = flashlightOn = false;
        lanternCharge = lanternFullCharge;
        flashlightCharge = flashlightFullCharge;

        lanternOn = lantern.activeInHierarchy;
        flashlightOn = flashlight.activeInHierarchy;
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(lanternKey))
        {
            if (lanternOn)
            {
                lantern.SetActive(false);
                lanternOn = false;
                negateLantern = 1f;
            }
            else
            {
                lantern.SetActive(true);
                lanternOn = true;
                negateLantern = -1f;
            }
        }

        if (Input.GetKeyDown(flashlightKey))
        {
            if (flashlightOn)
            {
                flashlight.SetActive(false);
                flashlightOn = false;
                negateFlash = 1f;
            }
            else
            {
                flashlight.SetActive(true);
                flashlightOn = true;
                negateFlash = -1f;
            }
        }
        if (lanternCharge >= lanternFullCharge && !lanternOn)
            lanternCharge = lanternFullCharge;
        else if (lanternCharge <= 0f && lanternOn)
        {
            lanternOn = false;
            lantern.SetActive(false);
            negateLantern = 1f;
        }
        else
            lanternCharge += Time.deltaTime * negateLantern;
        

        if (flashlightCharge >= flashlightFullCharge && !flashlightOn)
            flashlightCharge = flashlightFullCharge;
        else if (flashlightCharge <= 0f && flashlightOn)
        {
            flashlightOn = false;
            flashlight.SetActive(false);
            negateFlash = 1f;
        }
        else
            flashlightCharge += Time.deltaTime * negateFlash;
        
    }
}
