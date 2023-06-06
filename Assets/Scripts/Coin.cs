using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float animationDuration = 2;
    SpriteRenderer sRenderer;

    private void Start()
    {
        Doorway door = FindObjectOfType<Doorway>();
        if(door!=null) door.RegisterCoin();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlatformerController2D controller = other.gameObject.GetComponent<PlatformerController2D>();
        if (controller != null) {
            controller.CollectCoin();
            Doorway door = FindObjectOfType<Doorway>();
            if (door!=null) door.CoinCollected();
            StartCoroutine(CollectAnimation());
            Destroy(GetComponent<Collider2D>());
        }
    }

    IEnumerator CollectAnimation()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + Vector3.up * 1.5f;
        float startRotation = 0;
        float endRotation = 1500;
        Color startColor = Color.white;
        Color endColor = Color.white;
        endColor.a = 0;
        Vector3 startScale = new Vector3(1, 1, 1);
        Vector3 endScale = new Vector3(1.5f, 1.5f, 1.5f);
        float startTime = Time.time;
        float endTime = Time.time + animationDuration;
        while(Time.time < endTime)
        {
            float t = 1-((endTime - Time.time) / animationDuration);
            sRenderer.color = Color.Lerp(startColor, endColor, t);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(startRotation, endRotation, t), Vector3.up);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
