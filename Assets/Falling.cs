using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{

    public float fallSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PlatformerController2D pController = other.gameObject.GetComponent<PlatformerController2D>();
        if (pController != null)
        {
            pController.TakeDamage();
            return;
        }
    }

}
