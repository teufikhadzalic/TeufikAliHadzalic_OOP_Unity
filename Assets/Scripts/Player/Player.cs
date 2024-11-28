using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private PlayerMovement playerMovement;
    private Animator animator;

    public Weapon CurrentWeapon { get; private set; } // Senjata yang saat ini dipegang oleh pemain
    public int health = 100; // Health default pemain
    public int points = 0; // Poin pemain berdasarkan musuh yang dikalahkan

    // Referensi ke UI
    [Header("UI References")]
    [SerializeField] private Text healthText;
    [SerializeField] private Text pointsText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        // Inisialisasi UI
        UpdateHealthUI();
        UpdatePointsUI();
    }

    void Update()
    {
        // Pergerakan pemain dilakukan setiap frame
        playerMovement.Move();
    }

    // Metode untuk mengganti senjata yang dipegang
    public void EquipWeapon(Weapon newWeapon)
    {
        if (CurrentWeapon != null)
        {
            Destroy(CurrentWeapon.gameObject); // Hapus senjata lama
        }

        CurrentWeapon = newWeapon;
        CurrentWeapon.transform.SetParent(transform, false);
        CurrentWeapon.transform.localPosition = Vector3.zero;
        CurrentWeapon.transform.localRotation = Quaternion.identity;
    }

    public bool IsMoving()
    {
        return playerMovement.IsMoving();
    }

    // Metode untuk menerima damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthUI(); // Perbarui UI health

        if (health <= 0)
        {
            Die();
        }
    }

    // Metode untuk menambahkan poin
    public void AddPoints(int enemyLevel)
    {
        points += enemyLevel;
        UpdatePointsUI(); // Perbarui UI points
    }

    // Metode untuk menghapus pemain (jika health = 0)
    private void Die()
    {
        Debug.Log("Player died!");
        // Tambahkan logika kematian (contoh: reload scene, tampilkan menu game over, dll.)
    }

    // Metode untuk memperbarui UI health
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {health}";
        }
    }

    // Metode untuk memperbarui UI points
    private void UpdatePointsUI()
    {
        if (pointsText != null)
        {
            pointsText.text = $"Points: {points}";
        }
    }
}
