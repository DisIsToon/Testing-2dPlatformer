using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour
{
    public Text currentExpText;
    public Text maxExpText;
    public Text currentHpText;
    public Text maxHpText;
    public Text currentMpText;
    public Text maxMpText;
    public Text damageText;
    public Text moveSpeedText;
    public Text attackSpeedText;

    public GameManager gameManager;
    public PlayerHealth playerHealth;
    public PlayerMana playerMana;
    public HeroKnight playerScript;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("game manager on PlayerStatus system is missing");

            if (playerHealth == null)
            {
                Debug.LogWarning("playerHealth on PlayerStatus system is missing");
            }
            if (playerMana == null)
            {
                Debug.LogWarning("playerMana on PlayerStatus system is missing");
            }
            if (playerScript == null)
            {
                Debug.LogWarning("playerScript on PlayerStatus system is missing");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerStatusText();
    }

    public void UpdatePlayerStatusText()
    {
        currentExpText.text = gameManager.expPoints.ToString();
        maxExpText.text = gameManager.nextLevelExpReq.ToString();
        currentHpText.text = playerHealth.currentHealth.ToString();
        maxHpText.text = playerHealth.maxHealth.ToString();
        currentMpText.text = playerMana.currentMana.ToString();
        maxMpText.text = playerMana.maxMana.ToString();
        damageText.text = playerScript.m_damage.ToString();
        moveSpeedText.text = playerScript.m_speed.ToString();
        attackSpeedText.text = playerScript.m_attackSpeed.ToString();
    }
}
