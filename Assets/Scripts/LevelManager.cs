using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Awake()
    {
        // Matikan animator pada saat inisialisasi
        animator.enabled = false;
    }

    public void LoadScene(string sceneName)
    {
        // Memulai coroutine untuk memuat scene secara asinkron
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Aktifkan animator agar transisi dapat diputar
        animator.enabled = true;

        // Jika animator tidak null, aktifkan transisi (opsional)
        if (animator != null)
        {
            // animator.SetTrigger("Start"); // Gunakan jika diperlukan
        }

        // Tunggu beberapa saat untuk memberi waktu pada animasi transisi
        yield return new WaitForSeconds(1f); // Sesuaikan waktu tunggu dengan durasi animasi

        // Mulai proses pemuatan scene secara asinkron
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Setel trigger pada animator setelah memulai pemuatan scene
        animator.SetTrigger("Start");

        // Tunggu hingga proses pemuatan scene selesai
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
