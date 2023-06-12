using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform[] spawnPoints; // The array of spawn points
    public int waveCount = 0; // The wave count

    private int enemiesPerWave = 10; // The number of enemies per wave
    private int enemiesSpawned = 0; // The number of enemies spawned in the current wave
    private float spawnInterval = 5f; // The time interval between enemy spawns
    private float waveCooldown = 15f; // The cooldown period between waves

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(waveCooldown);

        waveCount++;
        enemiesSpawned = 0;
        enemiesPerWave += 5;

        while (enemiesSpawned < enemiesPerWave)
        {
            if (enemiesSpawned < enemiesPerWave)
                SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

        StartCoroutine(SpawnWave());
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned to the EnemySpawner!");
            return;
        }

        if (enemiesSpawned >= enemiesPerWave)
        {
            Debug.LogWarning("Reached the enemy count limit for the wave!");
            return;
        }

        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        enemiesSpawned++;
    }
}