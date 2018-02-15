using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {

    public GameObject lantern, flashlight;
    public float lanternCharge, flashlightCharge;
    public KeyCode lanternKey, flashlightKey;
    public bool lanternOn, flashlightOn;
    private float negatorLantern = 1f, negatorFlash = 1f;
    public float lanternFullCharge = 15f, flashlightFullCharge = 10f;
    

	// Use this for initialization
	void Start () {
        lanternOn = flashlightOn = false;
        lanternCharge = lanternFullCharge;
        flashlightCharge = flashlightFullCharge;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(lanternKey))
        {
            if (lanternOn)
            {
                lantern.SetActive(false);
                lanternOn = false;
                negatorLantern = 1f;
            }
            else
            {
                lantern.SetActive(true);
                lanternOn = true;
                negatorLantern = -1f;
            }
        }

        if (Input.GetKeyDown(flashlightKey))
        {
            if (flashlightOn)
            {
                flashlight.SetActive(false);
                flashlightOn = false;
                negatorFlash = 1f;
            }
            else
            {
                flashlight.SetActive(true);
                flashlightOn = true;
                negatorFlash = -1f;
            }
        }
        if (lanternCharge > lanternFullCharge)
            lanternCharge = lanternFullCharge;
        else if (lanternCharge <= 0f && lanternOn)
        {
            lanternOn = false;
            lantern.SetActive(false);
            negatorLantern = 1f;
        }
        else
            lanternCharge += Time.deltaTime * negatorLantern;
        

        if (flashlightCharge > flashlightFullCharge)
            flashlightCharge = flashlightFullCharge;
        else if (flashlightCharge <= 0f && flashlightOn)
        {
            flashlightOn = false;
            flashlight.SetActive(false);
            negatorFlash = 1f;
        }
        else
            flashlightCharge += Time.deltaTime * negatorFlash;
        
    }
}
