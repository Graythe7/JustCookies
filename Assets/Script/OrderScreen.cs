using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScreen : MonoBehaviour
{
    private PlateContainer currentPlate;

    public GameObject[] BaseType;
    public GameObject[] SyrupType;
    public GameObject[] DecorType;

    private int BaseIndexRandom;
    private int SyrupIndexRandom;
    private int DecorIndexRandom;


    private void Start()
    {
        CreateRandomOrder();
    }

    public void CreateRandomOrder()
    {
        BaseIndexRandom = Random.Range(0, BaseType.Length);
        SyrupIndexRandom = Random.Range(0, SyrupType.Length);
        DecorIndexRandom = Random.Range(0, DecorType.Length);

        Instantiate(BaseType[BaseIndexRandom], transform.position, Quaternion.identity);
        Instantiate(SyrupType[SyrupIndexRandom], transform.position, Quaternion.identity);
        Instantiate(DecorType[DecorIndexRandom], transform.position, Quaternion.identity);
    }
}
