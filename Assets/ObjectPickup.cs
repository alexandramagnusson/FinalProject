using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private bool canPickup = false;     // Flag to check if the player can pick up an object
    private GameObject pickupObject;    // Reference to the object that can be picked up
    private Transform playerHand;       // Reference to the player's hand transform

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickupObject"))
        {
            canPickup = true;                       // Set the flag to true when the player enters the trigger of a pickup object
            pickupObject = collision.gameObject;    // Store a reference to the pickup object
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PickupObject"))
        {
            canPickup = false;                      // Set the flag to false when the player exits the trigger of a pickup object
            pickupObject = null;                    // Clear the reference to the pickup object
        }
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))   // Check if the player can pick up an object and presses the "E" key
        {
            PickUpObject();                             // Call the method to pick up the object
        }
        else if (!canPickup && Input.GetKeyDown(KeyCode.E)) // Check if the player cannot pick up an object and presses the "E" key
        {
            DropObject();                               // Call the method to drop the object
        }
    }

    private void PickUpObject()
    {
        if (pickupObject != null)                               // Check if a pickup object is currently stored
        {
            pickupObject.transform.SetParent(playerHand);      // Set the pickup object's parent to the player's hand
            pickupObject.transform.localPosition = Vector3.zero; // Reset the pickup object's position relative to the player's hand
            pickupObject.GetComponent<Rigidbody2D>().isKinematic = true; // Disable the pickup object's rigidbody physics
        }
    }

    private void DropObject()
    {
        if (pickupObject != null)                               // Check if a pickup object is currently stored
        {
            pickupObject.transform.SetParent(null);             // Set the pickup object's parent to null to release it from the player's hand
            pickupObject.GetComponent<Rigidbody2D>().isKinematic = false; // Enable the pickup object's rigidbody physics
        }
    }
}