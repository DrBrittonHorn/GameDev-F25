using UnityEngine;

public class CoinController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            //GameObject.Find("Player")
            pc.coinsCollected++;
            Destroy(gameObject);
        }
    }
}
