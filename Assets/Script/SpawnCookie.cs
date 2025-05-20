using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCookie : MonoBehaviour
{
    private PlateContainer currentPlate; // The plate we are currently interacting with

    public GameObject prefabObj; // The specific prefab for this spawner (e.g., BasePrefab, SyrupPrefab, DecorPrefab)

    // Using enums directly is safer and less error-prone than strings
    public PlateContainer.BaseType baseTypeToSpawn; // Set in inspector if this spawns a base
    public PlateContainer.SyrupType syrupTypeToSpawn; // Set in inspector if this spawns syrup
    public PlateContainer.DecorType decorTypeToSpawn; // Set in inspector if this spawns decor

    public string partType; // Set this in inspector to "Base", "Syrup", or "Decor" to know what this spawner handles

    private List<GameObject> spawnedParts = new List<GameObject>(); // Keep track of spawned visual parts

    // Public method to be called when interaction happens (e.g., button click, player enters trigger)
    public void AttemptToSpawnIngredient()
    {
        if (currentPlate == null)
        {
            Debug.LogWarning("No plate detected to spawn on.");
            return;
        }

        bool ingredientAddedSuccessfully = false;

        switch (partType)
        {
            case "Base":
                ingredientAddedSuccessfully = currentPlate.TryAddBase(baseTypeToSpawn);
                break;
            case "Syrup":
                ingredientAddedSuccessfully = currentPlate.TryAddSyrup(syrupTypeToSpawn);
                break;
            case "Decor":
                ingredientAddedSuccessfully = currentPlate.TryAddDecor(decorTypeToSpawn);
                break;
            default:
                Debug.LogError($"Unknown partType: {partType} on {gameObject.name}");
                break;
        }

        if (ingredientAddedSuccessfully)
        {
            SpawnVisualPrefab();
            Debug.Log($"{partType} {prefabObj.name} spawned on plate. New plate state: {currentPlate.CurrentState}");
        }
        else
        {
            Debug.Log($"Failed to add {partType} to plate. Plate state: {currentPlate.CurrentState}");
        }
    }

    // This function actually instantiates the prefab
    public void SpawnVisualPrefab()
    { 
        GameObject newPart = Instantiate(prefabObj, currentPlate.transform.position, Quaternion.identity, currentPlate.transform);
        newPart.name = $"{partType}_{prefabObj.name}"; // Name for clarity in hierarchy
        spawnedParts.Add(newPart);
    }

    /*

    // You might want a way to clear spawned visuals from this spawner
    public void ClearSpawnedVisuals()
    {
        foreach (GameObject part in spawnedParts)
        {
            Destroy(part);
        }
        spawnedParts.Clear();
    }
    */ 

    // --- Plate Detection (assuming your plate is a trigger) ---
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
        if (other.GetComponent<PlateContainer>() == currentPlate)
        {
            Debug.Log($"Plate {currentPlate.name} exited spawner {gameObject.name}");
            currentPlate = null;
        }
    }
}