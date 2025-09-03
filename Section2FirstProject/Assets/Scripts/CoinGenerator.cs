using System.Collections;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public GameObject coinPrefab;
    public Vector2 spawnPos;
    public float spawnDistance;
    public int coinsRemaining;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IEnumerator method = SpawnCoins();
        StartCoroutine(method);
    }

    IEnumerator SpawnCoins()
    {
        while (coinsRemaining-- > 0)
        {
            GameObject newObj = Instantiate(coinPrefab, spawnPos, Quaternion.identity, transform);
            newObj.name = "Coin" + (coinsRemaining + 1);
            spawnPos += Vector2.right * spawnDistance;
            yield return new WaitForSeconds(1f);
        }
    }
}
