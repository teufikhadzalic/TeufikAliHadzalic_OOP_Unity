using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    [SerializeField] private Bullet bulletPrefab; // Prefab peluru
    [SerializeField] private Transform bulletSpawnPoint; // Posisi spawn peluru

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> bulletPool;
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    private float timer;

    private void Awake()
    {
        // Inisialisasi pool untuk bullet
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );
    }

    private void Update()
    {
        // Menghitung waktu untuk interval penembakan
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            timer = 0f;
            Shoot();
        }
    }

    private Bullet CreateBullet()
    {
        // Membuat bullet baru dan menetapkan referensi ke pool
        Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBullet.SetPool(bulletPool);
        return newBullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        // Aktifkan bullet saat diambil dari pool dan posisikan di spawn point
        bullet.gameObject.SetActive(true);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        // Nonaktifkan bullet saat dikembalikan ke pool
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        // Hancurkan bullet jika dikeluarkan dari pool
         Destroy(bullet.gameObject);
    }

    public void Shoot()
    {
        Bullet spawnedBullet = bulletPool.Get();
        // Mengatur arah dan kecepatan bullet
        Rigidbody2D rb = spawnedBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = bulletSpawnPoint.up * spawnedBullet.bulletSpeed;
        }
    }
}