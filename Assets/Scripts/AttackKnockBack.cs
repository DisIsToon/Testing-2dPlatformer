using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackKnockBack : MonoBehaviour
{   
    [SerializeField] public int damageValue = 1; // Damage value the player will take
    [SerializeField] public float knockbackForce = 5f; // Force applied to the player for knockback
    [SerializeField] public Transform playerTransform; // Reference to the player's transform
    [SerializeField] public Rigidbody2D playerRigidbody; // Reference to the player's Rigidbody2D
    [SerializeField] public PolygonCollider2D attackCollider; // Reference to the attack collider

    private PlayerHealth playerHealth;
    private HeroKnight playerscript; 

    private void Awake()
    {
        // Find the object in the scene
        playerscript = FindObjectOfType<HeroKnight>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerRigidbody == null)
        {
            playerRigidbody = FindObjectOfType<Rigidbody2D>();
        }

        // If GameManager is still null, log a warning
        if (playerscript == null)
        {
            Debug.LogWarning("playerscript not found in Atk KnockBack");
        }
        if (playerHealth == null)
        {
            Debug.LogWarning("playerHealth not found in Atk KnockBack");
        }
        if (playerRigidbody == null)
        {
            Debug.LogWarning("playerRigidbody not found in Atk KnockBack");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (playerHealth != null && playerscript != null)
            {
                playerHealth.TakeDamage(damageValue);
                if(playerHealth.currentHealth <= 0)
                {
                    playerscript.PlayDeathAnimation();
                }
                else
                {
                    playerscript.PlayHurtAnimation();
                }
                
            }

            // Apply knockback to the player
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
            playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
