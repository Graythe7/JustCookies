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

    public bool AddBase(BaseType type)
    {
        if (baseType == BaseType.None)
        {
            baseType = type;
            return true;
        }
        return false;
    }

    public bool AddSyrup(SyrupType type)
    {
        if (syrupType == SyrupType.None)
        {
            syrupType = type;
            return true;
        }
        return false;
    }

    public bool AddDecor(DecorType type)
    {
        if (decorType == DecorType.None)
        {
            decorType = type;
            return true;
        }
        return false;
    }


}
