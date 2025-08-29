using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movement;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * 5f;
    }

    void OnMove(InputValue value)
    {
        Debug.Log("Move action triggered: " + value.Get<Vector2>());
        movement = value.Get<Vector2>();
    }
}
