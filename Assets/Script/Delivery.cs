using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    public GameObject deliveryBoxPrefab;
    private PlateContainer currentPlate;
    private float speed = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject newBox = Instantiate(deliveryBoxPrefab, transform.position, Quaternion.identity);
        Destroy(other.gameObject);

        if(newBox != null)
        {
            StartCoroutine(MoveBox(newBox));
        }

        PlateContainer plate = other.GetComponent<PlateContainer>();
        if (plate != null)
        {
            currentPlate = plate;
            plate.ShowFinalOrder();
        }

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
