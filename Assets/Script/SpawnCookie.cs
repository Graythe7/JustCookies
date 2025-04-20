using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCookie : MonoBehaviour
{
    public GameObject prefabObj;
    private bool prefabObjHasSpawned = false;

    public void SpawnPrefab()
    {
        if (!prefabObjHasSpawned)
        {
            Instantiate(prefabObj, transform.position, Quaternion.identity);
            prefabObjHasSpawned = true;
        }
    }
}
