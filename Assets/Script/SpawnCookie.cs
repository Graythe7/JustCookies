using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCookie : MonoBehaviour
{
    public GameObject prefabObj;

    public List<GameObject> cookieParts = new List<GameObject>();
    //private bool prefabObjHasSpawned = false;


    public void SpawnPrefab()
    {
        /*
        if (!prefabObjHasSpawned)
        {
            Instantiate(prefabObj, transform.position, Quaternion.identity);
            prefabObjHasSpawned = true;
        }
        */
        GameObject lastPart = GetLastPart();
        //Debug.Log("LastPart:" + lastPart.name);

        if (lastPart == null || lastPart.tag != prefabObj.tag)
        {
            GameObject newPart = Instantiate(prefabObj, transform.position, Quaternion.identity);
            cookieParts.Add(newPart);
            //Debug.Log("cookieParts.Count: " + cookieParts.Count);
        }
    }

    public GameObject GetLastPart()
    {
        if (cookieParts.Count == 0) return null;
        return cookieParts[cookieParts.Count - 1];
    }


}
