using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScreen : MonoBehaviour
{

    public GameObject[] BaseType;
    public GameObject[] SyrupType;
    public GameObject[] DecorType;

    private int BaseIndexRandom;
    private int SyrupIndexRandom;
    private int DecorIndexRandom;

    public SpriteRenderer winStateMonitor;

    private List<GameObject> currentVisuals = new List<GameObject>();

    private void Start()
    {
        winStateMonitor.gameObject.SetActive(false);
    }

    public void CreateRandomOrder()
    {
        // Clear previous visuals
        ClearVisuals();

        if (GameManager.Instance.hasGameWin)
        {
            winStateMonitor.gameObject.SetActive(true);
        }
        else
        {
            BaseIndexRandom = Random.Range(0, BaseType.Length);
            SyrupIndexRandom = Random.Range(0, SyrupType.Length);
            DecorIndexRandom = Random.Range(0, DecorType.Length);

            // Instantiate and add to visuals
            AddToVisuals(BaseType[BaseIndexRandom]);
            AddToVisuals(SyrupType[SyrupIndexRandom]);
            AddToVisuals(DecorType[DecorIndexRandom]);
        }

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
