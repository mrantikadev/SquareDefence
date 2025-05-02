using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private int damage = 10;
    private Transform target;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy the bullet if no target is set
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        Destroy(gameObject, 5f);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
