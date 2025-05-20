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

    public enum CakeSlotState //all different states each cookie could have
    {
        Empty,
        BaseOnly,
        BaseWithSyrup,
        BaseWithDecor,
        BaseWithSyrupAndDecor
    }

    [Header("Stored Ingredients")]
    [SerializeField] private BaseType baseType = BaseType.None;
    [SerializeField] private SyrupType syrupType = SyrupType.None;
    [SerializeField] private DecorType decorType = DecorType.None;

    // Public properties to get the current types
    public BaseType CurrentBaseType => baseType;
    public SyrupType CurrentSyrupType => syrupType;
    public DecorType CurrentDecorType => decorType;

    // Use a public getter for the current state
    public CakeSlotState CurrentState { get; private set; } = CakeSlotState.Empty;

    // --- Ingredient Adding Methods ---
    public bool TryAddBase(BaseType type)
    {
        if (baseType == BaseType.None)
        {
            baseType = type;
            UpdateCakeSlotState(); // Update state after adding
            return true;
        }
        Debug.Log("Base already present.");
        return false;
    }

    public bool TryAddSyrup(SyrupType type)
    {
        if (baseType != BaseType.None && syrupType == SyrupType.None) // Syrup needs a base
        {
            syrupType = type;
            UpdateCakeSlotState(); // Update state after adding
            return true;
        }
        else if (baseType == BaseType.None)
        {
            Debug.Log("Cannot add syrup without a base.");
        }
        else if (syrupType != SyrupType.None)
        {
            Debug.Log("Syrup already present.");
        }
        return false;
    }

    public bool TryAddDecor(DecorType type)
    {
        if (baseType != BaseType.None && decorType == DecorType.None) // Decor needs a base
        {
            decorType = type;
            UpdateCakeSlotState(); // Update state after adding
            return true;
        }
        else if (baseType == BaseType.None)
        {
            Debug.Log("Cannot add decor without a base.");
        }
        else if (decorType != DecorType.None)
        {
            Debug.Log("Decor already present.");
        }
        return false;
    }

    // --- State Update Logic ---
    private void UpdateCakeSlotState()
    {
        if (baseType != BaseType.None)
        {
            if (syrupType != SyrupType.None && decorType != DecorType.None)
            {
                CurrentState = CakeSlotState.BaseWithSyrupAndDecor;
            }
            else if (syrupType != SyrupType.None)
            {
                CurrentState = CakeSlotState.BaseWithSyrup;
            }
            else if (decorType != DecorType.None)
            {
                CurrentState = CakeSlotState.BaseWithDecor;
            }
            else
            {
                CurrentState = CakeSlotState.BaseOnly;
            }
        }
        else
        {
            CurrentState = CakeSlotState.Empty;
        }

        Debug.Log($"Plate State Updated to: {CurrentState}");
    }

    // Call UpdateCakeSlotState initially to set the correct state if ingredients are serialized
    private void Awake()
    {
        UpdateCakeSlotState();
    }

    // You might want a way to reset the plate for a new order
    public void ResetPlate()
    {
        baseType = BaseType.None;
        syrupType = SyrupType.None;
        decorType = DecorType.None;
        UpdateCakeSlotState();
        // You might also want to destroy spawned prefabs here
    }
}