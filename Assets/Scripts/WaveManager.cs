using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Button startWaveButton;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private int baseEnemiesPerWave = 5;
    [SerializeField] private int enemyIncreasePerWave = 2;
    [SerializeField] private float timeBetweenEnemies = 0.5f;
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private TMP_Text waveCountText;

    private int currentWave = 0;
    private int aliveEnemies = 0;
    private bool isWaveActive = false;

    private void Awake()
    {
        waveCountText.text = $"Wave: {currentWave}";
        enemyCountText.text = $"Remaining Enemies: {enemiesPerWave}";
        startWaveButton.gameObject.SetActive(true);
        startWaveButton.onClick.AddListener(StartWave);
    }

    private void StartWave()
    {
        currentWave++;
        enemiesPerWave = baseEnemiesPerWave + (enemyIncreasePerWave * (currentWave - 1));

        waveCountText.text = $"Wave: {currentWave}";
        enemyCountText.text = $"Remaining Enemies: {enemiesPerWave}";

        startWaveButton.gameObject.SetActive(false);
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        isWaveActive = true;
        
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    private void SpawnEnemy()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Transform enemyObj = Instantiate(enemyPrefab, point.position, Quaternion.identity);

        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.OnDeath += OnEnemyKilled;

        aliveEnemies++;
    }

    private void OnEnemyKilled(object sender, System.EventArgs e)
    {
        aliveEnemies--;
        enemyCountText.text = $"Remaining Enemies: {aliveEnemies}";
        if (aliveEnemies <= 0)
        {
            isWaveActive = false;
            startWaveButton.gameObject.SetActive(true);
            Debug.Log($"Wave {currentWave} completed!");
        }
    }
}
