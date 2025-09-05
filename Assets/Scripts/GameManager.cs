using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject controlPannel;
    public GameObject storePannel;
    public GameObject statusPannel;
    public GameObject playerStatusUI;
    public bool controlPannelIsOpen;
    public bool storePannelIsOpen;
    public bool statusPannelIsOpen;

    // LEVEL and STAT
    public int playerLevel;
    public int expPoints;
    public int statpoints;
    public Text statsText;
    public Text levelText;
    // Experience thresholds
    public int nextLevelExpReq = 100;

    // GOLD
    public int coins;
    public Text goldText;

    // TIME
    public Text timeText;  // Assign a UI Text component in the Inspector
    private float elapsedTime = 0f;  // Keeps track of elapsed time

    // Start is called before the first frame update
    void Start()
    {
        statpoints = 20;
        expPoints = 0;
        coins = 20;
        controlPannelIsOpen = false;
        storePannelIsOpen = false;
        statusPannelIsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGoldText();
        OpenAndCloseScreenMenu();

        UpdateLevel();
        UpdateLevelText();
        UpdateStatsText();

        // Time 
        // Increment elapsed time by the time since last frame
        elapsedTime += Time.deltaTime;

        // Convert elapsed time to minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // Format the time as a string (MM:SS)
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OpenAndCloseScreenMenu()
    {
        if (Input.GetKeyDown(KeyCode.I) && controlPannelIsOpen == false)
        {
            controlPannel.SetActive(true);
            controlPannelIsOpen = true;

            storePannel.SetActive(false);
            storePannelIsOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && controlPannelIsOpen == true)
        {
            controlPannel.SetActive(false);
            playerStatusUI.SetActive(false);
            controlPannelIsOpen = false;
        }
        // STORE
        if (Input.GetKeyDown(KeyCode.M) && storePannelIsOpen == false)
        {
            storePannel.SetActive(true);
            playerStatusUI.SetActive(true);
            storePannelIsOpen = true;

            controlPannel.SetActive(false);
            statusPannel.SetActive(false);
            controlPannelIsOpen = false;
            statusPannelIsOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.M) && storePannelIsOpen == true)
        {
            storePannel.SetActive(false);
            playerStatusUI.SetActive(false);
            storePannelIsOpen = false;
        }
        // STATUS
        if (Input.GetKeyDown(KeyCode.N) && statusPannelIsOpen == false)
        {
            statusPannel.SetActive(true);
            playerStatusUI.SetActive(true);
            statusPannelIsOpen = true;

            storePannel.SetActive(false);
            controlPannel.SetActive(false); 
            storePannelIsOpen = false;
            controlPannelIsOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.N) && statusPannelIsOpen == true)
        {
            statusPannel.SetActive(false);
            playerStatusUI.SetActive(false);
            statusPannelIsOpen = false;
        }
    }

    public void IncreaseGold()
    {
        coins++;
    }

    public void UpdateGoldText()
    {
        goldText.text = coins.ToString();
    }

    public void UpdateLevel()
    {
        if (expPoints >= nextLevelExpReq)
        {
            playerLevel++;
            statpoints += 1;
            nextLevelExpReq += 100;  // Increase the threshold for the next level
            Debug.Log("Player leveled up to level " + playerLevel);
        }
    }

    public void UpdateLevelText()
    {
        levelText.text = playerLevel.ToString();
    }
    public void UpdateStatsText()
    {
        statsText.text = statpoints.ToString();
    }

    // Call this method to reset the timer
    public void ResetTimer()
    {
        elapsedTime = 0f;
    }
}
