using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour
{
    public string nextLevel = "";
    public Sprite closedSprite;
    public Sprite[] openAnimation;
    public float animationFPS = 5;
    private float frameTimer = 0;
    private int frameIndex = 0;
    private SpriteRenderer sRenderer;


    public bool collectAllCoinsToOpen = true;
    private int numCoins = 0;

    private bool isOpen = false;

    private void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        Open();
    }

    private void Start()
    {
        if(nextLevel == "")
        {
            nextLevel = SceneManager.GetActiveScene().name;
        }
    }

    private void Update()
    {
        if (isOpen)
        {
            frameTimer -= Time.deltaTime;
            if (frameTimer <= 0.0f)
            {
                frameTimer = 1 / animationFPS;
                frameIndex %= openAnimation.Length;
                sRenderer.sprite = openAnimation[frameIndex];
                frameIndex++;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpen) return;

        PlatformerController2D controller = other.gameObject.GetComponent<PlatformerController2D>();
        if (controller != null) {
            SceneManager.LoadScene(nextLevel);
        }
    }

    public void RegisterCoin()
    {
        numCoins++;
        if (collectAllCoinsToOpen)
        {
            Close();
        }
    }

    public void CoinCollected()
    {
        numCoins--;
        if(numCoins <= 25)
        {
            Open();
        }
    }

    private void Open()
    {
        isOpen = true;
    }

    private void Close()
    {
        isOpen = false;
        sRenderer.sprite = closedSprite;
    }
}
