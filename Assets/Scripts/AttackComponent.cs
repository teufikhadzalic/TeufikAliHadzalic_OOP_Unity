using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah objek yang collide memiliki tag yang berbeda
        if (other.CompareTag(gameObject.tag))
        {
            return;
        }

        // Cek apakah objek yang collide memiliki HitboxComponent dan InvincibilityComponent
        var hitbox = other.GetComponent<HitboxComponent>();
        var invincibility = other.GetComponent<InvincibilityComponent>();

        if (hitbox != null)
        {
            if (invincibility != null)
            {
                // Mulai invincibility
                invincibility.StartInvincibility();
            }
            // Berikan damage jika objek tidak dalam kondisi invincible
            hitbox.Damage(damage);
        }
    }
}
