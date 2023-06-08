using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void Start()
    {
        // Start is called before the first frame update
        // This method is empty, no code execution is present in the Start() method
    }

    void Update()
    {
        // Update is called once per frame
        // Checks if the player pressed the "Escape" key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the "Escape" key is pressed, exit the game
            Application.Quit();
        }
    }
}