using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //since ChefGray has multiple states which shows different dialogues
    public enum DialogueType
    {
        Intro,
        Win,
        Lose
    }

    //Assign in inspector 
    [System.Serializable]
    public struct DialogueEntry
    {
        public DialogueType type;
        public Dialogue dialogue;
    }

    public DialogueEntry[] dialogues;

    // Trigger the right dialogue based on the current type
    public void TriggerDialogue(DialogueType type)
    {
        Dialogue selected = GetDialogue(type);
        if (selected == null)
        {
            Debug.LogWarning($"No dialogue assigned for type: {type}");
            return;
        }
        DialogueManager.Instance.StartDialogue(selected);
        Debug.Log($"Triggered {type} dialogue.");

    }

    private Dialogue GetDialogue(DialogueType type)
    {
        foreach (var entry in dialogues)
        {
            if (entry.type == type)
                return entry.dialogue;
        }
        return null;
    }
}
