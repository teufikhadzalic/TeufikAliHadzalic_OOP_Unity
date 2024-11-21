using UnityEngine;
using System.Collections;
public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    private void Start()
    {
        spawnCount = defaultSpawnCount;
    }

    public void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnEnemies());
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
                combatManager.totalEnemies++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave++;

        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            spawnCount += spawnCountMultiplier;
        }
    }
}
