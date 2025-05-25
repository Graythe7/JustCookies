using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public OrderScreen orderScreen;
    public Transform plateSpawnSpot;
    public GameObject platePrefab;

    public static GameManager Instance; // Singleton
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }


    private void Start()
    {
        orderScreen.CreateRandomOrder();
    }

    public void SpawnNewPlate()
    {
        Instantiate(platePrefab, plateSpawnSpot.transform.position, Quaternion.identity);
    }

    public bool MatchOrder(PlateContainer plate)
    {
        if (plate != null && orderScreen != null)
        {
            // Get the order from plate
            var (plateBase, plateSyrup, plateDecor) = plate.CurrentOrderOnPlate();

            //Get the order from screen
            var (screenBase, screenSyrup, screenDecor) = orderScreen.CurrentOrderOnScreen();

            // Compare them, if even one item is false -> all Fail
            return plateBase == screenBase &&
                   plateSyrup == screenSyrup &&
                   plateDecor == screenDecor;

        }

        Debug.LogWarning("Plate or OrderScreen reference missing.");
        return false;
    }

}
