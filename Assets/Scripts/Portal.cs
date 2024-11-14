using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotateSpeed = 90f;
    private Vector2 newPosition;
    private Animator animator;

    void Start()
    {
        ChangePosition(); // Mengatur posisi tujuan awal
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Gerakkan portal menuju newPosition
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        
        // Putar portal
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        // Jika portal dekat dengan posisi baru, atur posisi baru
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        // Aktifkan atau nonaktifkan collider dan sprite berdasarkan senjata pada Player
        if (GameObject.Find("Player").GetComponentInChildren<Weapon>() != null)
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aktifkan elemen UI terkait saat portal disentuh oleh Player
            foreach (Transform child in GameManager.Instance.transform)
            {
                if (child.GetComponent<Canvas>() != null || child.GetComponent<UnityEngine.UI.Image>() != null)
                {
                    child.gameObject.SetActive(true);
                }
            }

            GameManager.Instance.LevelManager.LoadScene("Main"); // Pindah ke scene "Main"
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        Debug.Log("Posisi tujuan baru: " + newPosition); // Log untuk memeriksa posisi baru
    }
}
