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
    public Animator dialogueAnimator;
    public Animator chefGrayAnimator;

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
        dialogueAnimator.SetBool("isOpen", true);
        chefGrayAnimator.SetBool("isInside", true);

        sentences.Clear();

        //get the sentences assigned in DialogueTrigger inspector 
        foreach(string sentence in dialogue.sentences)
        {
            //sentence assigned in the new queue
            sentences.Enqueue(sentence);
            
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
        dialogueText.text = sentence;

        //if player don't wanna wait for the whole typing effect, to make sure the sentence is displayed
        //so we stop it to go to the next sentence 
        StopAllCoroutines();

        //to display sentence char by char -> typing effect
        StartCoroutine(TypeSentence(sentence));
    }

    private float typingSpeed = 0.05f;
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndOfDialogue()
    {
        dialogueAnimator.SetBool("isOpen", false);
        chefGrayAnimator.SetBool("isInside", false);
    }

}
