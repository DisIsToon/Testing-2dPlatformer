using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiAtkDirection : MonoBehaviour
{
    [SerializeField] public GameObject attack1collider;
    [SerializeField] public GameObject attack2collider; // The object that should rotate based on the enemy's direction
    [SerializeField] public GameObject attack3collider; 
    [SerializeField] private EnemyAILvl1 enemyAI; // Reference to the EnemyAI script

    void Update()
    {
        // Check the direction the enemy is facing
        int facingDirection = enemyAI.FacingDirection();

        // Rotate the object based on the enemy's direction
        if (facingDirection == -1) // Facing left
        {
            attack1collider.transform.rotation = Quaternion.Euler(0, 180, 0);
            attack2collider.transform.rotation = Quaternion.Euler(0, 180, 0);
            attack3collider.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (facingDirection == 1) // Facing right
        {
            attack1collider.transform.rotation = Quaternion.Euler(0, 0, 0);
            attack2collider.transform.rotation = Quaternion.Euler(0, 0, 0);
            attack3collider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
