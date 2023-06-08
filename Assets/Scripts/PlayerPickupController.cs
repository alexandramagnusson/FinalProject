using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupController : MonoBehaviour
{
    public Transform grabDetect;
    public Transform objectHolder;
    public float rayDist;
    private GameObject grabbedObject = null;
    public LayerMask layerMask;

    private void Update()
    {
    

        if (Input.GetKeyDown(KeyCode.G)) // Check if the player pressed the grab key (G)

        //if (grabCheck.collider != null && grabCheck.collider.tag == "PickupObject")
        {


            if (grabbedObject == null) // If no object is currently grabbed
            {
                RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist, layerMask);
                // Cast a ray from grabDetect position in the direction of player's facing, with given distance and layer mask

                if (grabCheck.collider != null)
                {
                    //grabCheck.collider.transform.parent = objectHolder;
                    grabCheck.collider.transform.SetParent(objectHolder); // Set the grabbed object's parent to objectHolder
                    grabCheck.collider.transform.position = objectHolder.position; // Move the grabbed object to objectHolder's position
                    //grabCheck.collider.GetComponent<Rigidbody2D>().isKinematic = true;
                    //grabCheck.collider.enabled = false;
                    grabCheck.collider.GetComponent<Rigidbody2D>().simulated = false; // Disable the grabbed object's physics simulation
                    grabbedObject = grabCheck.collider.gameObject; // Update the reference to the grabbed object

                }
            }
            else
            {
                grabbedObject.transform.parent = null; // Release the grabbed object by setting its parent to null
                //grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                //grabbedObject.GetComponent<Collider2D>().enabled = true;
                grabbedObject.GetComponent<Rigidbody2D>().simulated = true; // Enable the grabbed object's physics simulation
                grabbedObject = null; // Reset the reference to the grabbed object
            }
        }
    }


}
