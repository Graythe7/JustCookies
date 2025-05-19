using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public LayerMask targetLayer;
   
    public void MoveForward()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Check if the GameObject's layer is included in the targetLayer LayerMask
            if ((targetLayer.value & (1 << obj.layer)) != 0)
            {
                obj.transform.position += new Vector3(3f, 0, 0);
            }
                
        }
    }

    public void MoveBackward()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if ((targetLayer.value & (1 << obj.layer)) != 0)
            {
                obj.transform.position += new Vector3(-3f, 0, 0);
            }

        }
    }
}
