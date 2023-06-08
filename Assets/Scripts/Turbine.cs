using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : MonoBehaviour
{
    public float animationDuration = 2; // Duration of the collect animation
    SpriteRenderer sRenderer; // Reference to the SpriteRenderer component

    private void Start()
    {
        // This method is empty, can be used for any additional initialization logic
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlatformerController2D controller = other.gameObject.GetComponent<PlatformerController2D>(); // Get the PlatformerController2D component of the collider's game object
        if (controller != null)
        {
            controller.Heal(Vector3.zero); // Call the Heal method on the controller, passing zero impact direction
        }
    }

    IEnumerator CollectAnimation()
    {
        Vector3 startPosition = transform.position; // Starting position of the turbine
        Vector3 endPosition = transform.position + Vector3.up * 1.5f; // Ending position of the turbine after animation
        float startRotation = 0; // Starting rotation angle of the turbine
        float endRotation = 1500; // Ending rotation angle of the turbine after animation
        Color startColor = Color.white; // Starting color of the turbine
        Color endColor = Color.white; // Ending color of the turbine after animation
        endColor.a = 0; // Set the alpha value of the ending color to zero (transparent)
        Vector3 startScale = new Vector3(1, 1, 1); // Starting scale of the turbine
        Vector3 endScale = new Vector3(1.5f, 1.5f, 1.5f); // Ending scale of the turbine after animation
        float startTime = Time.time; // Start time of the animation
        float endTime = Time.time + animationDuration; // End time of the animation

        while (Time.time < endTime)
        {
            float t = 1 - ((endTime - Time.time) / animationDuration); // Calculate the normalized time between 0 and 1
            sRenderer.color = Color.Lerp(startColor, endColor, t); // Interpolate the color of the turbine
            transform.localScale = Vector3.Lerp(startScale, endScale, t); // Interpolate the scale of the turbine
            transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(startRotation, endRotation, t), Vector3.up); // Interpolate the rotation of the turbine around the y-axis
            transform.position = Vector3.Lerp(startPosition, endPosition, t); // Interpolate the position of the turbine
            yield return null; // Wait for the next frame
        }

        Destroy(gameObject); // Destroy the turbine game object after the animation is finished
    }
}