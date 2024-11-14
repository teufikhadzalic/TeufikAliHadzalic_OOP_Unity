using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder; // Senjata yang akan muncul di pickup
    private Weapon weapon;

    void Awake()
    {
        if (weaponHolder != null)
        {
            // Membuat instance dari weaponHolder untuk digunakan sebagai pickup
            weapon = Instantiate(weaponHolder);
        }
    }

    void Start()
    {
        if (weapon != null)
        {
            // Inisialisasi semua komponen terkait dengan status tidak aktif
            TurnVisual(false);

            // Menempatkan weapon sebagai child dari pickup dan mengatur posisinya
            weapon.transform.SetParent(transform, false);
            weapon.transform.localPosition = Vector3.zero; // Menetapkan posisi pada pickup
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (weapon.transform.parent == transform && other.CompareTag("Player"))
        {
            // Mendapatkan senjata yang saat ini dipegang oleh Player
            Weapon currentWeapon = other.GetComponentInChildren<Weapon>();
            if (currentWeapon != null)
            {
                // Menghancurkan senjata lama agar tidak stay di dalam scene
                Destroy(currentWeapon.gameObject);
            }

            // Menetapkan senjata baru ke Player
            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = new Vector3(0, -0.05f, 0); // Mengatur posisi senjata pada Player
            TurnVisual(true);

            Debug.Log("Senjata berhasil diambil oleh Player.");
        }
    }

    void TurnVisual(bool state)
    {
        if (weapon != null)
        {
            // Menghidupkan atau mematikan semua komponen MonoBehaviour pada weapon
            foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
            {
                component.enabled = state;
            }

            // Menghidupkan atau mematikan komponen Animator
            Animator animator = weapon.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.enabled = state;
            }

            // Menghidupkan atau mematikan komponen Renderer
            foreach (var renderer in weapon.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = state;
            }
        }
    }

    void TurnVisual(bool state, Weapon specificWeapon)
    {
        if (specificWeapon != null)
        {
            // Menghidupkan atau mematikan semua komponen MonoBehaviour pada senjata yang spesifik
            foreach (var component in specificWeapon.GetComponentsInChildren<MonoBehaviour>())
            {
                component.enabled = state;
            }

            // Menghidupkan atau mematikan komponen Animator
            Animator animator = specificWeapon.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.enabled = state;
            }

            // Menghidupkan atau mematikan komponen Renderer
            foreach (var renderer in specificWeapon.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = state;
            }
        }
    }
}
