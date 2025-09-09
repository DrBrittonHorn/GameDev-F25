using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    bool isSetForDestruction = false;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy_Barrier"))
        {
            speed = -speed;
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isSetForDestruction)
        {
            Debug.Log("Player hit an enemy!");
            // if collider type is EdgeCollider2D
            // isSetForDestruction = true;
        }
    }
}
