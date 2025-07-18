using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    public GameObject deliveryBoxPrefab;
    public OrderScreen newOrderScreen;
    public Transform trashSpot;
    public Transform boxDeliveryEnd;
    public Transform newBoxSpawnPos;

    //Sprites for DeliveryScreen 
    public Sprite emptyLineIcon;
    public Sprite successIcon;
    public Sprite failIcon;
    public SpriteRenderer[] currIcons; //access the current icon we are updating
    public SpriteRenderer deliveryTruck;
    private int iconIndexCounter = 0; //to keep track of orders
    private int wrongOrderTracker = 0; //to know if player has lost or not

    private void Start()
    {
        iconIndexCounter = 0;
        wrongOrderTracker = 0;

        foreach (var icon in currIcons)
        {
            icon.gameObject.SetActive(true);
        }

        deliveryTruck.gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //When plate enters the zone, compare plateOrder and screenOrder
        PlateContainer plate = other.GetComponent<PlateContainer>();
        if (plate != null)
        {
            bool isMatch = GameManager.Instance.MatchOrder(plate);

            //Update the icon on deliveryScreen seperately
            UpdateDeliveryScreen(iconIndexCounter, isMatch);

            if (isMatch)
            {
                //Correct Order Delivered!
                Destroy(other.gameObject);

                //Since the Order is correct, a new Box appears and sends out 
                GameObject newBox = Instantiate(deliveryBoxPrefab, newBoxSpawnPos.transform.position, Quaternion.identity);

                if (newBox != null)
                {
                    StartCoroutine(MoveBox(newBox, boxDeliveryEnd.transform.position, 2f));
                }

                newOrderScreen.CreateRandomOrder(); //create new order
            }
            else
            {
                //Try again on the previous order till you get it right
                //Wrong Order!

                //The Old order gotta go to trash
                StartCoroutine(MoveToTrash(other.transform, trashSpot.transform.position, 1f));

                //add one unit to wrong order tracker 
                if(wrongOrderTracker >= 1)
                {
                    //"wrong orders exceeded one! -> lose state activated 

                    // halt game and call gameover method
                    GameManager.Instance.GameOver();

                }
                else
                {
                    wrongOrderTracker++;
                }
            }

            //create a new plate in either scenarios && if !gameOver() 
            GameManager.Instance.SpawnNewPlate();

            //move to next order in DeliveryScreen
            iconIndexCounter++; 
        }

        //Destroy(other.gameObject);
    }

    private IEnumerator MoveBox(GameObject box, Vector3 targetPos, float duration)
    {
        if (box == null) yield break;

        Vector3 startPos = box.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            box.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if(box != null)
        {
            box.transform.position = targetPos;
            Destroy(box);
        }

    } 

    private IEnumerator MoveToTrash(Transform item, Vector3 targetPos, float duration)
    {
        float elapsed = 0f;
        Vector3 startPos = item.position;
        Vector3 startScale = item.localScale;
        Vector3 targetScale = Vector3.zero;

        while (elapsed < duration)
        {
            item.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            item.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        item.position = targetPos;
        item.localScale = targetScale;
        Destroy(item.gameObject);
    }


    private void UpdateDeliveryScreen(int index, bool isMatched)
    {
        if(index < 0 || index >= currIcons.Length)
        {
            Debug.Log("index passed is not bounded, maybe round finished?");
            return; // don't run the rest of the code
        }

        //in this state -> the round is complete ! 
        if(index == currIcons.Length-1) //player completed all orders 
        {
            foreach (var icon in currIcons)
            {
                icon.gameObject.SetActive(false);
            }

            deliveryTruck.gameObject.SetActive(true);

            GameManager.Instance.WinGame();
        }


        if (isMatched)
        {
            currIcons[index].sprite = successIcon;
            currIcons[index].transform.localScale = new Vector3(0.8f, 0.8f, 1f);

        }

        else if (!isMatched)
        {
            currIcons[index].sprite = failIcon;
            currIcons[index].transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        }
    }

}
