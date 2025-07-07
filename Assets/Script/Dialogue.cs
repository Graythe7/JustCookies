using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * this class is created as an object we can pass to DialogueManager whenever we want to start a 
 * new dialogue i the game.
 * Therefore this class will contain all the information we would need relating to the dialogue
*/

[System.Serializable]
public class Dialogue
{
    //the sentences we want to load in our queue
    [TextArea(3,10)]
    public string[] sentences;
    
}
