using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TilemapController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>();
        rb.linearVelocity = movementVector * moveSpeed;
    }

    void OisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);    
    }
}
