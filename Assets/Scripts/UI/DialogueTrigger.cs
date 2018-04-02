using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : DialogueManager{


    public Sentence[] sentences;
    private DialogueManager _dm;
	// Use this for initialization
	public void TriggerDialogue()
    {
        _dm = FindObjectOfType<DialogueManager>();
        _dm.StartDialogue(sentences);
    }

    public void Continue()
    {
        _dm = FindObjectOfType<DialogueManager>();
        _dm.DisplaySentence();
    }
    
}
