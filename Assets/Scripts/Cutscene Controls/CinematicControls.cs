using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;


public class CinematicControls : MonoBehaviour {

    public PlayableAsset CutsceneTimeline;
    public bool DisableMovement,
                DisplaysText,
                RequiresInput;
    private Collider2D _trigger;
    private GameObject _mplayer;
    private double _mtimer;
    private bool _mplaying;

	// Use this for initialization
	void Start () {
        _trigger = GetComponent<Collider2D>();
	}

    void Update()
    {
        if (_mplaying)
        {
            _mtimer -= Time.deltaTime;
            if (_mtimer <= 0)
            {
                EventManager.TriggerEnd();
                _mplaying = false;
            }
        }
    }

    void OnEnable()
    {
        EventManager.Cutscene += PlayTimeline;
        if (DisableMovement)
        {
            EventManager.Cutscene += DisablePlayerMovement;
            EventManager.EndCutscene += RestorePlayerMovement;
        }
    }

    void OnDisable()
    {
        EventManager.Cutscene -= PlayTimeline;
        if (DisableMovement)
        {
            EventManager.Cutscene -= DisablePlayerMovement;
            EventManager.EndCutscene -= RestorePlayerMovement;
        }
    }

    public void PlayTimeline()
    {
        if (!_mplayer)
            _mplayer = GameObject.Find("player");

        var pd = _mplayer.GetComponent<PlayableDirector>();
        pd.playableAsset = CutsceneTimeline;
        pd.Play();
        EndAt();
    }

    void EndAt()
    {
        _mtimer = CutsceneTimeline.duration;
        _mplaying = true;
    }
	
    public void DisablePlayerMovement()
    {
        _mplayer.GetComponent<BasicMovement>().enabled = false;
    }

    public void RestorePlayerMovement()
    {
        _mplayer.GetComponent<BasicMovement>().enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "player")
        {
            _mplayer = other.gameObject;
            EventManager.TriggerCutscene();
        }
    }
}
