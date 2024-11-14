using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private float speed = 3f;
    private Vector2 moveDirection;
    private float verticalOffset = 1.0f; // Offset vertikal untuk spawn lebih tinggi di layar

    protected override void Start()
    {
        base.Start();
        SetSpawnPosition();
    }

    private void SetSpawnPosition()
    {
        float screenEdge = Camera.main.orthographicSize * Camera.main.aspect;
        float screenVerticalLimit = Camera.main.orthographicSize - verticalOffset; // Batasi vertikal spawn di dalam layar

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
        // Gerakkan musuh secara horizontal
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Periksa jika musuh melewati tepi layar, lalu balikkan arah
        float screenEdge = Camera.main.orthographicSize * Camera.main.aspect;
        if (Mathf.Abs(transform.position.x) > screenEdge + 1)
        {
            moveDirection = -moveDirection;
        }
    }
}
