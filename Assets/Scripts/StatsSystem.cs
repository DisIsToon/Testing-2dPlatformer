using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsSystem : MonoBehaviour
{
    public GameObject statsUI;
    public GameObject playerStatusUI;
    public GameObject InsufficientStatsUI;

    // Stats BTN
    Button strBtn;
    Button agiBtn;
    Button dexBtn;
    Button vitBtn;
    Button wisBtn;
    Button closeStatsPanelBtn;

    // Stats
    public int strCount;
    public int agiCount;
    public int dexCount;
    public int vitCount;
    public int wisCount;

    // Stats Text
    public Text strText;
    public Text agiText;
    public Text dexText;
    public Text vitText;
    public Text wisText;

    public GameManager gameManager;
    public PlayerHealth playerHealth;
    public PlayerMana playerMana;
    public HeroKnight playerScript;

    void Start()
    {
        strCount = 0;
        agiCount = 0;
        dexCount = 0;   
        vitCount = 0;
        wisCount = 0;

        if (gameManager == null)
        {
            Debug.LogWarning("game manager on store system is missing");

            if (playerHealth == null)
            {
                Debug.LogWarning("playerHealth on store system is missing");
            }
            if (playerMana == null)
            {
                Debug.LogWarning("playerMana on store system is missing");
            }
            if (playerScript == null)
            {
                Debug.LogWarning("playerScript on store system is missing");
            }
        }
    }
    void Update()
    {
        UpdateStatsText();
    }

    private IEnumerator ShowInsufficientGoldUI()
    {
        InsufficientStatsUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        InsufficientStatsUI.SetActive(false);
    }
    // Buttons
    public void CloseStorePanel()
    {
        statsUI.SetActive(false);
        playerStatusUI.SetActive(false);
        gameManager.statusPannelIsOpen = false;
    }

    public void ImproveStrength()
    {
        if (gameManager.statpoints >= 1)
        {
            playerScript.UpgradeDamage();
            gameManager.statpoints -= 1;
            strCount++;
        }
        else
        {
            StartCoroutine(ShowInsufficientGoldUI());
        }
    }
    public void ImproveAgility()
    {
        if (gameManager.statpoints >= 1)
        {
            playerScript.m_speed +=1;
            gameManager.statpoints -= 1;
            agiCount++;
        }
        else
        {
            StartCoroutine(ShowInsufficientGoldUI());
        }
    }
    public void ImproveDexterity()
    {
        if (gameManager.statpoints >= 1)
        {
            playerScript.m_attackSpeed -= 0.1f;
            gameManager.statpoints -= 1;
            dexCount++;
        }
        else
        {
            StartCoroutine(ShowInsufficientGoldUI());
        }
    }
    public void ImproveVitality()
    {
        if (gameManager.statpoints >= 1)
        {
            playerHealth.maxHealth += 10;
            playerHealth.currentHealth += 10;
            gameManager.statpoints -= 1;
            vitCount++;
        }
        else
        {
            StartCoroutine(ShowInsufficientGoldUI());
        }
    }
    public void ImproveWisdom()
    {
        if (gameManager.statpoints >= 1)
        {
            playerMana.maxMana += 1;
            playerMana.currentMana += 1;
            gameManager.statpoints -= 1;
            wisCount++;
        }
        else
        {
            StartCoroutine(ShowInsufficientGoldUI());
        }
    }

    public void UpdateStatsText()
    {
        strText.text = strCount.ToString();
        agiText.text = agiCount.ToString();
        dexText.text = dexCount.ToString();
        vitText.text = vitCount.ToString();
        wisText.text = wisCount.ToString();
    }

}
