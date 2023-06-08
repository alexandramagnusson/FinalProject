using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public GameObject firePrefab; // The fire object to spawn
    public Vector2[] spawnPoints; // The possible spawn points
    public float spawnInterval = 5f; // The interval between spawns, in seconds

    public int totalFires = 0; // Total number of fires spawned

    private void Start()
    {
        StartCoroutine(SpawnFire());
    }

    IEnumerator SpawnFire()
    {
        while (true)
        {
            // Choose a random spawn point
            Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the fire object at the chosen spawn point
            Instantiate(firePrefab, spawnPoint, Quaternion.identity);

            // Wait for spawnInterval seconds
            yield return new WaitForSeconds(spawnInterval);

            // Increment the totalFires
            totalFires++;
        }
    }
}

