using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO config;
    private float currentHealth;

    private void Start()
    {
        currentHealth = config.Health;
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
