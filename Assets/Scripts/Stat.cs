using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stat : MonoBehaviour
{
    public TextMeshProUGUI statisticsText;

    void Start()
    {
        int extinguishedFires = GameStatistics.instance.extinguishedFires;
        int totalFires = GameStatistics.instance.totalFires;
        int cansCollected = GameStatistics.instance.cansCollected;
        int totalCans = GameStatistics.instance.totalCans;

        statisticsText.text = $"Fires Extinguished: {extinguishedFires}\n" +
                              $"Total Fires: {totalFires}\n" +
                              $"Cans Collected: {cansCollected}\n" +
                              $"Total Cans: {totalCans}";
    }
}

