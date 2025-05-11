using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesToSpawnList;
    [SerializeField] private float spawnRangeX;
    [SerializeField] private float spawnInterval;


    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), transform.position.y);
            yield return new WaitForSeconds(spawnInterval);
            int randomIndex = Random.Range(0, enemiesToSpawnList.Count);
            GameObject enemyPrefab = enemiesToSpawnList[randomIndex];
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
