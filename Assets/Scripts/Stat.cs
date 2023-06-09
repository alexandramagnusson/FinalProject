using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stat : MonoBehaviour
{
    public TextMeshProUGUI statisticsText;

    void Start()
    {
        StartCoroutine(DisplayStats());
    }

    private IEnumerator DisplayStats()
    {
        yield return new WaitForSeconds(0.1f); // Delay the display of stats

        int extinguishedFires = GameStatistics.instance.firesStat;
        int totalFires = 5;
        int cansCollectedDisplay = GameStatistics.instance.cansCollectedDisplay;
        int totalCans = GameStatistics.instance.totalCans;

        statisticsText.text = $"Fires Extinguished: {extinguishedFires}\n" +
                              $"Cans Collected: {cansCollectedDisplay}\n" +
                              $"Total Cans: {totalCans}";
    }
}

