using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bugEnemyPrefab;
    public Transform[] spawnPoints;
    public int initialWaveSize = 10;
    public int additionalEnemiesPerWave = 5;
    public float spawnInterval = 5f;
    public float waveCooldown = 15f;
    private int currentWaveSize;
    public int waveCount;
    private bool isSpawning;
    private int aliveEnemiesCount;
    public TextMeshProUGUI waveText;
    public float baseWeight = 10f;
    public float currentWeight = 10f;
    public float weightIncrease = 5f;
    private float ratWeight = 2f;
    private float bugWeight = 1f;
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
        waveText.text = "Wave:" + waveCount;
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentWaveSize; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

        while (aliveEnemiesCount > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(waveCooldown);

        currentWaveSize += additionalEnemiesPerWave;
        baseWeight += weightIncrease;
        currentWeight = baseWeight;
        waveCount++;
        StartWave();
    }

    private void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[spawnPointIndex].position;
        Quaternion spawnRotation = Quaternion.identity;
        GameObject enemy = null;
        GameObject bugEnemy = null;

        if (currentWeight > 0)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
                currentWeight -= ratWeight;
                aliveEnemiesCount++;
            }
            else if (rand == 1)
            {
                bugEnemy = Instantiate(bugEnemyPrefab, spawnPosition, spawnRotation);
                currentWeight -= bugWeight;
                //enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
                //currentWeight -= ratWeight;;
                aliveEnemiesCount++;
            }

            if (enemy != null)
            {
                Rat_Enemy enemyScript = enemy.GetComponent<Rat_Enemy>();
                enemyScript.OnEnemyDeath += OnEnemyDeath;
                enemyScript.OnEnemyDeath += DecrementAliveEnemiesCount;
            }

            if (bugEnemy != null)
            {
                Bug_Enemy bugEnemyScript = bugEnemy.GetComponent<Bug_Enemy>();
                bugEnemyScript.OnEnemyDeath += OnEnemyDeath;
                bugEnemyScript.OnEnemyDeath += DecrementAliveEnemiesCount;
            }
        }
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