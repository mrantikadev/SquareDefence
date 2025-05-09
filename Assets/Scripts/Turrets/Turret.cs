using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Turret : MonoBehaviour, ITurret
{
    public static event Action OnTurretDestroyed;

    [SerializeField] protected TurretSO type;

    protected List<BaseEnemy> enemiesInRange = new List<BaseEnemy>();
    protected Quaternion defaultRotation;
    protected float currentHealth;

    private void Awake()
    {
        GameManager.Instance.RegisterNewTurret();
        Debug.Log("RegisterNewTurret metodu - Turret");
        currentHealth = type.HitPoint;
        defaultRotation = transform.rotation;
    }

    public void Die()
    {
        OnTurretDestroyed?.Invoke();
        Destroy(gameObject);
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

        return enemiesInRange
            .OrderBy(enemy => Vector2.Distance(transform.position, enemy.transform.position))
            .FirstOrDefault();
    }

    public void RotateBackToDefault()
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            defaultRotation,
            type.RotationSpeed * Time.deltaTime
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
            type.RotationSpeed * Time.deltaTime
        );
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
