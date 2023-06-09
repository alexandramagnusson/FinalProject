using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public FireSpawner fireSpawner;             // Reference to the FireSpawner object
    public List<NPCController> npcControllers;  // List of NPCController objects

    public int extinguishedFires { get; set; }  // Number of fires extinguished
    public int totalFires => fireSpawner.totalFires;  // Total number of fires from the fire spawner
    public int cansCollected { get; set; }      // Number of cans collected
    public int totalCans { get; set; }          // Total number of cans collected from NPCs

    // Update the game statistics with the provided values
    public void UpdateStatistics(int extFires, int cansColl)
    {
        int totalCans = 0;

        // Calculate the total number of cans collected from all NPCs
        foreach (NPCController npc in npcControllers)
        {
            totalCans += npc.totalCans;
        }

        extinguishedFires = extFires;
        cansCollected = cansColl;
        this.totalCans = totalCans;
    }
}

