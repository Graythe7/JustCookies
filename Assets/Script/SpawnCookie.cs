using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCookie : MonoBehaviour
{
    public GameObject prefabObj;
    public string partType; //set this to either 'Base, Syrup, Decor'
    public List<GameObject> cookieParts = new List<GameObject>();


    public enum CakeSlotState
    {
        Empty,
        BaseOnly,
        BaseWithSyrup,
        BaseWithDecor,
        BaseWithSyrupAndDecor
    }

    public CakeSlotState currentState = CakeSlotState.Empty; //default state at start

    //Handle Logic of Cookie Assembly 
    public void SpawnPrefab()
    {
        switch (partType)
        {
            case "Base":
                if (currentState == CakeSlotState.Empty)
                {
                    Spawn("Base");
                    currentState = CakeSlotState.BaseOnly;
                }
                else
                {
                    Debug.Log("Can't place Base again!");
                }
                break;

            case "Syrup":
                if (currentState == CakeSlotState.BaseOnly)
                {
                    Spawn("Syrup");
                    currentState = CakeSlotState.BaseWithSyrup;
                }
                else if (currentState == CakeSlotState.BaseWithDecor)
                {
                    Debug.Log("Can't place Syrup on top of Decor!");
                }
                else
                {
                    Debug.Log("Can't place Syrup yet!");
                }
                break;

            case "Decor":
                if (currentState == CakeSlotState.BaseOnly)
                {
                    Spawn("Decor");
                    currentState = CakeSlotState.BaseWithDecor;
                }
                else if (currentState == CakeSlotState.BaseWithSyrup)
                {
                    Spawn("Decor");
                    currentState = CakeSlotState.BaseWithSyrupAndDecor;
                }
                else
                {
                    Debug.Log("Can't place Decor yet!");
                }
                break;
        }
    }

    //this function actually instantiate the prefab 
    private void Spawn(string type)
    {
        GameObject newPart = Instantiate(prefabObj, transform.position, Quaternion.identity);
        newPart.tag = type; // Useful for checking later if needed
        cookieParts.Add(newPart);
        Debug.Log(type + " spawned.");
    }

    public GameObject GetLastPart()
    {
        if (cookieParts.Count == 0) return null;
        return cookieParts[cookieParts.Count - 1];
    }


}
