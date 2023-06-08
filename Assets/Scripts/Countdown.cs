using UnityEngine;
using TMPro; // Required for dealing with TextMeshPro
using System.Collections;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public float timeLeft = 60.0f; // Time to countdown from
    public TextMeshProUGUI countdownText; // TextMeshProUGUI component to display the countdown

    public string nextLevel = "";

    private void Start()
    {
        if (nextLevel == "")
        {
            nextLevel = SceneManager.GetActiveScene().name;
        }
    }

    void Update()
    {
        // Reduce the time left
        timeLeft -= Time.deltaTime;

        // Ensure that the time does not go below 0
        timeLeft = Mathf.Max(timeLeft, 0);

        // Display the time left
        countdownText.text = "Time Left: " + Mathf.Round(timeLeft).ToString();

        // Check if time has run out
        if (timeLeft <= 0)
        {
            // Handle time running out, such as ending the game
            SceneManager.LoadScene(nextLevel);

        }
    }
}

