using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnRangeX = 2.5f;

    private void Start()
    {
        InvokeRepeating("TestSpawn", 1f, spawnRate);
    }

    private void TestSpawn()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), transform.position.y);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
