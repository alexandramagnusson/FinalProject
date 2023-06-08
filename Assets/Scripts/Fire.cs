using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Bear bear = collision.gameObject.GetComponent<Bear>();
            if (bear != null && bear.water)
            {
                Destroy(gameObject); // Destroy the fire object
            }
        }
    }
}
