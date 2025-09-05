using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    public Slider manaSlider;
    public Text manaCounter;

    public float currentMana, maxMana;

    /*public static PlayerState Instance { get; set; }
     
     private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);    
        }
        else
        {
            Instance = this;
        }
    }

    maxHealth = playerState.GetComponent<PlayerState>().maxHealth;  
    */

    void Awake()
    {
        manaSlider = GetComponent<Slider>();
    }

    void Start()
    {
        currentMana = maxMana;
    }
    void Update()
    {
        float manaFillValue = currentMana / maxMana;
        manaSlider.value = manaFillValue;
        manaCounter.text = currentMana + "/" + maxMana;

        if (Input.GetKeyDown(KeyCode.U))
        {
            currentMana -= 1;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseMaxMana();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            RestoreMana();
        }
    }

    void IncreaseMaxMana()
    {
        maxMana += 1;
        currentMana += 1;
    }

    void RestoreMana()
    {
        currentMana += 1;
    }
}
