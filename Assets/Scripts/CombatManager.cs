using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 0;
    public int totalEnemies = 0;

    [Header("UI References")]
    [SerializeField] private UnityEngine.UI.Text waveText;
    [SerializeField] private UnityEngine.UI.Text enemiesText;
    [SerializeField] private UnityEngine.UI.Text pointsText;

    private int points = 0;

    void Start()
    {
        waveNumber = 0;
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.combatManager = this;
        }
        UpdateUI();
    }

    void Update()
    {
        if (totalEnemies <= 0)
        {
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                timer = 0;
                StartNextWave();
            }
        }
    }

    private void StartNextWave()
    {
        timer = 0;
        waveNumber++;
        UpdateUI();

        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.startSpawning();
        }
    }

    public void onDeath(Enemy enemy)
    {
        totalEnemies--;
        points += enemy.level; // Tambahkan poin berdasarkan level musuh
        UpdateUI();
    }

    // Ubah metode ini menjadi public agar dapat diakses dari EnemySpawner
    public void UpdateUI()
    {
        waveText.text = "Wave: " + waveNumber;
        enemiesText.text = "Enemies: " + totalEnemies;
        pointsText.text = "Points: " + points;
    }
}
