using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void VoidDelegate();
    public static event VoidDelegate Cutscene;
    public static event VoidDelegate EndCutscene;

    public static void TriggerCutscene()
    {
        Cutscene();
    }

    public static void TriggerEnd()
    {
        EndCutscene();
    }
}

