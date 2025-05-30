using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO config;
    private float currentHealth;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHealth = config.Health;
    }

    private void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, -config.Speed);

        if (transform.position.y <= -20)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        foreach(var behavior in config.Behaviors)
        {
            behavior.OnDeath(this);
        }

        Destroy(gameObject);
    }
}
