using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    bool firstInstance = false;
    void Start()
    {
        MusicPlayer[] instances = FindObjectsOfType<MusicPlayer>();
        if (instances.Length > 1) {
            if (!firstInstance) { 
                Destroy(gameObject);
            }
        } else {
            firstInstance = true;
            DontDestroyOnLoad(gameObject);
        }
    }
}

