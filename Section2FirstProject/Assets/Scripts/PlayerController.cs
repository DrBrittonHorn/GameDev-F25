
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public float movement;
    private bool isJumping;
    public LayerMask groundLayer;
    Rigidbody2D rb;
    [SerializeField]
    private float speed;
    public float jumpForce;
    private Vector2 spawnLoc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnLoc = transform.position;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement * speed, rb.linearVelocity.y);
        if (isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJumping = false;
        }
        // tons of mods
        //rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, speed);
        //Debug.Log("velocity: " + rb.linearVelocity.magnitude);
    }

    void OnMove(InputValue value)
    {
        //Debug.Log("move: " + value.Get<Vector2>());
        movement = value.Get<float>();
    }

    void OnJump(InputValue value)
    {
        Debug.Log("jumping");
        if (!isJumping && value.isPressed && isGrounded())
        {
            isJumping = true;
        }

    }

    bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered by " + collision.name);
        if (collision.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //if (collision.collider.GetType() == typeof(EdgeCollider2D))
            if (collision.collider is EdgeCollider2D)
            {
                Debug.Log("Landed on enemy");
                collision.gameObject.SetActive(false);
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("Ouch!");
                Respawn();
            }
        }
        if (collision.collider.CompareTag("Respawn"))
        {
            Debug.Log("You fell!");
            Respawn();
        }
        if (collision.collider.CompareTag("Finish"))
        {
            Debug.Log("You win!");
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = spawnLoc;
        rb.linearVelocity = Vector2.zero;
        isJumping = false;
        movement = 0;

        // foreach enemy in respawn handler
        // set active = true;
    }
}
