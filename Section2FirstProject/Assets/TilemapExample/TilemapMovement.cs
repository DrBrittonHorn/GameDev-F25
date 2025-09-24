using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TilemapMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        rb.linearVelocity = inputVector * 5f;
    }
}
