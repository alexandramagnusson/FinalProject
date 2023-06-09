using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    public float animationDuration = 2;
    SpriteRenderer sRenderer;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    // This method is called when the Can object collides with another collider
    void OnTriggerEnter2D(Collider2D other)
    {
        Bear player = other.gameObject.GetComponent<Bear>();
        if (player != null)
        {
            // Increase the number of cans collected in the game statistics
            GameStatistics.instance.IncreaseCansCollected();

            // Start the animation coroutine
            StartCoroutine(CollectAnimation());

            // Remove the collider component from the Can object
            Destroy(GetComponent<Collider2D>());
        }
    }

    // Coroutine for the collection animation
    IEnumerator CollectAnimation()
    {
        // Define the start and end positions, rotation, color, and scale for the animation
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + Vector3.up * 1.5f;
        float startRotation = 0;
        float endRotation = 1500;
        Color startColor = Color.white;
        Color endColor = Color.white;
        endColor.a = 0;
        Vector3 startScale = new Vector3(1, 1, 1);
        Vector3 endScale = new Vector3(1.5f, 1.5f, 1.5f);
        float startTime = Time.time;
        float endTime = Time.time + animationDuration;

        // Perform the animation over time
        while (Time.time < endTime)
        {
            float t = 1 - ((endTime - Time.time) / animationDuration);
            sRenderer.color = Color.Lerp(startColor, endColor, t);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(startRotation, endRotation, t), Vector3.up);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        // Destroy the Can object after the animation is finished
        Destroy(gameObject);
    }
}


