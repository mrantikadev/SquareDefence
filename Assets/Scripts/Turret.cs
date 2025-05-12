using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretSO turretType;
    [SerializeField] private Transform muzzlePosition;
    [SerializeField] private EnemyDetector enemyDetector;

    protected List<Enemy> enemiesInRange = new List<Enemy>();
    protected Quaternion defaultRotation;

    private float fireRate;
    private float currentHitPoints;

    private void Awake()
    {
        GetComponentInChildren<CircleCollider2D>().radius = turretType.FireRange;
        currentHitPoints = turretType.HitPoints;
        defaultRotation = transform.rotation;
    }

    private void Update()
    {
        fireRate -= Time.deltaTime;

        CleanDeadEnemies();

        Enemy closestEnemy = GetClosestEnemy();

        if (closestEnemy != null)
        {
            RotateTowards(closestEnemy.transform.position);

            if (fireRate <= 0)
            {
                FireAt(closestEnemy);
                fireRate = turretType.FireRate;
            }
        }
        else
        {
            RotateBackToDefault();
        }
    }

    private void OnEnable()
    {
        if (enemyDetector != null)
        {
            enemyDetector.OnEnemyEnter += OnEnemyEnter;
            enemyDetector.OnEnemyExit += OnEnemyExit;
        }
    }

    private void OnDisable()
    {
        if (enemyDetector != null)
        {
            enemyDetector.OnEnemyEnter -= OnEnemyEnter;
            enemyDetector.OnEnemyExit -= OnEnemyExit;
        }
    }

    private void OnEnemyEnter(Enemy enemy)
    {
        if (!enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnEnemyExit(Enemy enemy)
    {
        if (enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }

    private void CleanDeadEnemies()
    {
        enemiesInRange.RemoveAll(enemy => enemy == null);
    }

    public void FireAt(Enemy target)
    {
        RotateTowards(target.transform.position);
        GameObject bullet = Instantiate(turretType.BulletType.BulletPrefab, muzzlePosition.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(target, turretType.BulletType);
    }

    public Enemy GetClosestEnemy()
    {
        if (enemiesInRange.Count == 0) return null;

        return enemiesInRange
            .OrderBy(enemy => Vector2.Distance(transform.position, enemy.transform.position))
            .FirstOrDefault();
    }

    public void RotateBackToDefault()
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            defaultRotation,
            turretType.RotationSpeed * Time.deltaTime
        );
    }

    public void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            turretType.RotationSpeed * Time.deltaTime
        );
    }

    public void TakeDamage(float amount)
    {
        currentHitPoints -= amount;

        if (currentHitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
