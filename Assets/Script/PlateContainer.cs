using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateContainer : MonoBehaviour
{
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

    private string categoryName;
    private bool isBaseAdded = false;
    private bool isSyrupAdded = false;
    private bool isDecorAdded = false;

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
    public bool MarkCategoryAsAdded(string category)
    {
        switch (category)
        {
            case "Base":
                if (!isBaseAdded)
                {
                    isBaseAdded = true;
                    Debug.Log($"Base category marked as added on plate: {gameObject.name}");
                    return true;
                }
                Debug.LogWarning($"Base category already marked as added on plate: {gameObject.name}");
                return false;
            case "Syrup":
                if (!isSyrupAdded)
                {
                    isSyrupAdded = true;
                    Debug.Log($"Syrup category marked as added on plate: {gameObject.name}");
                    return true;
                }
                Debug.LogWarning($"Syrup category already marked as added on plate: {gameObject.name}");
                return false;
            case "Decor":
                if (!isDecorAdded)
                {
                    isDecorAdded = true;
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


    /*
   
    [Header("Stored Ingredients")]
    [SerializeField] private BaseType baseType = BaseType.None;
    [SerializeField] private SyrupType syrupType = SyrupType.None;
    [SerializeField] private DecorType decorType = DecorType.None;

    // Public properties to get the current types
    public BaseType CurrentBaseType => baseType;
    public SyrupType CurrentSyrupType => syrupType;
    public DecorType CurrentDecorType => decorType;


    

    // --- Ingredient Adding Methods ---
    public bool TryAddBase(BaseType type)
    {
        if (baseType == BaseType.None)
        {
            baseType = type;
            Debug.Log($"Added Base: {type}");
            return true;
        }
        Debug.Log("Base already present.");
        return false;
    }

    public bool TryAddSyrup(SyrupType type)
    {
        if (syrupType == SyrupType.None)
        {
            syrupType = type;
            Debug.Log($"Added Syrup: {type}");
            return true;
        }
        else if (syrupType != SyrupType.None)
        {
            Debug.Log("Syrup already present.");
        }
        return false;
    }

    public bool TryAddDecor(DecorType type)
    {
        if (decorType == DecorType.None) // Decor needs a base
        {
            decorType = type;
            Debug.Log($"Added Decor: {type}");
            return true;
        }
        else if (decorType != DecorType.None)
        {
            Debug.Log("Decor already present.");
        }
        return false;
    }

    */

}