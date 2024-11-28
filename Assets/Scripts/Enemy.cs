using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int level = 1; // Level dari enemy, bisa diatur di Inspector
    protected Rigidbody2D rb;
    protected Transform player;

    public EnemySpawner enemySpawner;
    
    public CombatManager combatManager;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Tidak terpengaruh oleh gravitasi
        player = GameObject.FindWithTag("Player").transform; // Temukan posisi player berdasarkan tag
    }

    protected virtual void Start()
    {
        FacePlayer();
    }
      private void OnDestroy()
    {
        if (enemySpawner != null && combatManager != null)
        {
            enemySpawner.onDeath();
            combatManager.onDeath(this); // Pass the enemy instance to onDeath
        }
    }
    protected void FacePlayer()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            transform.right = direction; // Menghadap ke arah player
        }
    }
}
