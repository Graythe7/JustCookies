using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private OrderScreen orderScreen;

    public Transform plateSpawnSpot;

    private void Start()
    {
        orderScreen.CreateRandomOrder();
    }

    private void SpawnNewPlate()
    {
        //based on the Fail/Accepeted Order -> spawn new plate 
    }



}
