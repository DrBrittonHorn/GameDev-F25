using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class TilemapMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    GameObject[] boxes;
    Transform[] goals;
    public Tilemap unwalkable;
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
        //rb.linearVelocity = inputVector * 5f;
        // check for movement in both directions (skip it)
        if (inputVector.x != 0 && inputVector.y != 0) return;
        if (isValidMove(inputVector))
        {
            transform.position += (Vector3)inputVector;
        }
    }

    private bool isValidMove(Vector2 direction)
    {
        if (unwalkable.HasTile(unwalkable.WorldToCell(transform.position + (Vector3)direction)))
        {
            return false;
        }
        // foreach (GameObject box in boxes)
        // if (box.canMove(direction) == false) return false;
            return true;
    }
}
