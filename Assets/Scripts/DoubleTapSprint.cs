using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapSprint : MonoBehaviour
{

    public float sprintSpeed = 10f; // Speed when sprinting
    public float normalSpeed = 5f; // Normal speed
    public float sprintDuration = 2f; // Duration of the sprint in seconds
    public float doubleTapTimeThreshold = 0.2f; // Maximum time between key presses to register as a double tap

    private float sprintTimer = 0f; // Timer for sprint duration
    private bool isSprinting = false; // Flag to indicate if the player is currently sprinting
    private float lastKeyPressTime = 0f; // Time of the last key press
    private bool doubleTapDetected = false; // Flag to indicate if a double tap has been detected

    private void Update()
    {
        // Check for double tap
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Time.time - lastKeyPressTime <= doubleTapTimeThreshold)
            {
                doubleTapDetected = true;
            }
            lastKeyPressTime = Time.time;
        }

        // Start sprinting if double tap detected
        if (doubleTapDetected)
        {
            StartSprint();
            doubleTapDetected = false;
        }

        // Update sprint timer and stop sprinting if duration is over
        if (isSprinting)
        {
            sprintTimer += Time.deltaTime;
            if (sprintTimer >= sprintDuration)
            {
                StopSprint();
            }
        }

        // Move the character using the appropriate speed
        float moveSpeed = isSprinting ? sprintSpeed : normalSpeed;
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);
    }

    private void StartSprint()
    {
        if (!isSprinting)
        {
            isSprinting = true;
            sprintTimer = 0f;
            Debug.Log("Sprinting!");
        }
    }

    private void StopSprint()
    {
        if (isSprinting)
        {
            isSprinting = false;
            Debug.Log("Stopped sprinting.");
        }
    }
}
