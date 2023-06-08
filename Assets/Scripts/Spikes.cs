using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        PlatformerController2D pController = other.gameObject.GetComponent<PlatformerController2D>(); // Get the PlatformerController2D component of the collider's game object
        if (pController != null)
        {
            pController.TakeDamage(); // Call the TakeDamage method of the PlatformerController2D component if it exists
            return; // Exit the method to avoid further interactions
        }
    }
}