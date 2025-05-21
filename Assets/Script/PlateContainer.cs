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

}