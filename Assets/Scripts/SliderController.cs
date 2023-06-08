using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    public TMP_Text valueText; // Reference to the TextMeshProUGUI component displaying the value
    int temp = 10; // The current value
    public Slider slider; // Reference to the Slider component

    public void AddTemp()
    {
        temp++; // Increment the value
        slider.value = temp; // Update the Slider value to match the new value
        valueText.text = temp.ToString(); // Update the value text display
        Debug.Log("AddTemp"); // Log a message to the console
    }

    public void RemoveTemp()
    {
        temp--; // Decrement the value
        Debug.Log("RemoveTemp"); // Log a message to the console
        slider.value = temp; // Update the Slider value to match the new value
        valueText.text = temp.ToString(); // Update the value text display
    }
}