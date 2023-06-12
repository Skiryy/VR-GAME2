using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int initialWaveSize = 10;
    public int additionalEnemiesPerWave = 5;
    public float spawnInterval = 5f;
    public float waveCooldown = 15f;
    private int currentWaveSize;
    public int waveCount;
    private bool isSpawning;
    private int aliveEnemiesCount;

    private void Start()
    {
        currentWaveSize = initialWaveSize;
        waveCount = 1;
        isSpawning = false;
        aliveEnemiesCount = 0;

        StartWave();
    }

    private void StartWave()
    {
        isSpawning = true;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentWaveSize; i++)
        {
            SpawnEnemy();
            aliveEnemiesCount++;
            yield return new WaitForSeconds(spawnInterval);
        }

        while (aliveEnemiesCount > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(waveCooldown);

        currentWaveSize += additionalEnemiesPerWave;
        waveCount++;
        StartWave();
    }

    private void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[spawnPointIndex].position;
        Quaternion spawnRotation = Quaternion.identity;
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        Rat_Enemy enemyScript = enemy.GetComponent<Rat_Enemy>();
        enemyScript.OnEnemyDeath += OnEnemyDeath;
        enemyScript.OnEnemyDeath += DecrementAliveEnemiesCount;
    }

    private void OnEnemyDeath()
    {
        if (aliveEnemiesCount == 0)
        {
            isSpawning = false;
            StopCoroutine(SpawnWave());
        }
    }

    private void DecrementAliveEnemiesCount()
    {
        aliveEnemiesCount--;
    }
}