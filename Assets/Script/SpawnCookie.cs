using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCookie : MonoBehaviour
{
    private PlateContainer currentPlate; // The plate we are currently interacting with

    public GameObject[] ingredientType;

    private string counterCategory;


    private void Awake()
    {
        counterCategory = gameObject.tag;
    }

    //Adding data to plate: Category, Typee of ingredient 
    public void AddDataToPlate()
    {
        if (currentPlate != null)
        {
            currentPlate.CategoryData(counterCategory);
        }
    }

    // Connect to Button to add visuals 
    public void SpawnIngredient(int ingredientIndex)
    {
        if (currentPlate != null)
        {
            // Check if the ingredientIndex is within the bounds of the array
            if (ingredientIndex >= 0 && ingredientIndex < ingredientType.Length)
            {
                Instantiate(ingredientType[ingredientIndex], currentPlate.transform.position, Quaternion.identity, currentPlate.transform);
                AddDataToPlate();
            }
        }else if (currentPlate == null)
        {
            Debug.LogWarning("No plate detected to spawn ingredient on.");
        }

    }


    // Plate Detection 
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlateContainer plate = other.GetComponent<PlateContainer>();
        if (plate != null)
        {
            currentPlate = plate;
            Debug.Log($"Plate {plate.name} entered spawner {gameObject.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentPlate != null)
        {
            if (other.gameObject == currentPlate.gameObject)
            {
                Debug.Log($"Plate {currentPlate.name} exited spawner {gameObject.name}");
                currentPlate = null; // Clear the reference only if it's the actual plate leaving
            }
        }
       
    }
}