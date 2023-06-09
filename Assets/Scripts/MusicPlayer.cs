using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    bool firstInstance = false;

    // Start is called before the first frame update
    void Start()
    {
        // Find all instances of the MusicPlayer script in the scene
        MusicPlayer[] instances = FindObjectsOfType<MusicPlayer>();

        // Check if there is more than one instance of the MusicPlayer
        if (instances.Length > 1)
        {
            // If it's not the first instance, destroy the duplicate MusicPlayer object
            if (!firstInstance)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Set the firstInstance flag to true for the initial MusicPlayer object
            firstInstance = true;

            // Make the MusicPlayer object persistent across scene changes
            DontDestroyOnLoad(gameObject);
        }
    }
}


