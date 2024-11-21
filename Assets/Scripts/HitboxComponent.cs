using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah objek yang mengenai adalah Bullet
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                Damage(bullet);
            }
        }
    }

    public void Damage(int damageAmount)
    {
        var invincibility = GetComponent<InvincibilityComponent>();

        if (invincibility == null || !invincibility.isInvincible)
        {
            health.Subtract(damageAmount);
        }
    }

   public void Damage(Bullet bullet)
    {
        var invincibility = GetComponent<InvincibilityComponent>();

        // Hanya kurangi health jika tidak sedang invincible
        if (invincibility == null || !invincibility.isInvincible)
        {
            
            health.Subtract(bullet.damage);
            if (invincibility != null)
            {
                invincibility.StartInvincibility();
            }
        }
    }
}