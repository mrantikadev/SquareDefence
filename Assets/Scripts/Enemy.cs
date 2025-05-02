using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 20;
    [SerializeField] private int damage = 5;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with a turret
        if (collision.gameObject.CompareTag("Turret"))
        {
            // Deal damage to the turret
            Turret turret = collision.gameObject.GetComponent<Turret>();
            if (turret != null)
            {
                turret.TakeDamage(damage);
            }
            // Destroy the enemy after collision
            Destroy(gameObject);
        }
    }

    private void Die()
    {
        // Handle enemy death (e.g., play animation, drop loot, etc.)
        Destroy(gameObject);
    }
}
