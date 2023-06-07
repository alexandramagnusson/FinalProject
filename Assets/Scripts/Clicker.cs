using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    public Button clickButton;
    public Slider healthBar;

    public float healthIncreaseAmount = 10f;
    public float maxHealth = 100f;

    private float currentHealth;

    private void Start()
    {
        clickButton.onClick.AddListener(OnClick);
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void OnClick()
    {
        currentHealth += healthIncreaseAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
    }

}
