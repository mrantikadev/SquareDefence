using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private Transform muzzlePosition;
    [SerializeField] private Transform bulletPrefab;

    private void Update()
    {
        // Check for input to fire the projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        Transform bullet = Instantiate(bulletPrefab, muzzlePosition.position, Quaternion.identity);
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
}
