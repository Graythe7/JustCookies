using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCookie : MonoBehaviour
{
    private PlateContainer currentPlate; // The plate we are currently interacting with

    public AssemblyMachine assemblyMachine;
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
                assemblyMachine.ActivateAnimation(true);

                AddPrefab(ingredientType[ingredientIndex]);

                //Mark the category as added on the current plate
                currentPlate.MarkCategoryAsAdded(counterCategory, ingredientIndex);

                //assemblyMachine.ActivateAnimation(false);
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

    private void AddPrefab(GameObject prefab)
    {
        Vector3 spawnPosition = currentPlate.transform.position + new Vector3(0f, 0.3f, 0f);
        GameObject newIngredient = Instantiate(prefab, spawnPosition, Quaternion.identity, currentPlate.transform);

        //the originalScale is for the distorbed ratio of the child (ingredient), not sure if I still need it 
        Vector3 originalScale = newIngredient.transform.localScale;
        newIngredient.transform.localScale = new Vector3(originalScale.x * 1f, originalScale.y * 1f, originalScale.z);
    }


    // Plate Detection 
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlateContainer plate = other.GetComponent<PlateContainer>();
        if (plate != null)
        {
            //current plate assigned
            currentPlate = plate;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentPlate != null)
        {
            if (other.gameObject == currentPlate.gameObject)
            {
                //Plate exited spawner
                currentPlate = null; 
            }
        }
       
    }
}