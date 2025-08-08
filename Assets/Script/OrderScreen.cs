using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScreen : MonoBehaviour
{
    // ----- Level 1 fields -----
    public GameObject[] BaseType;
    public GameObject[] SyrupType;
    public GameObject[] DecorType;

    // ----- Level 2 fields -----
    public CookieShape[] cookieShapes;

    //the Random items index we'll generate later 
    private int ShapeIndexRandom;
    private int BaseIndexRandom;
    private int SyrupIndexRandom;
    private int DecorIndexRandom;

    //The smiley Face at the end
    public SpriteRenderer winStateMonitor;

    private List<GameObject> currentVisuals = new List<GameObject>();

    private string currentScene;

    private void Start()
    {
        winStateMonitor.gameObject.SetActive(false);
        currentScene = GameManager.Instance.CurrentScene();
    }

    public void CreateRandomOrder()
    {
        // Clear previous visuals
        ClearVisuals();

        if (GameManager.Instance.hasGameWin)
        {
            winStateMonitor.gameObject.SetActive(true);
            return;
        }
        if(currentScene == "Level-1")
        {
            GenerateLevel1Order();
        }else if(currentScene == "Level-2")
        {
            GenerateLevel2Order();
        }

    }

    private void GenerateLevel1Order()
    {
        BaseIndexRandom = Random.Range(0, BaseType.Length);
        SyrupIndexRandom = Random.Range(0, SyrupType.Length);
        DecorIndexRandom = Random.Range(0, DecorType.Length);

        // Instantiate and add to visuals
        AddToVisuals(BaseType[BaseIndexRandom]);
        AddToVisuals(SyrupType[SyrupIndexRandom]);
        AddToVisuals(DecorType[DecorIndexRandom]);
    }

    private void GenerateLevel2Order()
    {
        ShapeIndexRandom = Random.Range(0, cookieShapes.Length);
        CookieShape currentShape = cookieShapes[ShapeIndexRandom];

        // Pick random indices for variants
        BaseIndexRandom = Random.Range(0, currentShape.baseVariants.Length);
        SyrupIndexRandom = Random.Range(0, currentShape.syrupVariants.Length);
        DecorIndexRandom = Random.Range(0, currentShape.decorVariants.Length);

        // Add visuals
        AddToVisuals(currentShape.shapePrefab);
        AddToVisuals(currentShape.baseVariants[BaseIndexRandom]);
        AddToVisuals(currentShape.syrupVariants[SyrupIndexRandom]);
        AddToVisuals(currentShape.decorVariants[DecorIndexRandom]);
    }

    //in level-2 we need to compare the shape first -> to be able to compare the rest 
    public int CurrentShapeOnScreen()
    {
        return (ShapeIndexRandom);
    }

    public (int baseIndex, int syrupIndex, int decorIndex) CurrentOrderOnScreen()
    {
        return (BaseIndexRandom, SyrupIndexRandom, DecorIndexRandom);
    }

    private void AddToVisuals(GameObject prefab)
    {
        GameObject newIngredient = Instantiate(prefab, transform.position, Quaternion.identity);
        newIngredient.transform.localScale *= 2f;
        currentVisuals.Add(newIngredient);
    }

    private void ClearVisuals()
    {
        foreach (GameObject obj in currentVisuals)
        {
            if (obj != null)
                Destroy(obj);
        }
        currentVisuals.Clear();
    }


}
