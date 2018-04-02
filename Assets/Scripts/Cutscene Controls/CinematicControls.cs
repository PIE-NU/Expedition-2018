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
    private GameObject _player;
    private double _timer;
    private bool _playing;

	// Use this for initialization
	void Start () {
        _trigger = GetComponent<Collider2D>();
	}

    void Update()
    {
        if (_playing)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                EventManager.TriggerEnd();
                _playing = false;
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
        if (!_player)
            _player = GameObject.Find("player");

        var pd = _player.GetComponent<PlayableDirector>();
        pd.playableAsset = CutsceneTimeline;
        pd.Play();
        EndAt();
    }

    void EndAt()
    {
        _timer = CutsceneTimeline.duration;
        _playing = true;
    }
	
    public void DisablePlayerMovement()
    {
        _player.GetComponent<BasicMovement>().enabled = false;
    }

    public void RestorePlayerMovement()
    {
        _player.GetComponent<BasicMovement>().enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "player")
        {
            _player = other.gameObject;
            EventManager.TriggerCutscene();
        }
    }
}
