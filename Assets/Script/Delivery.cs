using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    public GameObject deliveryBoxPrefab;
    public OrderScreen newOrderScreen;

    private float speed = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject newBox = Instantiate(deliveryBoxPrefab, transform.position, Quaternion.identity);

        if(newBox != null)
        {
            StartCoroutine(MoveBox(newBox));
        }

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
                newOrderScreen.CreateRandomOrder(); //create new order
            }
            else
            {
                //Try again on the previous order till you get it right
                Debug.Log("Wrong Order!");
            }
        }

        Destroy(other.gameObject);
    }

    private IEnumerator MoveBox(GameObject Box)
    {
        while (Box.transform.position.y <= 8 && Box != null)
        {
            if(Box.transform.position.y >= 6)
            {
                Destroy(Box.gameObject);
                break;
            }
            else
            {
                Box.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
    
            yield return null;
        }
    }

}
