 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnCookie : MonoBehaviour
{
    private PlateContainer currentPlate; // The plate we are currently interacting with

    public AssemblyMachine assemblyMachine;
    public IngredientAnimation ingredientAnim; //passing the Drop Animation 
    public GameObject[] ingredientType;

    private string counterCategory;

    //required order of ingredients in  each level 
    [SerializeField] private string[] requiredOrderLevel1 = { "Base", "Syrup", "Decor" };
    [SerializeField] private string[] requiredOrderLevel2 = { "Shape", "Base", "Syrup", "Decor" };

    private string[] currentRequiredOrder;



    private void Awake()
    {
        counterCategory = gameObject.tag;

        // Get the active scene name
        string sceneName = SceneManager.GetActiveScene().name;

        // Choose required order based on scene
        if (sceneName == "Level-1")
        {
            currentRequiredOrder = requiredOrderLevel1;
        }
        else if (sceneName == "Level-2")
        {
            currentRequiredOrder = requiredOrderLevel2;
        }
    }


    // Connect to Button to add visuals 
    public void SpawnIngredient(int ingredientIndex)
    {
        //if no plate detected, skip this method completely
        if (currentPlate == null)
        {
            assemblyMachine.CantSpawnAnimation(true);
            return;
        }

        //prevent Skipping a category
        //This is either "Base", "Syrup", or "Decor"
        string category = counterCategory; 

        // Find the current category index in order
        int currentIndex = System.Array.IndexOf(currentRequiredOrder, category);

        // Check if all previous categories are already added
        for (int i = 0; i < currentIndex; i++)
        {
            string requiredCategory = currentRequiredOrder[i];
            if (!currentPlate.HasCategoryBeenAdded(requiredCategory))
            {
                Debug.LogWarning($"Cannot add {category} before {requiredCategory} is added.");
                assemblyMachine.CantSpawnAnimation(true);
                return;
            }
        }


        // Prevent duplicate placement
        if (currentPlate.HasCategoryBeenAdded(category))
        {
            assemblyMachine.CantSpawnAnimation(true);
            return;
        }

        // Proceed with spawn
        assemblyMachine.ActivateAnimation(true);
        ingredientAnim.ActivateAnimation(ingredientIndex, true);
        StartCoroutine(DelayedSpawn(ingredientIndex, currentPlate));

        // Mark as added
        currentPlate.MarkCategoryAsAdded(category, ingredientIndex);


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
        if (currentPlate != null && other.gameObject == currentPlate.gameObject)
        {
            currentPlate = null;
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