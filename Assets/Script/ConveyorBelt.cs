using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public LayerMask targetLayer;
    private Vector3 movementDistant = new Vector3(3.0f, 0 ,0);
   
    public void MoveForward()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Check if the GameObject's layer is included in the targetLayer LayerMask
            if ((targetLayer.value & (1 << obj.layer)) != 0)
            {
                StartCoroutine(MoveObjectSmoothly(obj.transform, movementDistant, 0.3f));

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
                StartCoroutine(MoveObjectSmoothly(obj.transform, -movementDistant, 0.3f));

            }

        }
    }

    //plate moves smoothly 
    public IEnumerator MoveObjectSmoothly(Transform obj, Vector3 offset, float duration)
    {
        Vector3 start = obj.position;
        Vector3 end = start + offset;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            if (obj != null) //check that obj still exists
            {
                obj.position = Vector3.Lerp(start, end, t);
            }
            else
            {
                yield break; // exit coroutine safely
            }
        }

        obj.position = end;
    }

}
