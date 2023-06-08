using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCController : MonoBehaviour
{
    public GameObject bear; // The bear object
    public TextMeshProUGUI statusText; // The UI Text object

    public Vector2 pointA;
    public Vector2 pointB;
    public float speed = 2f;

    public GameObject objectToSpawn; // The GameObject to spawn
    public float spawnRate = 2f; // The rate to spawn the GameObject, in seconds

    private Vector2 currentTarget;

    public int totalCans; // total cans

    private void Start()
    {
        currentTarget = pointB;
        StartCoroutine(SpawnObject());
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, step);

        if ((Vector2)transform.position == pointA)
        {
            currentTarget = pointB;
        }
        else if ((Vector2)transform.position == pointB)
        {
            currentTarget = pointA;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Bear bear = collision.gameObject.GetComponent<Bear>();
            if (bear != null)
            {
                bear.TakeDamage(1);

                // Update the status text and then wait for 3 seconds before changing it back to empty
                statusText.text = $"Scared a camper, {bear.getLives()} lives left!";
                StartCoroutine(WaitAndClearText(3f));
            }
        }
    }

    System.Collections.IEnumerator SpawnObject()
    {
        // Loop indefinitely
        while (true)
        {
            // Instantiate the object at the NPC's current position
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            totalCans++;

            // Wait for spawnRate seconds
            yield return new WaitForSeconds(spawnRate);
        }
    }

    IEnumerator WaitAndClearText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        statusText.text = "";
    }

}
