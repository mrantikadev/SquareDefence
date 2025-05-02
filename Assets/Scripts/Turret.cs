using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private Transform muzzlePosition;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private float fireCooldown = 1f;

    private float fireTimer;
    private List<Enemy> enemiesInRange = new List<Enemy>();

    private void Update()
    {
        fireTimer -= Time.deltaTime;

        Enemy closestEnemy = GetClosestEnemy();
        if (closestEnemy != null && fireTimer <= 0f)
        {
            FireProjectile(closestEnemy);
            fireTimer = fireCooldown;
        }
    }

    private void FireProjectile(Enemy target)
    {
        Transform bullet = Instantiate(bulletPrefab, muzzlePosition.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetTarget(target.transform);
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private Enemy GetClosestEnemy()
    {
        enemiesInRange = enemiesInRange.Where(e => e != null).ToList(); //Cleanup
        if (enemiesInRange.Count == 0) return null;

        return enemiesInRange
            .OrderBy(e => Vector2.Distance(transform.position, e.transform.position))
            .FirstOrDefault();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && !enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy);
            }
        }
    }
}
