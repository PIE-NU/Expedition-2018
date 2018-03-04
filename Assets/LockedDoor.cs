using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : DoorController
{
    //this is temp hardcoded to a generator script
    [SerializeField] private GeneratorScript MyGeneratorScript;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "player" && MyGeneratorScript.buttonPressed == true )
        {
            gm.SwitchToSceneString(toScene);
        }
    }
}
