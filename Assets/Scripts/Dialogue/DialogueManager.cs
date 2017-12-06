using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private Queue<string> sentences;
    Dialogue dialogue;
	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
        dialogue = GetComponent<Dialogue>();
        foreach (string s in dialogue.sentences)
        {
            sentences.Enqueue(s);
        }
    }

    // display the next sentence from the speaker
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) return;

        if (SceneManager.GetActiveScene().name == "BossTheme") {

            FindObjectOfType<DialogueTrigger>().WriteText(dialogue.name, FindObjectOfType<Dialogue>().sentences[3]);
            FindObjectOfType<AudioController>().play("ItSeems");

        }
        else {

        FindObjectOfType<DialogueTrigger>().WriteText(dialogue.name, sentences.Dequeue());
        }


        switch (Level.level) {

            case 2:
                FindObjectOfType<AudioController>().play("YourIgnorance");
                break;

            case 3:
                FindObjectOfType<AudioController>().play("FortuneSmiles");

                break;

            case 4:
               
                FindObjectOfType<AudioController>().play("ItSeems");

                break;

           
                

            default:
                break;
        }
    }
}
