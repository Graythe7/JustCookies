using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCookie : MonoBehaviour
{
    private PlateContainer currentPlate; // The plate we are currently interacting with

    public AssemblyMachine assemblyMachine;
    public IngredientAnimation ingredientAnim;
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
                //play activation animation -> both machine and ingredient drop
                assemblyMachine.ActivateAnimation(true);
                ingredientAnim.ActivateAnimation(ingredientIndex, true);

                // Cache the plate and start the coroutine with it
                StartCoroutine(DelayedSpawn(ingredientIndex, currentPlate));

                //Mark the category as added on the current plate
                currentPlate.MarkCategoryAsAdded(counterCategory, ingredientIndex);
            }
            else if (currentPlate.HasCategoryBeenAdded(counterCategory))
            {
                //play can't spawn animation
                assemblyMachine.CantSpawnAnimation(true);
            }

        }
        else if (currentPlate == null)
        {
            //play can't spawn animation
            assemblyMachine.CantSpawnAnimation(true);
        }

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

    //add 1f delay to let the drop prefab animation to play first
    private IEnumerator DelayedSpawn(int ingredientIndex, PlateContainer targetPlate)
    {
        yield return new WaitForSeconds(1f);
        AddPrefab(ingredientType[ingredientIndex], targetPlate);
    }

    private void AddPrefab(GameObject prefab, PlateContainer targetPlate)
    {
        if (targetPlate == null) return; // Safety check

        Vector3 spawnPosition = targetPlate.transform.position + new Vector3(0f, 0.3f, 0f);
        GameObject newIngredient = Instantiate(prefab, spawnPosition, Quaternion.identity, targetPlate.transform);

        //the originalScale is for the distorbed ratio of the child (ingredient), not sure if I still need it 
        Vector3 originalScale = newIngredient.transform.localScale;
        newIngredient.transform.localScale = new Vector3(originalScale.x * 1f, originalScale.y * 1f, originalScale.z);
    }
}