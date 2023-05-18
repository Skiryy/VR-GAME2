using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 10f;
    public int maxEnemies = 10;

    private int currentEnemies = 0;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (currentEnemies < maxEnemies)
            {
                SpawnEnemy();
                currentEnemies++;
            }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void DecreaseEnemyCount()
    {
        currentEnemies--;
    }
}