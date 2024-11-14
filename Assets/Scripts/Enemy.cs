using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int level = 1; // Level dari enemy, bisa diatur di Inspector
    protected Rigidbody2D rb;
    protected Transform player;

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

    protected void FacePlayer()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            transform.right = direction; // Menghadap ke arah player
        }
    }
}
