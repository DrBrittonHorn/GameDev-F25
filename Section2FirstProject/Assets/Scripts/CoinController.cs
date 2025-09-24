using UnityEngine;

public class CoinController : MonoBehaviour
{
    float lifeTime = 2f;
    float startTime = 10000000f;
    void Update()
    {
        if (Time.time - startTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }
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
            GetComponent<ParticleSystem>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            startTime = Time.time;
            //Destroy(gameObject);
        }
    }
}
