
using TMPro;
using Unity.VisualScripting;
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
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public TMP_Text coinText;
    public GameObject pausePanel;
    public GameObject followObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (rb.linearVelocityX < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (rb.linearVelocityX > 0)
        {
            spriteRenderer.flipX = false;
        }

        //update xVelo animator value
        animator.SetFloat("xVelo", Mathf.Abs(rb.linearVelocityX));
        animator.SetFloat("yVelo", rb.linearVelocityY);
        coinText.text = "Coins Collected: " + coinsCollected;
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

    void OnMouseMovement(InputValue value)
    {
        Vector2 mousePos = value.Get<Vector2>();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        followObj.transform.position = worldPos /* mouse position */;
        Debug.Log("Mouse position: " + mousePos);
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
        //Debug.Log("Triggered by: " + collision.name);
        if (collision.CompareTag("Collectable"))
        {
            //coinsCollected++;
            //Debug.Log("Coins Collected: " + coinsCollected);
            //Destroy(collision.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided with: " + collision.collider.name);
        if (collision.collider.CompareTag("Enemy"))
        {
            //if (collision.otherCollider.GetType().Equals(typeof(EdgeCollider2D)))
            Debug.Log(collision.collider.GetType());
            if (collision.collider is EdgeCollider2D)
            {
                Debug.Log("Jumped on an enemy!");
                collision.gameObject.SetActive(false);
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("Hit by an enemy!");
                Respawn();
            }
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

    void OnPause()
    {
        Debug.Log("Pause action triggered");
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Debug.Log("Resume button clicked");
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    void OnBurst(InputValue value)
    {
        if (value.isPressed)
        {
            followObj.GetComponent<ParticleSystem>().Play();
        }
        
    }
}
