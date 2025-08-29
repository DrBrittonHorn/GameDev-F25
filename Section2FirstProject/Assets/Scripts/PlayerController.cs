
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }
}
