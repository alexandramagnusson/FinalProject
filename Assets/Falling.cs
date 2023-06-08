using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public float fallSpeed = 5f; // Speed at which the object falls

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime); // Move the object downwards based on the fall speed and frame rate
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PlatformerController2D pController = other.gameObject.GetComponent<PlatformerController2D>(); // Get the PlatformerController2D component of the collider's game object
        if (pController != null)
        {
            pController.TakeDamage(); // Call the TakeDamage method on the platformer controller
            return;
        }
    }
}