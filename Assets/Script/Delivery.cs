using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("On triggere activates");
        Destroy(other.gameObject); // 💥 Boom, gone!
    }
}
