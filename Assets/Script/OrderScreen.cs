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

    private void CreateRandomOrder()
    {
        BaseIndexRandom = Random.Range(0, BaseType.Length);
        SyrupIndexRandom = Random.Range(0, SyrupType.Length);
        DecorIndexRandom = Random.Range(0, DecorType.Length);


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        PlateContainer plate = other.GetComponent<PlateContainer>();
        if (plate != null)
        {
            currentPlate = plate;
            plate.ShowFinalOrder();
        }
    }
}
