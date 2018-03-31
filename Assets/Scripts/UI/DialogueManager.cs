using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    


    private Queue<Sentence> sentences;

    public Text nameText;
    public Text dialogueText;
    public GameObject TextBox;
    [Range(1,10)]
    public float type_speed;
    private float last_type;
    private float type_time;


	// Use this for initialization
	void Start () {
        sentences = new Queue<Sentence>();
	}
	
	public void StartDialogue(Sentence[] dialogue)
    {
        Debug.Log("Starting Dialogue with " + dialogue[0].name);
        TextBox.SetActive(true); 
        sentences.Clear();

        foreach( Sentence s in dialogue)
        {
            sentences.Enqueue(s);
        }
        DisplaySentence();
        return;
    }

    public void DisplaySentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            Sentence sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence.text));
            nameText.text = sentence.name;
        }
    }

    public IEnumerator TypeSentence(string text)
    {
        dialogueText.text = "";
        type_time = 1 / (type_speed * 10);
        foreach(char letter in text.ToCharArray())
        {
            skip:
                yield return null;  

            if (Time.time - last_type > type_time)
            {
                dialogueText.text += letter;
                last_type = Time.time;
            }
            else goto skip;
            
        }
    }

    public void EndDialogue()
    {
        Debug.Log("...");
        TextBox.SetActive(false);
    }
}
