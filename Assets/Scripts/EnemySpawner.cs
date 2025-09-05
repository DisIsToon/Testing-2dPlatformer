using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Reference to the enemy prefab
    public Transform spawnPoint;    // Reference to the spawn point for enemies
    public float spawnDelay = 2.0f; // Delay before spawning an enemy
    public float respawnDelay = 2.0f; // Delay after enemy death before respawning
    public float maxEnemyLifetime = 10.0f; // Maximum time before spawning a new enemy

    private void Start()
    {
        StartCoroutine(SpawnEnemyAtStart());
    }

    private IEnumerator SpawnEnemyAtStart()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        StartCoroutine(ManageEnemySpawn(enemy));
    }

    private IEnumerator ManageEnemySpawn(GameObject enemy)
    {
        // Wait for the max lifetime
        yield return new WaitForSeconds(maxEnemyLifetime);

        // Spawn a new enemy after max lifetime
        SpawnEnemy();

    }
}
