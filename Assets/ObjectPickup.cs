using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canPickup = false;     // Flag to check if the player can pick up an object
    private GameObject pickupObject;    // Reference to the object that can be picked up
    private Transform playerHand;       // Reference to the player's hand transform

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickupObject"))
        {
            canPickup = true;
            pickupObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PickupObject"))
        {
            canPickup = false;
            pickupObject = null;
        }
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            PickUpObject();
        }
        else if (!canPickup && Input.GetKeyDown(KeyCode.E))
        {
            DropObject();
        }
    }

    private void PickUpObject()
    {
        if (pickupObject != null)
        {
            pickupObject.transform.SetParent(playerHand);
            pickupObject.transform.localPosition = Vector3.zero;
            pickupObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    private void DropObject()
    {
        if (pickupObject != null)
        {
            pickupObject.transform.SetParent(null);
            pickupObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
