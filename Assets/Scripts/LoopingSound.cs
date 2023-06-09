using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class LoopingSound : MonoBehaviour
{
    public AudioClip clip; // The audio clip that will be looped

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = true; // Set the audio to loop
        audioSource.Play(); // Start playing the audio
    }
}
