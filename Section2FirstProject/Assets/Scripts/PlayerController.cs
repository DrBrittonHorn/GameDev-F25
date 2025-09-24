
using TMPro;
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
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public int coinsCollected = 0;
    public TMP_Text coinText;
    public GameObject pausePanel;
    public GameObject followMe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (rb.linearVelocityX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.linearVelocityX > 0)
        {
            spriteRenderer.flipX = false;
        }
        // tons of mods
        //rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, speed);
        //Debug.Log("velocity: " + rb.linearVelocity.magnitude);
        animator.SetFloat("xVelo", Mathf.Abs(rb.linearVelocityX));
        animator.SetFloat("yVelo", rb.linearVelocityY);
        coinText.text = "Coins Collected: " + coinsCollected;
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

    void OnMouseMove(InputValue value)
    {
        Vector2 mousePos = value.Get<Vector2>();
        Debug.Log("mouse at: " + mousePos);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        followMe.transform.position = worldPos /* where is my mouse */;
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

    /*void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered by " + collision.name);
        if (collision.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
        }
    }*/

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

    void OnPause()
    {
        Debug.Log("Game Paused");
        // bring up pause menu
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Debug.Log("Game Resumed");
        // close pause menu
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void OnBurst(InputValue value)
    {
        if (value.isPressed)
        {
            followMe.GetComponent<ParticleSystem>().Play();
        }
    }
}
