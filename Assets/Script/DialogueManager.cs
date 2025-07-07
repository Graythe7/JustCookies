using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Dialogue Manager handles queues and showing sentences 
 */


public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences;

    //use singleton 
    public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        //get the sentences assigned in DialogueTrigger inspector 
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            Debug.Log("sentence assigned in the new queue");
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //check if the sentences queue is empty or not 
        if(sentences.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
    }

    private void EndOfDialogue()
    {
        Debug.Log("Reached end of sentences queue, End of Dialogues");
    }

}
