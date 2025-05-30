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
    private Quaternion initialRotation;
    private float lockOnTimer = 0f;

    private void Awake()
    {
        initialRotation = transform.rotation;
    }

    private void Start()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        Enemy target = GetClosestEnemy();

        if (target != null)
        {
            RotateTowardsTarget(target.transform.position);

            float angleDifference = Quaternion.Angle(transform.rotation, GetTargetRotation(target.transform.position));

            if (angleDifference < Config.RotationTolerance)
            {
                lockOnTimer += Time.deltaTime;

                if (fireCooldown <= 0f && lockOnTimer >= Config.LockOnDelay)
                {
                    Config.Behavior.Fire(this, target);
                    fireCooldown = Config.FireRate;
                    lockOnTimer = 0f;
                }
            }
            else
            {
                lockOnTimer = 0f;
            }
        }
        else
        {
            RotateBackToDefault();
            lockOnTimer = 0f;
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

    private void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            Config.RotationSpeed * Time.deltaTime
        );
    }

    private void RotateBackToDefault()
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            initialRotation,
            Config.RotationSpeed * Time.deltaTime
        );
    }

    private Quaternion GetTargetRotation(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.Euler(0, 0, angle);
    }
}
