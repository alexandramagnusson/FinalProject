using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPickupController : MonoBehaviour
{
    private bool isPickedUp = false;   // Flag to check if the object is picked up
    private Transform playerHand;     // Reference to the player's hand transform

    public void PickUp(Transform handTransform)
    {
        isPickedUp = true;                      // Set the flag to true when the object is picked up
        playerHand = handTransform;             // Store a reference to the player's hand transform
        transform.SetParent(playerHand);        // Set the object's parent to the player's hand
        transform.localPosition = Vector3.zero; // Reset the object's position relative to the player's hand
        transform.rotation = Quaternion.identity; // Reset the object's rotation to its original orientation
    }

    public void Release()
    {
        isPickedUp = false;            // Set the flag to false when the object is released
        transform.SetParent(null);     // Set the object's parent to null to release it
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("destroy");
            Destroy(gameObject);
        }
    }*/
}