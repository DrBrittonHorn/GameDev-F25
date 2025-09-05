
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public float movement;
    private bool isJumping;
    public float jumpHeight;
    public LayerMask groundLayer;
    Rigidbody2D rb;
    [SerializeField]
    private float speed;
    private Vector2 startPosition;
    public int coinsCollected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement * speed, rb.linearVelocity.y);
        //Debug.Log("Current Velocity: " + rb.linearVelocity.magnitude);
        if (isJumping)
        {
            rb.AddForce(Vector2.up * jumpHeight);
            isJumping = false;
        }
    }

    void OnMove(InputValue value)
    {
        //Debug.Log("Move action triggered: " + value.Get<Vector2>());
        movement = value.Get<float>();
    }

    void OnJump(InputValue value)
    {
        if (!isJumping && value.isPressed && isGrounded())
        {
            isJumping = true;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        if (hit.collider != null)
        {
            //Debug.Log("Grounded on: " + hit.collider.name);
            isJumping = false;
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered by: " + collision.name);
        if (collision.CompareTag("Collectible"))
        {
            coinsCollected++;
            Debug.Log("Coins Collected: " + coinsCollected);
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.collider.name);
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit by an enemy!");
            Respawn();
        }
        if (collision.collider.CompareTag("Respawn"))
        {
            Debug.Log("Fell off the level!");
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
        isJumping = false;
        movement = 0f;
    }
}
