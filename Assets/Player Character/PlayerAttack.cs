using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] public float knockbackForce = 5f; // Force applied to the player for knockback
    [SerializeField] public PolygonCollider2D attackCollider; // Reference to the attack collider
    private EnemyAILvl1 enemyScript; // Reference to the attack collider and enemy hp
    private HeroKnight playerscript; // Get Damage value the from the player
    private GameManager gameManager;

    private void Awake()
    {
        // Find the object in the scene
        playerscript = FindObjectOfType<HeroKnight>();
        gameManager = FindObjectOfType<GameManager>();

        if (playerscript == null)
        {
            Debug.LogWarning("playerscript not found in PlayerAttack");
        }
        if (gameManager == null)
        {
            Debug.LogWarning("gameManager not found in PlayerAttack");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyScript = other.GetComponent<EnemyAILvl1>();
            if (enemyScript == null)
            {
                Debug.LogWarning("enemyScript not found in PlayerAttack");
            }
            if (enemyScript != null && enemyScript.isDead != true)
            {
                enemyScript.TakeDamage(playerscript.m_damage);
                if (enemyScript.currentHP <= 0)
                {
                    gameManager.expPoints += 40;
                    Debug.Log("Death Animation will play");
                    enemyScript.PlayDeathAnimation();
                }
                else
                {
                    enemyScript.PlayHurtAnimation();
                }

            }

        }
    }
}
