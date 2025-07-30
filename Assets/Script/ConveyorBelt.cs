using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    private Transform currentPlate;  // assign the new plate to this when spawned
    public float movementDist = 3.0f;
    private Vector3 movementDistant;

    private void Awake()
    {
        movementDistant = new Vector3(movementDist, 0, 0);
    }

    //get the ref to latest plate spawned in game manager 
    public void SetCurrentPlate(Transform plate)
    {
        //"latest plate assigned to coneyorBelt"
        currentPlate = plate;
    }


    public void MoveForward()
    {
        if (currentPlate != null)
            StartCoroutine(MoveObjectSmoothly(currentPlate, movementDistant, 0.3f));
    }

    public void MoveBackward()
    {
        if (currentPlate != null)
        {
            float newX = currentPlate.position.x - movementDistant.x;

            // Block movement if it would go beyond x = -5
            if (newX < -8.0f)
            {
                Debug.Log("Can't move further left. Plate would go beyond -5 on x-axis.");
                return;
            }

            StartCoroutine(MoveObjectSmoothly(currentPlate, -movementDistant, 0.3f));
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

            yield return null;
        }

        obj.position = end;
    }

}
