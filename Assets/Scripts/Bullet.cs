using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f; // Kecepatan bullet
    private IObjectPool<Bullet> pool;
    public int damage = 10;
    public void SetPool(IObjectPool<Bullet> objectPool)
    {
        pool = objectPool;
    }

    private void OnEnable()
    {
        // Mengatur kecepatan peluru saat aktif
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kembalikan bullet ke pool saat bertabrakan
        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
           // Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // Kembalikan bullet ke pool saat keluar layar
        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
