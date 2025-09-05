using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAILvl1 : MonoBehaviour
{
    [SerializeField] float chaseSpeed = 3.0f;
    [SerializeField] float jumpForce = 7.5f;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float attackCooldown = 1.0f;
    [SerializeField] float groundCheckDistance = 0.1f;
    [SerializeField] float verticalChaseThreshold = 2.0f; // The vertical distance at which the enemy will jump to reach the player
    [SerializeField] bool m_noBlood = false;
    public float currentHP;     
    public float maxHP;

    private Animator animator;
    private Rigidbody2D body2d;
    private Transform playerTransform;
    private float timeSinceLastAttack = 0.0f;

    private bool isChasing = false;
    private bool isAttacking = false;
    private bool grounded = false;
    private int facingDirection = 1;
    private int currentAttack = 0;
    public bool isDead = false;

    [Header("Spawn Settings")]
    public GameObject objectToSpawn; // Prefab to spawn
    public int spawnCount = 3; // Number of objects to spawn
    public float spawnHeightOffset = 1.0f; // Height offset for spawning

    public PlayerHealth playerHealth;
    private void Awake()
    {
        // Find the object in the scene
        playerHealth = FindObjectOfType<PlayerHealth>();
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        currentHP = maxHP;

        if (playerHealth == null)
        {
            Debug.LogWarning("playerHealth not found in EnemyAI");
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (isDead != true)
        {
            timeSinceLastAttack += Time.deltaTime;

            CheckGroundStatus();

            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            float verticalDistanceToPlayer = playerTransform.position.y - transform.position.y;

            if (distanceToPlayer < attackRange)
            {
                if (timeSinceLastAttack > attackCooldown)
                {
                    Attack();
                    timeSinceLastAttack = 0.0f;
                }
            }
            else
            {
                if (verticalDistanceToPlayer > verticalChaseThreshold && grounded)
                {
                    JumpToFollowPlayer();
                }
                else
                {
                    if (playerHealth != null)
                    {
                        if (playerHealth.currentHealth == 0)
                        {
                            Debug.Log("enemy stop running");
                            animator.SetInteger("AnimState", 0);

                        }
                        else
                        {
                            ChasePlayer();
                        }
                    }
                }
            }

            // Update air speed in animator
            animator.SetFloat("AirSpeedY", body2d.velocity.y);

            // Update grounded status in animator
            animator.SetBool("Grounded", grounded);
        }
       
    }

    public int FacingDirection()
    {
        return facingDirection;
    }

    void ChasePlayer()
    {
            if (playerHealth.currentHealth > 0)
            {
                isChasing = true;
                isAttacking = false;

                Vector2 direction = (playerTransform.position - transform.position).normalized;

                if (direction.x > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    facingDirection = 1;
                }
                else if (direction.x < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    facingDirection = -1;
                }

                body2d.velocity = new Vector2(direction.x * chaseSpeed, body2d.velocity.y);

                if (grounded)
                {
                    animator.SetInteger("AnimState", 1); // Running animation
                }
            }
    }
    
    public void Attack()
    {
        if(playerHealth != null)
        {
            if (playerHealth.currentHealth > 0)
            {
                isChasing = false;
                isAttacking = true;

                currentAttack++;

                // Loop back to the first attack after the third attack
                if (currentAttack > 3)
                    currentAttack = 1;

                // Reset combo if too much time has passed since the last attack
                if (timeSinceLastAttack > 1.0f)
                    currentAttack = 1;

                // Trigger one of the attack animations
                animator.SetTrigger("Attack" + currentAttack);

                body2d.velocity = Vector2.zero; // Stop movement
            }
        }
        
    }

    void JumpToFollowPlayer()
    {
        animator.SetTrigger("Jump");
        grounded = false;
        animator.SetBool("Grounded", grounded);
        body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
    }

    void CheckGroundStatus()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance);
        grounded = hit.collider != null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Animations
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }
    // Play Hurt Animation
    public void PlayHurtAnimation()
    {
        animator.SetTrigger("Hurt");
    }

    // Play Death Animation
    public void PlayDeathAnimation()
    {
        animator.SetBool("noBlood", m_noBlood);
        animator.SetTrigger("Death");
        isDead = true;

        // Spawn objects at the enemy's position, slightly above
        Vector3 spawnPosition = transform.position + new Vector3(0, spawnHeightOffset, 0);

        // Destroy the enemy after 1 second
        Destroy(gameObject, 2f);

        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
