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
    [SerializeField] private float rotationSpeed = 5f;

    private float fireTimer;
    private List<Enemy> enemiesInRange = new List<Enemy>();
    private Quaternion defaultRotation;

    private void Awake()
    {
        defaultRotation = transform.rotation;
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;

        Enemy closestEnemy = GetClosestEnemy();

        if (closestEnemy != null)
        {
            RotateTowards(closestEnemy.transform.position);

            if (fireTimer <= 0)
            {
                FireProjectile(closestEnemy);
                fireTimer = fireCooldown;
            }
        }
        else
        {
            RotateToDefault();
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

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);
        Debug.DrawRay(transform.position, direction, Color.red);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void RotateToDefault()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation, rotationSpeed * Time.deltaTime);
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
