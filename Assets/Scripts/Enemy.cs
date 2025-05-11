using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySO enemyType;
    private float currentHitPoints;

    private void Awake()
    {
        currentHitPoints = enemyType.HitPoints;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * enemyType.Speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Turret"))
        {
            Turret turret = collision.gameObject.GetComponent<Turret>();

            if (turret != null)
            {
                turret.TakeDamage(enemyType.Damage);
            }

            Die();
        }
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
