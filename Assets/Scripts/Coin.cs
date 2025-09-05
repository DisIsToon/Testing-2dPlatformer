using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        // Find the GameManager object in the scene
        gameManager = FindObjectOfType<GameManager>();

        // If GameManager is still null, log a warning
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager not found in the scene. Make sure it exists.");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject, 0f);
            gameManager.IncreaseGold();
        }
    }


}
