using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCookie : MonoBehaviour
{
    public GameObject CookiePrefab;
    public GameObject SyrupPrefab;

    private bool cookieBaseHasSpawned = false;

    public void SpawnCookieBase()
    {
        if (!cookieBaseHasSpawned)
        {
            Instantiate(CookiePrefab, transform.position, Quaternion.identity);
            cookieBaseHasSpawned = true;
        }
           
    }

    public void SpawnSyrup()
    {
        Instantiate(SyrupPrefab, transform.position, Quaternion.identity);
    }
}
