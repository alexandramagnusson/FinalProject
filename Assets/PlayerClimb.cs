using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private float vertical;         // Stores the vertical input from the player
    private float speed = 5f;       // Climbing speed
    private bool isTree;            // Flag to check if the player is colliding with a tree
    public bool isClimbing;         // Flag to check if the player is currently climbing
    //public AudioClip climbsound;  // Audio clip for climbing sound

    [SerializeField] private Rigidbody2D rb;   // Reference to the player's Rigidbody2D component

    void Update()
    {
        vertical = Input.GetAxis("Vertical");    // Get the vertical input from the player

        if (isTree && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;   // If the player is colliding with a tree and pressing vertical input, set the climbing flag to true
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;   // Set the gravity scale to 0 to prevent the player from falling
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);   // Apply vertical velocity to simulate climbing
            //AudioSource.PlayClipAtPoint(climbsound, transform.position);  // Play climbing sound (if audio source is available)
        }
        else
        {
            rb.gravityScale = 1f;   // Set the gravity scale back to 1 to allow the player to fall
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree"))
        {
            isTree = true;    // If the player collides with a tree, set the isTree flag to true
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTree = false;        // When the player exits the collision with a tree, set the isTree flag to false
        isClimbing = false;    // Reset the climbing flag to false
    }
}