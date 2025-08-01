 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnCookie : MonoBehaviour
{
    private PlateContainer currentPlate; // The plate we are currently interacting with

    public AssemblyMachine assemblyMachine;
    public IngredientAnimation ingredientAnim; //passing the Drop Animation 

    [Header("Shapes Data")]
    public CookieShape[] cookieShapes;        // Assign in inspector
    private CookieShape currentShape;         // The shape chosen for current cookie by player

    private string counterCategory;

    //required order of ingredients in  each level 
    //private string[] requiredOrderLevel1 = { "Base", "Syrup", "Decor" };
    private string[] requiredOrder = { "Shape", "Base", "Syrup", "Decor" };

    private void Awake()
    {
        counterCategory = gameObject.tag;

        // Get the active scene name
        string sceneName = SceneManager.GetActiveScene().name;

        // If we are in Level-1 and haven't chosen a shape yet,
        // automatically use the default Circle shape
        if (sceneName == "Level-1")
        {
            currentShape = cookieShapes[2]; //assign Circle Shape
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
        //This is either "Shape", "Base", "Syrup", or "Decor"
        string category = counterCategory; 

        // Find the current category index in order
        int currentIndex = System.Array.IndexOf(requiredOrder, category);

        // Check if all previous categories are already added
        for (int i = 0; i < currentIndex; i++)
        {
            string requiredCategory = requiredOrder[i];
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

        // Trigger Assembly Machine Animations 
        assemblyMachine.ActivateAnimation(true);
        ingredientAnim.ActivateAnimation(ingredientIndex, true);

        // Mark as added
        currentPlate.MarkCategoryAsAdded(category, ingredientIndex);

        //Actually Spawning the ingredient (this function connected to UI Buttons)
        StartCoroutine(DelayedSpawn(ingredientIndex, category, currentShape,currentPlate));

    }

    // Plate Detection on counter 
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
            //Plate exited spawner
            currentPlate = null;
        }
       
    }

    //add 1f delay to let the drop prefab animation to play first
    private IEnumerator DelayedSpawn(int ingredientIndex, string category, CookieShape currentShape,PlateContainer targetPlate)
    {
        yield return new WaitForSeconds(1f); // wait for drop animation 

        GameObject prefabToSpawn = null;
        

        switch (category)
        {
            case "Shape":
                if (currentShape == null)//since in level-1 we assign shape from very beggining
                {
                    currentShape = cookieShapes[ingredientIndex];
                    this.currentShape = currentShape; // Update the main variable for later stations
                    prefabToSpawn = currentShape.shapePrefab;
                    Debug.Log(cookieShapes[ingredientIndex] + "has been added to plate");
                }
                break;

            case "Base":
                if (currentShape != null)
                {
                    prefabToSpawn = currentShape.baseVariants[ingredientIndex];
                    Debug.Log(currentShape.baseVariants[ingredientIndex] + "has been added to plate");
                }
                    
                break;

            case "Syrup":
                if (currentShape != null)
                {
                    prefabToSpawn = currentShape.syrupVariants[ingredientIndex];
                    Debug.Log(currentShape.syrupVariants[ingredientIndex] + "has been added to plate");
                }
                    
                break;

            case "Decor":
                if (currentShape != null)
                {
                    prefabToSpawn = currentShape.syrupVariants[ingredientIndex];
                    Debug.Log(currentShape.syrupVariants[ingredientIndex] + "has been added to plate");
                }

                break;
        }

        if (prefabToSpawn != null)
            AddPrefab(prefabToSpawn, targetPlate);

    }

    private void AddPrefab(GameObject prefab, PlateContainer targetPlate)
    {
        if (targetPlate == null) return;

        Transform parentTransform = targetPlate.transform;

        // If it's a Shape, spawn directly on the plate
        if (prefab == currentShape.shapePrefab)
        {
            Vector3 spawnPosition = parentTransform.position + new Vector3(0f, 0.3f, 0f);
            GameObject shapeObj = Instantiate(prefab, spawnPosition, Quaternion.identity, parentTransform);

            // Save the shape object in plate
            targetPlate.currentShapeObject = shapeObj;
        }
        else
        {
            // Spawn Base, Syrup, or Decor as a child of the Shape
            Transform shapeParent = targetPlate.currentShapeObject != null
                                    ? targetPlate.currentShapeObject.transform
                                    : parentTransform;

            // Spawn at local origin of shape (adjust if needed)
            GameObject ingredientObj = Instantiate(prefab, shapeParent.position, Quaternion.identity, shapeParent);

            // Reset local position for perfect overlay
            ingredientObj.transform.localPosition = Vector3.zero;
        }
    }

}