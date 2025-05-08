using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SniperTurret : MonoBehaviour, ITurret
{
    public TurretSO type;
    private float fireCooldown;
    private float currentHealth;
    private Quaternion defaultRotation;
    private List<BaseEnemy> enemiesInRange = new List<BaseEnemy>();
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

        BaseEnemy closestEnemy = GetClosestEnemy();

        if (closestEnemy != null)
        {
            RotateTowards(closestEnemy.transform.position);

            if (fireCooldown <= 0)
            {
                FireAt(closestEnemy);
                fireCooldown = type.FireCooldown;
            }
        }
        else
        {
            RotateBackToDefault();
        }
    }

    public void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            type.RotationSpeed * Time.deltaTime
        );
    }

    public void RotateBackToDefault()
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            defaultRotation,
            type.RotationSpeed * Time.deltaTime
        );
    }

    public void FireAt(BaseEnemy target)
    {
        GameObject projectile = Instantiate(type.ProjectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetValues(type.BulletSpeed, type.BulletDamage);
        projectile.GetComponent<Projectile>().Initialize(target);
    }

    public BaseEnemy GetClosestEnemy()
    {
        enemiesInRange = enemiesInRange.Where(enemy => enemy != null).ToList();

        if (enemiesInRange.Count == 0) return null;

        Debug.Log("Enemies in range: " + enemiesInRange.Count);
        return enemiesInRange
            .OrderBy(enemy => Vector2.Distance(transform.position, enemy.transform.position))
            .FirstOrDefault();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, type.FireRange);
    //}

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
