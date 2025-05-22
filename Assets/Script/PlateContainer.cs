using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateContainer : MonoBehaviour
{
    /*
    public enum BaseType
    {
        None,
        One,
        Two,
        Three
    }

    public enum SyrupType
    {
        None,
        One,
        Two,
        Three
    }

    public enum DecorType
    {
        None,
        One,
        Two,
        Three
    }
    */

    // Flags to track if a category has been added
    private bool isBaseAdded = false;
    private bool isSyrupAdded = false;
    private bool isDecorAdded = false;

    // Variables to store the SPECIFIC index of the ingredient added for each category
    private int baseTypeIndex = -1;
    private int syrupTypeIndex = -1;
    private int decorTypeIndex = -1;

    //Pass to SpawnCookie to whether spawn ingredient or now/ prevent duplication
    public bool HasCategoryBeenAdded(string category)
    {
        switch (category)
        {
            case "Base":
                return isBaseAdded;
            case "Syrup":
                return isSyrupAdded;
            case "Decor":
                return isDecorAdded;
            default:
                Debug.LogWarning($"Unknown category '{category}' queried for plate {gameObject.name}.");
                return false; 
        }
    }

    //Get the category name from counter (spawnCookie) to update plate's data 
    public bool MarkCategoryAsAdded(string category, int ingredientIndex)
    {
        switch (category)
        {
            case "Base":
                if (!isBaseAdded)
                {
                    isBaseAdded = true;
                    baseTypeIndex = ingredientIndex;
                    Debug.Log($"Base category marked as added on plate: {gameObject.name}");
                    return true;
                }
                Debug.LogWarning($"Base category already marked as added on plate: {gameObject.name}");
                return false;
            case "Syrup":
                if (!isSyrupAdded)
                {
                    isSyrupAdded = true;
                    syrupTypeIndex = ingredientIndex;
                    Debug.Log($"Syrup category marked as added on plate: {gameObject.name}");
                    return true;
                }
                Debug.LogWarning($"Syrup category already marked as added on plate: {gameObject.name}");
                return false;
            case "Decor":
                if (!isDecorAdded)
                {
                    isDecorAdded = true;
                    decorTypeIndex = ingredientIndex;
                    Debug.Log($"Decor category marked as added on plate: {gameObject.name}");
                    return true;
                }
                Debug.LogWarning($"Decor category already marked as added on plate: {gameObject.name}");
                return false;
            default:
                Debug.LogWarning($"Unknown category '{category}' attempted to be marked on plate {gameObject.name}.");
                return false;
        }
    }


    public void ShowFinalOrder()
    {
        Debug.Log("Base added:" + isBaseAdded + "type of Base:" + baseTypeIndex);
        Debug.Log("Syrup added:" + isSyrupAdded + "type of Syrup:" + syrupTypeIndex);
        Debug.Log("Decor added:" + isDecorAdded + "type of Decor:" + decorTypeIndex);
    }

}