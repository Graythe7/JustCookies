using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScreen : MonoBehaviour
{

    public GameObject[] BaseType;
    public GameObject[] SyrupType;
    public GameObject[] DecorType;

    private int BaseIndexRandom;
    private int SyrupIndexRandom;
    private int DecorIndexRandom;

    private List<GameObject> currentVisuals = new List<GameObject>();

    public void CreateRandomOrder()
    {
        // Clear previous visuals if any
        ClearVisuals();

        BaseIndexRandom = Random.Range(0, BaseType.Length);
        SyrupIndexRandom = Random.Range(0, SyrupType.Length);
        DecorIndexRandom = Random.Range(0, DecorType.Length);

        currentVisuals.Add(Instantiate(BaseType[BaseIndexRandom], transform.position, Quaternion.identity, transform));
        currentVisuals.Add(Instantiate(SyrupType[SyrupIndexRandom], transform.position, Quaternion.identity, transform));
        currentVisuals.Add(Instantiate(DecorType[DecorIndexRandom], transform.position, Quaternion.identity, transform));
    }

    public (int baseIndex, int syrupIndex, int decorIndex) CurrentOrderOnScreen()
    {
        return (BaseIndexRandom, SyrupIndexRandom, DecorIndexRandom);
    }


    private void ClearVisuals()
    {
        foreach (GameObject obj in currentVisuals)
        {
            if (obj != null)
                Destroy(obj);
        }
        currentVisuals.Clear();
    }

}
