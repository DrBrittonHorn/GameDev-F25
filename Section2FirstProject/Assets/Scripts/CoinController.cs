using UnityEngine;

public class CoinController : MonoBehaviour
{
    //public GameObject player;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //update coins collected
            collision.GetComponent<PlayerController>().coinsCollected++;
            //PlayerController pc = collision.GetComponent<PlayerController>();
            //GameObject.Find("Player").GetComponent<PlayerController>().coinsCollected++; 
            //destroy coin
            Destroy(gameObject);
        }
    }
}
