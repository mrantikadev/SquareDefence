using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SniperTurret : Turret
{
    private float fireCooldown;
    private CircleCollider2D circleColllider;

    private void Awake()
    {
        circleColllider = GetComponent<CircleCollider2D>();
        currentHealth = type.HitPoint;
        fireCooldown = type.FireCooldown;
        defaultRotation = transform.rotation;
    }

    private void Start()
    {
        circleColllider.radius = type.FireRange;
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        BaseEnemy closestEnemy = base.GetClosestEnemy();

        if (closestEnemy != null)
        {
            base.RotateTowards(closestEnemy.transform.position);

            if (fireCooldown <= 0)
            {
                FireAt(closestEnemy);
                fireCooldown = type.FireCooldown;
            }
        }
        else
        {
            base.RotateBackToDefault();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            BaseEnemy enemy = collision.GetComponent<BaseEnemy>();

            if (enemy != null && !enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
            }
        }
    }
}
