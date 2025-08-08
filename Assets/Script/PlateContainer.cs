using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateContainer : MonoBehaviour
{
    /* In terms of compability of this code with both levels 
     * Still works with Level 1 because Shape is never queried or marked in that level
     */

    // The instantiated shape object on the plate in level-2
    // This will store the ScriptableObject for the current shape ---
    public GameObject currentShapeObject;
    public CookieShape currentCookieShape;

    // Flags to track if a category has been added
    private bool isShapeAdded = false;
    private bool isBaseAdded = false;
    private bool isSyrupAdded = false;
    private bool isDecorAdded = false;

    // Variables to store the SPECIFIC index of the ingredient added for each category
    private int shapeTypeIndex = -1;
    private int baseTypeIndex = -1;
    private int syrupTypeIndex = -1;
    private int decorTypeIndex = -1;

    //Pass to SpawnCookie to whether spawn ingredient or now/ prevent duplication
    public bool HasCategoryBeenAdded(string category)
    {
        switch (category)
        {
            case "Shape":
                return isShapeAdded;
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
            case "Shape":
                if (!isShapeAdded)
                {
                    isShapeAdded = true;
                    shapeTypeIndex = ingredientIndex;
                    return true;
                }
                Debug.LogWarning($"Shape category already marked as added on plate: {gameObject.name}");
                return false;

            case "Base":

                //Base category marked as added on plate: {gameObject.name}
                if (!isBaseAdded)
                {
                    isBaseAdded = true;
                    baseTypeIndex = ingredientIndex;
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

    //returns the shape of the plate to compare it later with OrderScreen's Shape
    public int CurrentShapeOnPlate()
    {
        int shapeIndex = shapeTypeIndex;

        return (shapeIndex);
    }

    public (int baseIndex, int syrupIndex, int decorIndex) CurrentOrderOnPlate()
    {
        int baseIndex = baseTypeIndex;
        int syrupIndex = syrupTypeIndex;
        int decorIndex = decorTypeIndex;

        return (baseIndex, syrupIndex, decorIndex);
    }

    /*

    public (int baseIndex, int syrupIndex, int decorIndex) CurrentOrderOnPlate_Level2()
    {
        int baseIndex = baseTypeIndex;   
        int syrupIndex = syrupTypeIndex;
        int decorIndex = decorTypeIndex;

        return (baseIndex, syrupIndex, decorIndex);
    }

    public (int baseIndex, int syrupIndex, int decorIndex) CurrentOrderOnPlate_Level1()
    {
        int baseIndex = baseTypeIndex;
        int syrupIndex = syrupTypeIndex;
        int decorIndex = decorTypeIndex;

        return (baseIndex, syrupIndex, decorIndex);
    }

    */

}