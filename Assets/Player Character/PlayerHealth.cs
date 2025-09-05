using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider hpSlider;
    public Text healthCounter;

    public float currentHealth;
    public float maxHealth;

    void Awake()
    {
        hpSlider = GetComponent<Slider>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        float hpFillValue = currentHealth / maxHealth;
        hpSlider.value = hpFillValue;
        healthCounter.text = currentHealth + "/" + maxHealth;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentHealth -= 1;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreseMaxHp();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            RestoreHp();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    void IncreseMaxHp()
    {
        maxHealth += 1;
        currentHealth += 1;
    }

    void RestoreHp()
    {
        currentHealth += 1;
    }
}
