using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class stats : MonoBehaviour
{
    public TextMeshProUGUI statisticsText;

    void Start()
    {
        int firesExtinguished = PlayerPrefs.GetInt("FiresExtinguished");
        int totalFires = PlayerPrefs.GetInt("TotalFires");
        int cansCollected = PlayerPrefs.GetInt("CansCollected");
        int totalCans = PlayerPrefs.GetInt("TotalCans");

        statisticsText.text = $"Fires Extinguished: {firesExtinguished}\n" +
                              $"Total Fires: {totalFires}\n" +
                              $"Cans Collected: {cansCollected}\n" +
                              $"Total Cans: {totalCans}";
    }
}
