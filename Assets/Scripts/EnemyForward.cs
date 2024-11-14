using UnityEngine;

public class EnemyForward : Enemy
{
    [SerializeField] private float speed = 3f;
    private Vector2 moveDirection = Vector2.down; // Gerakan default ke bawah
    private float horizontalOffset = 1.0f; // Offset horizontal untuk spawn di dalam layar
    private float bottomLimit; // Batas bawah untuk menghancurkan musuh

    protected override void Start()
    {
        base.Start();
        SetSpawnPosition();

        // Kunci rotasi agar tetap vertikal tanpa miring
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }

        // Tentukan batas bawah layar
        bottomLimit = -Camera.main.orthographicSize - 1;
    }

    private void SetSpawnPosition()
    {
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float screenHeight = Camera.main.orthographicSize;

        // Spawn di bagian atas layar pada posisi horizontal acak
        transform.position = new Vector2(Random.Range(-screenWidth + horizontalOffset, screenWidth - horizontalOffset), screenHeight + 1);

        // Set rotasi agar tetap vertikal
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        // Gerakkan musuh lurus ke bawah
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Hapus musuh jika sudah melewati batas bawah layar
        if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
        }
    }

}
