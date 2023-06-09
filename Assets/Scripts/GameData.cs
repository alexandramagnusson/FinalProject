using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public FireSpawner fireSpawner;
    public List<NPCController> npcControllers;

    public int extinguishedFires { get; set; }
    public int totalFires => fireSpawner.totalFires;
    public int cansCollected { get; set; }
    public int totalCans { get; set; }

    public void UpdateStatistics(int extFires, int cansColl)
    {
        int totalCans = 0;

        foreach (NPCController npc in npcControllers)
        {
            totalCans += npc.totalCans;
        }

        extinguishedFires = extFires;
        cansCollected = cansColl;
        this.totalCans = totalCans;
    }
}
