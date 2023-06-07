using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPickupController : MonoBehaviour
{
    private bool isPickedUp = false;   // Flag to check if the object is picked up
    private Transform playerHand;     // Reference to the player's hand transform

    public void PickUp(Transform handTransform)
    {
        isPickedUp = true;
        playerHand = handTransform;
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void Release()
    {
        isPickedUp = false;
        transform.SetParent(null);
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
