using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpawner : MonoBehaviour
{

    public GameObject objectPrefab;      // Prefab of the object to spawn
    public float spawnInterval = 1f;     // Time interval between object spawns
    public Vector3 spawnPosition;        // Position where the object spawns

    private float timer;                 // Timer to track spawn intervals

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a new object
        if (timer >= spawnInterval)
        {
            // Reset the timer
            timer = 0f;

            // Spawn a new object
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        // Instantiate the object at the spawn position
        GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        // Attach the FallingObject script to the spawned object
        Falling fallingObject = newObject.AddComponent<Falling>();
        // Set the falling speed of the object
        fallingObject.fallSpeed = Random.Range(2f, 6f);
    }


}
