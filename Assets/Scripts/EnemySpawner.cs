using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = defaultSpawnCount; // Inisialisasi jumlah spawn default
    }

    public void stopSpawning()
    {
        isSpawning = false; // Menghentikan proses spawning
    }

    public void startSpawning()
    {
        if (spawnedEnemy.level <= combatManager.waveNumber)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    public IEnumerator SpawnEnemies()
    {
        if (isSpawning)
        {
            if (spawnCount == 0)
            {
                spawnCount = defaultSpawnCount;
            }
            int i = spawnCount;
            while (i > 0)
            {
                Enemy enemy = Instantiate(spawnedEnemy); // Spawn enemy
                enemy.GetComponent<Enemy>().enemySpawner = this; // Hubungkan enemy dengan spawnernya
                enemy.GetComponent<Enemy>().combatManager = combatManager; // Hubungkan dengan CombatManager
                --i;
                spawnCount = i;

                if (combatManager != null)
                {
                    combatManager.totalEnemies++; // Update totalEnemies di CombatManager
                    combatManager.UpdateUI(); // Perbarui UI jumlah musuh
                }

                yield return new WaitForSeconds(spawnInterval); // Tunggu sebelum spawn berikutnya
            }
        }
    }

    public void onDeath()
    {
        Debug.Log("Enemy Killed");
        // Panggil metode ini saat musuh terbunuh
        totalKill++;
        ++totalKillWave;
        Debug.Log(totalKillWave);

        // Cek apakah totalKillWave mencapai minimum untuk meningkatkan spawn count
        if (totalKillWave == minimumKillsToIncreaseSpawnCount)
        {
            Debug.Log("Increasing spawn count");
            totalKillWave = 0; // Reset totalKillWave untuk wave baru
            defaultSpawnCount *= spawnCountMultiplier; // Tingkatkan defaultSpawnCount
            if (spawnCountMultiplier < 3)
                spawnCountMultiplier += multiplierIncreaseCount; // Tingkatkan multiplier
            spawnCount = defaultSpawnCount; // Perbarui spawnCount
        }
    }
}
