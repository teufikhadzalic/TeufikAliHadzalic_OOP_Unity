using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f; // kecepatan maksimum
    [SerializeField] private float accelerationTime = 0.5f; // waktu menuju kecepatan penuh
    [SerializeField] private float decelerationTime = 0.5f; // waktu menuju berhenti
    [SerializeField] private float stopThreshold = 0.1f; // ambang batas berhenti

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Rigidbody2D rb;
    private float accelerationRate;
    private float decelerationRate;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D tidak ditemukan pada GameObject Player.");
            return;
        }

        rb.gravityScale = 0;

        accelerationRate = maxSpeed / accelerationTime;
        decelerationRate = maxSpeed / decelerationTime;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        Vector2 targetVelocity = moveDirection * maxSpeed;

        if (moveDirection != Vector2.zero)
        {
            moveVelocity = Vector2.MoveTowards(moveVelocity, targetVelocity, accelerationRate * Time.deltaTime);
        }
        else
        {
            moveVelocity = Vector2.MoveTowards(moveVelocity, Vector2.zero, decelerationRate * Time.deltaTime);
            
            if (moveVelocity.magnitude < stopThreshold)
            {
                moveVelocity = Vector2.zero;
            }
        }

        rb.velocity = moveVelocity;

        ApplyPositionConstraints();
    }

    private void ApplyPositionConstraints()
    {
        Vector3 pos = transform.position;
        Vector3 minBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 maxBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        pos.x = Mathf.Clamp(pos.x, minBounds.x + 0.5f, maxBounds.x - 0.5f);
        pos.y = Mathf.Clamp(pos.y, minBounds.y + 0.5f, maxBounds.y - 0.5f);

        transform.position = pos;
    }

    public bool IsMoving()
    {
        return moveVelocity.magnitude > stopThreshold;
    }
}
