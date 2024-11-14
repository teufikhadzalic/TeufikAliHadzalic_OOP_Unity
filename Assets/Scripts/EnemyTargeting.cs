using UnityEngine;

public class EnemyTargeting : Enemy
{
    private float speed = 3f;
    private Vector2 moveDirection;
    private float horizontalOffset = 1.0f; // Offset untuk posisi horizontal spawn

    protected override void Start()
    {
        base.Start();
        SetInitialPosition();
    }

    private void SetInitialPosition()
    {
        float screenEdge = Camera.main.orthographicSize * Camera.main.aspect;
        float screenVerticalLimit = Camera.main.orthographicSize - horizontalOffset;

        if (Random.value < 0.5f)
        {
            // Spawn di sisi kiri dan bergerak ke kanan
            transform.position = new Vector2(-screenEdge - 1, Random.Range(-screenVerticalLimit, screenVerticalLimit));
            moveDirection = Vector2.right;
        }
        else
        {
            // Spawn di sisi kanan dan bergerak ke kiri
            transform.position = new Vector2(screenEdge + 1, Random.Range(-screenVerticalLimit, screenVerticalLimit));
            moveDirection = Vector2.left;
        }

        // Pastikan orientasi musuh tetap horizontal
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (player != null)
        {
            // Gerakkan musuh secara horizontal dengan kecepatan yang menargetkan posisi vertikal Player
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            Vector2 combinedDirection = new Vector2(moveDirection.x, directionToPlayer.y);

            // Pergerakan yang menggabungkan arah horizontal dan vertikal menuju player
            transform.Translate(combinedDirection * speed * Time.deltaTime);

            // Memeriksa jika musuh melewati tepi layar secara horizontal dan membalikkan arah horizontal
            float screenEdge = Camera.main.orthographicSize * Camera.main.aspect;
            if (Mathf.Abs(transform.position.x) > screenEdge + 1)
            {
                moveDirection = -moveDirection;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Hapus musuh saat bertabrakan dengan Player
        }
    }
}
