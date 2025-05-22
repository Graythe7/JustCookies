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


    // Connect to Button to add visuals 
    public void SpawnIngredient(int ingredientIndex)
    {
        if (currentPlate != null)
        {
            // Check if the ingredientIndex is within the bounds of the array
            if (ingredientIndex >= 0 && ingredientIndex < ingredientType.Length && !currentPlate.HasCategoryBeenAdded(counterCategory))
            {
                Instantiate(ingredientType[ingredientIndex], currentPlate.transform.position, Quaternion.identity, currentPlate.transform);
                //Mark the category as added on the current plate
                currentPlate.MarkCategoryAsAdded(counterCategory, ingredientIndex);
            }
            else if (currentPlate.HasCategoryBeenAdded(counterCategory))
            {
                Debug.LogWarning("Ingredient Already added to plate.");
            }

        }
        else if (currentPlate == null)
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentPlate != null)
        {
            if (other.gameObject == currentPlate.gameObject)
            {
                Debug.Log($"Plate exited spawner");
            }
        }
       
    }
}