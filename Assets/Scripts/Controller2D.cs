using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller2D : MonoBehaviour
{
    public float speed; // The movement speed of the controller
    public bool grounded; // Flag indicating if the controller is grounded
    public LayerMask groundLayers; // The layers considered as ground
    public float groundRayLength = 0.1f; // The length of the ground raycast
    public float groundRaySpread = 0.1f; // The spread of the ground raycasts
    [HideInInspector] public Vector2 relativeVelocity = new Vector2(); // The relative velocity of the controller

    protected Rigidbody2D rb2d; // Reference to the Rigidbody2D component
    protected MovingPlatform onMovingPlatform; // Reference to the MovingPlatform the controller is standing on

    public virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    protected bool UpdateGrounding()
    {
        Vector3 rayStart = transform.position + Vector3.up * groundRayLength; // The starting position of the center ground raycast
        Vector3 rayStartLeft = transform.position + Vector3.up * groundRayLength + Vector3.left * groundRaySpread; // The starting position of the left ground raycast
        Vector3 rayStartRight = transform.position + Vector3.up * groundRayLength + Vector3.right * groundRaySpread; // The starting position of the right ground raycast

        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, groundRayLength * 2, groundLayers); // Perform a center ground raycast
        RaycastHit2D hitLeft = Physics2D.Raycast(rayStartLeft, Vector2.down, groundRayLength * 2, groundLayers); // Perform a left ground raycast
        RaycastHit2D hitRight = Physics2D.Raycast(rayStartRight, Vector2.down, groundRayLength * 2, groundLayers); // Perform a right ground raycast

        Debug.DrawLine(rayStart, rayStart + Vector3.down * groundRayLength * 2, Color.red); // Draw a red line for the center ground raycast
        Debug.DrawLine(rayStartLeft, rayStartLeft + Vector3.down * groundRayLength * 2, Color.red); // Draw a red line for the left ground raycast
        Debug.DrawLine(rayStartRight, rayStartRight + Vector3.down * groundRayLength * 2, Color.red); // Draw a red line for the right ground raycast

        if (hit.collider != null)
        {
            grounded = true; // Set grounded to true
            onMovingPlatform = hit.collider.gameObject.GetComponent<MovingPlatform>(); // Get the MovingPlatform component if standing on one
            return true; // Return true to indicate grounded status
        }
        else if (hitLeft.collider != null)
        {
            grounded = true; // Set grounded to true
            onMovingPlatform = hitLeft.collider.gameObject.GetComponent<MovingPlatform>(); // Get the MovingPlatform component if standing on one
            return true; // Return true to indicate grounded status
        }
        else if (hitRight.collider != null)
        {
            grounded = true; // Set grounded to true
            onMovingPlatform = hitRight.collider.gameObject.GetComponent<MovingPlatform>(); // Get the MovingPlatform component if standing on one
            return true; // Return true to indicate grounded status
        }
        onMovingPlatform = null;
        grounded = false; // Set grounded to false if not standing on anything
        return false; // Return false to indicate not grounded
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Controller2D controller = collision.gameObject.GetComponent<Controller2D>();
        if (controller != null)
        {
            Vector3 impactDirection = collision.gameObject.transform.position - transform.position;
            if (collision.gameObject.name.Contains("Turbine"))
            {
                Heal(impactDirection); // Call the Heal method if colliding with an object containing "Turbine" in its name
            }
            else
            {
                Hurt(impactDirection); // Call the Hurt method for other collisions
            }
        }
    }

    protected abstract void Hurt(Vector3 impactDirection); // Abstract method to handle when the controller gets hurt
    public abstract void Heal(Vector3 impactDirection); // Abstract method to handle when the controller gets healed
}