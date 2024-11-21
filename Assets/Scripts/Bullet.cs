using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f; // Kecepatan bullet
    public int damage = 10; // Damage bullet
    private IObjectPool<Bullet> pool;

    // Properti owner
    public GameObject owner;

    public void SetPool(IObjectPool<Bullet> objectPool)
    {
        pool = objectPool;
    }

    private void OnEnable()
    {
        // Atur kecepatan peluru saat aktif
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Abaikan jika peluru mengenai owner
        if (collision.gameObject == owner)
        {
            return;
        }

        // Jika mengenai objek lain, proses damage
        var hitbox = collision.gameObject.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            hitbox.Damage(damage);
        }

        // Kembalikan peluru ke pool
        if (pool != null)
        {
            pool.Release(this);
        }
    }

    

    private void OnBecameInvisible()
    {
        // Kembalikan peluru ke pool saat keluar layar
        if (pool != null)
        {
            pool.Release(this);
        }
    }
}
