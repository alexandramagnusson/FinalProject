using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    // Singleton instance
    public static GameStatistics instance = null;

    public FireSpawner fireSpawner; // Reference to the FireSpawner
    public List<NPCController> npcControllers; // List of all NPCControllers

    public int extinguishedFires { get; private set; }
    public int totalFires => fireSpawner.totalFires; // Total fires is now pulled from the FireSpawner
    public int cansCollected { get; private set; }
    public int totalCans { get; private set; }
    public int cansCollectedDisplay;
    public int firesStat;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // this lets our gameObject survive scene changes
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject); // Destroys the new object that was created on scene load
            }
        }
    }

    public void UpdateStatistics(int extFires, int cansColl)
    {
        // Count up all cans
        int totalCans = 0;

        foreach (NPCController npc in npcControllers)
        {
            totalCans += npc.totalCans;
        }

        extinguishedFires = extFires;
        cansCollected = cansColl;
        this.totalCans = totalCans;
    }
    public void IncreaseCansCollected()
    {
        cansCollectedDisplay++;
    }
    public void IncreaseFires()
    {
        firesStat++;
    }
}




