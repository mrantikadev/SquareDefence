using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IEnemy
{
    public BaseEnemySO type;
    private float currentHealth;
    private float speed;

    private void Awake()
    {
        currentHealth = type.HitPoint;
        speed = type.Speed;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < -6) Die();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0) Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Turret"))
        {
            Turret turret = collision.gameObject.GetComponent<Turret>();

            if (turret != null)
            {
                turret.TakeDamage(type.Damage);
                Die();
            }
        }
    }
}
