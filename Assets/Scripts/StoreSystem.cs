using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSystem : MonoBehaviour
{
    public GameObject storeUI;
    public GameObject playerStatusUI;
    public GameObject InsufficientGoldUI;

    // Store Item BTNs
    Button buyHpPBtn;
    Button buyMpPBtn;
    Button buyHpRingBtn;
    Button buyMpRingBtn;
    Button buySwordBtn;
    Button closeStorePanelBtn;

    public GameManager gameManager;
    public PlayerHealth playerHealth;
    public PlayerMana playerMana;
    public HeroKnight playerScript;

    void Start()
    {
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
    private IEnumerator ShowInsufficientGoldUI()
    {
        InsufficientGoldUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        InsufficientGoldUI.SetActive(false);
    }
    // Buttons
    public void CloseStorePanel()
    {
        storeUI.SetActive(false);
        playerStatusUI.SetActive(false);
        gameManager.storePannelIsOpen = false;
    }

    public void BuyHealthPotion()
    {
        if (gameManager.coins >= 5)
        {
            playerHealth.currentHealth += 20;
            gameManager.coins -= 5;
        }
        else
        {
            StartCoroutine(ShowInsufficientGoldUI());
        }
    }
    public void BuyManaPotion()
    {
        if (gameManager.coins >= 5)
        {
            playerMana.currentMana += 3;
            gameManager.coins -= 5;
        }
        else
        {
            StartCoroutine(ShowInsufficientGoldUI());
        }
    }
    public void BuyHealthRing()
    {
        if (gameManager.coins >= 100)
        {
            playerHealth.currentHealth += 200;
            gameManager.coins -= 100;
        }
        else
        {
            StartCoroutine(ShowInsufficientGoldUI());
        }
    }
    public void BuyManaRing()
    {
        if (gameManager.coins >= 100)
        {
            playerMana.currentMana += 100;
            gameManager.coins -= 100;
        }
        else
        {
            StartCoroutine(ShowInsufficientGoldUI());
        }
    }
    public void BuySword()
    {
        if (gameManager.coins >= 10)
        {
            playerScript.UpgradeDamage();
            gameManager.coins -= 10;
        }
        else
        {
            StartCoroutine(ShowInsufficientGoldUI());
        }
    }
}
