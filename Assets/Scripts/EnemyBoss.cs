using UnityEngine;

public class EnemyBoss : Enemy
{
    public float speed = 5f;
    public Weapon weapon; // Komponen Weapon untuk menembak
    private Vector3 direction;
    private Vector3 spawnPoint;
    private AttackComponent attackComponent;
    
    void Start()
    {   
         attackComponent = GetComponent<AttackComponent>();
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        
        // Tentukan posisi spawn dan arah berdasarkan sisi layar
        if (Random.value > 0.5f)
        {
            spawnPoint = new Vector3(-screenBounds.x + 1, Random.Range(-screenBounds.y + 3f, screenBounds.y), 0);
            direction = Vector3.right;
        }
        else
        {
            spawnPoint = new Vector3(screenBounds.x - 1, Random.Range(-screenBounds.y + 3f, screenBounds.y), 0);
            direction = Vector3.left;
        }
        
        transform.position = spawnPoint;
        weapon = GetComponent<Weapon>(); // Inisialisasi weapon

        // Kunci rotasi agar tetap horizontal
        transform.rotation = Quaternion.identity;
    }
    

    void Update()
    {
        // Gerakkan musuh secara horizontal
        transform.position += direction * speed * Time.deltaTime;

        // Periksa batas layar untuk membalik arah jika sudah melewati
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        if (transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x)
        {
            direction = -direction;
        }

        // Tetap atur rotasi agar tidak miring
        transform.rotation = Quaternion.identity;

        // Panggil metode untuk menembak
        Shoot();
    }

    void Shoot()
    {
        if (weapon != null)
        {
            weapon.Shoot(); // Gunakan metode untuk menembak pada komponen weapon
        }
    }
}
