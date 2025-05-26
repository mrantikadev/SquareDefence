using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public TurretSO Config;
    public Transform FirePoint;

    private float fireCooldown;
    private List<Enemy> enemiesInRange = new List<Enemy>();
    private Collider2D rangeCollider;

    private void Start()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        Enemy target = GetClosestEnemy();
        if (target != null && fireCooldown <= 0f)
        {
            Config.Behavior.Fire(this, target);
            fireCooldown = Config.FireRate;
        }
    }

    private Enemy GetClosestEnemy()
    {
        enemiesInRange.RemoveAll(e => e == null);

        Enemy closest = null;
        float closestDistance = float.MaxValue;

        foreach (var enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemiesInRange.Remove(enemy);
        }
    }
}
