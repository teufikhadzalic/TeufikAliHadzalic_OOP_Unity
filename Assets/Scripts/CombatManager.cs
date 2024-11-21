using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    private void Start()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.combatManager = this;
        }

        StartWave();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waveInterval && totalEnemies <= 0)
        {
            timer = 0;
            waveNumber++;
            StartWave();
        }
    }

    private void StartWave()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.StartSpawning();
        }
    }

    public void OnEnemyKilled()
    {
        totalEnemies--;

        if (totalEnemies <= 0)
        {
            foreach (var spawner in enemySpawners)
            {
                spawner.StopSpawning();
            }
        }
    }
}
