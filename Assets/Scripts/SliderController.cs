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
    int temp = 0;
    public Slider slider; 

    public void onSliderChanged(float value)
    {
        valueText.text = value.ToString();
    }

    public void updateTemp
    {
        temp++; 
        slider.value = Temp;
    }

    public void removeTemp
    {
        temp--;
        slider.value = Temp;
    }
}
