using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    public Image firstHeart; // Reference to the first heart image in the UI
    public GameObject heartsPanel; // Reference to the panel containing the hearts
    public Image[] hearts; // Array of heart images
    private int heartsVisible; // Number of currently visible hearts
    private static HeartsUI instance; // Static reference to the HeartsUI instance

    void Awake()
    {
        instance = this; // Set the instance to this HeartsUI component
    }

    public static void SetLives(int lives)
    {
        instance._SetLives(lives); // Call the _SetLives method on the instance
    }

    public static void RemoveHeart()
    {
        instance._RemoveHeart(); // Call the _RemoveHeart method on the instance
    }

    private void _SetLives(int lives)
    {
        RectTransform panelRT = heartsPanel.GetComponent<RectTransform>(); // Get the RectTransform component of the hearts panel
        panelRT.sizeDelta = new Vector2(14 + lives * 18, panelRT.sizeDelta.y); // Adjust the width of the panel based on the number of lives
        hearts = new Image[lives]; // Create a new array of heart images with the specified number of lives
        hearts[0] = firstHeart; // Set the first heart image in the array
        for (int i = 1; i < lives; i++)
        {
            GameObject newHeartObj = Instantiate(firstHeart.gameObject, panelRT) as GameObject; // Instantiate a new heart game object based on the first heart
            hearts[i] = newHeartObj.GetComponent<Image>(); // Get the Image component of the new heart game object
            RectTransform heartRT = newHeartObj.GetComponent<RectTransform>(); // Get the RectTransform component of the new heart game object
            RectTransform firstHeartRT = firstHeart.GetComponent<RectTransform>(); // Get the RectTransform component of the first heart
            // Set the properties of the new heart RectTransform to match the first heart RectTransform
            heartRT.anchorMax = firstHeartRT.anchorMax;
            heartRT.anchorMin = firstHeartRT.anchorMin;
            heartRT.anchoredPosition = firstHeartRT.anchoredPosition;
            heartRT.sizeDelta = firstHeartRT.sizeDelta;
            heartRT.localPosition = new Vector3(firstHeartRT.localPosition.x + 18 * i, firstHeartRT.localPosition.y, firstHeartRT.localPosition.z); // Adjust the position of the new heart
        }
        heartsVisible = lives; // Set the number of visible hearts to the specified number of lives
    }

    private void _RemoveHeart()
    {
        heartsVisible--; // Decrease the number of visible hearts
        if (heartsVisible >= 0)
        {
            hearts[heartsVisible].enabled = false; // Disable the last visible heart
        }
    }

    public static void AddHeart()
    {
        instance._AddHeart(); // Call the _AddHeart method on the instance
    }

    private void _AddHeart()
    {
        if (heartsVisible < hearts.Length)
        {
            hearts[heartsVisible].enabled = true; // Enable the next heart in the array
            heartsVisible++; // Increase the number of visible hearts
        }
    }
}