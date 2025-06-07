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


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //When plate enters the zone, compare plateOrder and screenOrder
        PlateContainer plate = other.GetComponent<PlateContainer>();
        if (plate != null)
        {
            bool isMatch = GameManager.Instance.MatchOrder(plate);

            //create a new plate in either scenarios
            GameManager.Instance.SpawnNewPlate();

            if (isMatch)
            {
                Debug.Log("Correct Order Delivered!");
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
                Debug.Log("Wrong Order!");

                //The Old order gotta go to trash
                StartCoroutine(MoveToTrash(other.transform, trashSpot.transform.position, 1f));
            }
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


}
