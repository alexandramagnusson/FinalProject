using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlatformerController2D : Controller2D
{
    public float jumpforce; // The force applied when jumping
    public int lives = 5; // The number of lives the player has
    private float inputX; // The horizontal input value
    private SpriteRenderer sRenderer; // Reference to the SpriteRenderer component
    private bool invulnerable = false; // Flag indicating if the player is invulnerable

    public AudioClip jumpsound; // Sound played when jumping
    public AudioClip hitsound; // Sound played when the player gets hit
    public AudioClip coinsound; // Sound played when collecting a coin
    public AudioClip killsound; // Sound played when killing an enemy
    private AudioSource audioSource; // Reference to the AudioSource component

    public override void Start()
    {
        base.Start();
        sRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        HeartsUI.SetLives(lives); // Set the initial number of lives in the HeartsUI
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal") * speed; // Get the horizontal input value and multiply it by the speed
        Vector2 vel = rb2d.velocity; // Get the current velocity
        vel.x = inputX; // Set the x component of the velocity
        relativeVelocity = vel; // Update the relative velocity

        UpdateGrounding(); // Update the grounding status
        if (onMovingPlatform != null)
        {
            vel.x += onMovingPlatform.rb2d.velocity.x; // Adjust the horizontal velocity when standing on a moving platform
        }

        bool inputJump = Input.GetKeyDown(KeyCode.Space); // Check if the jump key is pressed
        if (inputJump && grounded)
        {
            AudioSource.PlayClipAtPoint(jumpsound, transform.position); // Play the jump sound at the player's position
            vel.y = jumpforce; // Set the vertical velocity to the jump force
            relativeVelocity.y = vel.y; // Update the relative velocity
        }
        rb2d.velocity = vel; // Apply the updated velocity to the Rigidbody2D component
    }

    protected override void Hurt(Vector3 impactDirection)
    {
        if (Mathf.Abs(impactDirection.x) > Mathf.Abs(impactDirection.y))
        {
            TakeDamage(); // Take damage if hit horizontally
        }
        else
        {
            if (impactDirection.y > 0.0f)
            {
                TakeDamage(); // Take damage if hit from below
            }
            AudioSource.PlayClipAtPoint(killsound, transform.position); // Play the kill sound at the player's position
            Vector2 vel = rb2d.velocity;
            vel.y = jumpforce; // Set the vertical velocity to the jump force to simulate being knocked upwards
            rb2d.velocity = vel;
        }
    }

    public void TakeDamage()
    {
        if (invulnerable)
        {
            return; // Do nothing if the player is currently invulnerable
        }
        AudioSource.PlayClipAtPoint(hitsound, transform.position); // Play the hit sound at the player's position
        lives--; // Decrease the number of lives
        HeartsUI.RemoveHeart(); // Remove a heart from the HeartsUI
        if (lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the current scene if there are no more lives
        }
        StartCoroutine(Invulnerability(1)); // Start the invulnerability coroutine for a specified time
    }

    public override void Heal(Vector3 impactDirection)
    {
        if (invulnerable)
        {
            return; // Do nothing if the player is currently invulnerable
        }

        lives++; // Increase the number of lives
        HeartsUI.AddHeart(); // Add a heart to the HeartsUI
    }

    public void CollectCoin()
    {
        AudioSource.PlayClipAtPoint(coinsound, transform.position); // Play the coin sound at the player's position
    }

    IEnumerator Invulnerability(float time)
    {
        Debug.Log("Invulnerable " + Time.time);
        invulnerable = true; // Set the invulnerability flag to true
        for (int i = 0; i < time / 0.2f; i++)
        {
            sRenderer.color = Color.red; // Change the sprite color to red
            yield return new WaitForSeconds(0.1f); // Wait for a short period
            sRenderer.color = Color.white; // Change the sprite color back to white
            yield return new WaitForSeconds(0.1f); // Wait for a short period
        }
        invulnerable = false; // Set the invulnerability flag to false
    }
}