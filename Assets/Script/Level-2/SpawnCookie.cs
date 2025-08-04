 using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnCookie : MonoBehaviour
{
    private PlateContainer currentPlate; // The plate we are currently interacting with

    public AssemblyMachine assemblyMachine;
    public IngredientAnimation ingredientAnim; //passing the Drop Animation 

    [Header("Shapes Data")]
    public CookieShape[] cookieShapes;        // Assign in inspector

    private string counterCategory;

    //required order of ingredients in level-2
    private readonly string[] requiredOrder = { "Shape", "Base", "Syrup", "Decor" };

    private void Awake()
    {
        counterCategory = gameObject.tag;
        
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


        if (category == "Shape")
        {
            currentPlate.currentCookieShape = cookieShapes[ingredientIndex];
        }

        // --- NEW: Check the shape on the PlateContainer ---
        if (currentPlate.currentCookieShape == null)
        {
            assemblyMachine.CantSpawnAnimation(true);
            Debug.LogError("A cookie shape must be chosen first.");
            return;
        }

        // Mark as added
        currentPlate.MarkCategoryAsAdded(category, ingredientIndex);


        //Actually Spawning the ingredient (this function connected to UI Buttons)
        StartCoroutine(DelayedSpawn(ingredientIndex, category,currentPlate));

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
    private IEnumerator DelayedSpawn(int ingredientIndex, string category, PlateContainer targetPlate)
    {
        yield return new WaitForSeconds(1f); // wait for drop animation 

        if (targetPlate.currentCookieShape == null)
        {
            Debug.LogError("Spawn Error: A cookie shape must be added first.");
            yield break; // Exit the coroutine immediately to prevent the crash
        }

        GameObject prefabToSpawn = null;

        switch (category)
        {
            case "Shape":
                prefabToSpawn = targetPlate.currentCookieShape.shapePrefab;
                Debug.Log($"Shape {targetPlate.currentCookieShape.name} has been added to plate");
                break;

            case "Base":
                prefabToSpawn = targetPlate.currentCookieShape.baseVariants[ingredientIndex];
                Debug.Log($"Base {prefabToSpawn.name} has been added to plate");
                break;

            case "Syrup":
                prefabToSpawn = targetPlate.currentCookieShape.syrupVariants[ingredientIndex];
                Debug.Log($"Syrup {prefabToSpawn.name} has been added to plate");
                break;

            case "Decor":
                prefabToSpawn = targetPlate.currentCookieShape.decorVariants[ingredientIndex];
                Debug.Log($"Decor {prefabToSpawn.name} has been added to plate");
                break;
        }

        if (prefabToSpawn != null)
        {
            AddPrefab(prefabToSpawn, targetPlate);
        }

    }

    private void AddPrefab(GameObject prefab, PlateContainer targetPlate)
    {
        if (targetPlate == null) return;

        if (prefab == targetPlate.currentCookieShape.shapePrefab)
        {
            Transform plateTransform = targetPlate.transform;
            Vector3 spawnPosition = plateTransform.position + new Vector3(0f, 0.3f, 0f);
            GameObject shapeObj = Instantiate(prefab, spawnPosition, Quaternion.identity, plateTransform);
            targetPlate.currentShapeObject = shapeObj;
        }
        else
        {
            if (targetPlate.currentShapeObject == null) return;
            Transform shapeParent = targetPlate.currentShapeObject.transform;

            GameObject ingredientObj = Instantiate(prefab, shapeParent);
            ingredientObj.transform.localPosition = Vector3.zero;
            ingredientObj.transform.localRotation = Quaternion.identity;
            ingredientObj.transform.localScale = Vector3.one;
        }
    }

}