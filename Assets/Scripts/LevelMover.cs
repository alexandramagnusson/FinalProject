using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMover : MonoBehaviour
{
   
    public string nextLevelName; // Name of the next level scene

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName); // Load the next level scene
    }
}

