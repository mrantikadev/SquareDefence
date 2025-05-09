using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int activeTurretCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Debug.Log(activeTurretCount.ToString());
    }

    private void OnEnable()
    {
        Turret.OnTurretDestroyed += HandleTurretDeath;
    }

    private void OnDisable()
    {
        Turret.OnTurretDestroyed -= HandleTurretDeath;
    }

    private void HandleTurretDeath()
    {
        activeTurretCount--;

        if (activeTurretCount <= 0)
        {
            GameOver();
        }
    }

    public void RegisterNewTurret()
    {
        Debug.Log("RegisterNewTurret metodu");
        activeTurretCount++;
        Debug.Log(activeTurretCount);
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("Game Over");
    }
}
