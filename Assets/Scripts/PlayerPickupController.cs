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
    

        if (Input.GetKeyDown(KeyCode.G))
        //if (grabCheck.collider != null && grabCheck.collider.tag == "PickupObject")
        {

           
            if (grabbedObject == null)
            {
                RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist, layerMask);
                if (grabCheck.collider != null)
                {
                    //grabCheck.collider.transform.parent = objectHolder;
                    grabCheck.collider.transform.SetParent(objectHolder);
                    grabCheck.collider.transform.position = objectHolder.position;
                    //grabCheck.collider.GetComponent<Rigidbody2D>().isKinematic = true;
                    //grabCheck.collider.enabled = false;
                    grabCheck.collider.GetComponent<Rigidbody2D>().simulated = false;
                    grabbedObject = grabCheck.collider.gameObject;

                }
            }
            else
            {
                grabbedObject.transform.parent = null;
                //grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                //grabbedObject.GetComponent<Collider2D>().enabled = true;
                grabbedObject.GetComponent<Rigidbody2D>().simulated = true;
                grabbedObject = null;
            }
        }
    }


}
