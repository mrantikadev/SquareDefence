using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyDetector : MonoBehaviour
{
    public event Action<Enemy> OnEnemyEnter;
    public event Action<Enemy> OnEnemyExit;

    private readonly HashSet<Enemy> detectedEnemies = new HashSet<Enemy>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            detectedEnemies.Add(enemy);
            OnEnemyEnter?.Invoke(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            detectedEnemies.Remove(enemy);
            OnEnemyExit?.Invoke(enemy);
        }
    }

    public List<Enemy> GetDetectedEnemies()
    {
        detectedEnemies.RemoveWhere(e => e == null);
        return new List<Enemy>(detectedEnemies);
    }
}
