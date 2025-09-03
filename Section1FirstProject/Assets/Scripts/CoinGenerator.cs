using System.Collections;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public GameObject coinPrefab;
    public Vector2 spawnStart;
    public float spawnDistance;
    public int numberOfCoins;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IEnumerator method = SpawnCoins();
        Debug.Log("Starting coin spawn...");
        StartCoroutine(method);
    }

    IEnumerator SpawnCoins()
    {
        while (numberOfCoins-- > 0)
        {
            Vector2 spawnPosition = spawnStart;
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity, transform);
            spawnStart += Vector2.right * spawnDistance;
            yield return new WaitForSeconds(1f);
        }
    }
}
