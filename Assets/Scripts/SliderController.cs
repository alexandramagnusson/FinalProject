using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SliderController : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text valueText;
    int temp = 10;
    public Slider slider; 

    public void AddTemp()
    {
        temp++; 
        slider.value = temp;
        valueText.text = temp.ToString();
        Debug.Log("AddTemp");
    }

    public void RemoveTemp()
    {
        temp--;
        Debug.Log("RemoveTemp");
        slider.value = temp;
        valueText.text = temp.ToString();
    }
}
