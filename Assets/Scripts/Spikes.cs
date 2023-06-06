using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        PlatformerController2D pController = other.gameObject.GetComponent<PlatformerController2D>();
        if (pController != null) {
            pController.TakeDamage();
            return;
        }
    }
}
