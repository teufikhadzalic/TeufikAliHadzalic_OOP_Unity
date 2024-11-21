using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    private PlayerMovement playerMovement;
    private Animator animator;
    

    public Weapon CurrentWeapon { get; private set; } // Senjata yang saat ini dipegang oleh pemain

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
}
