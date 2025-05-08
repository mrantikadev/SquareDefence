using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BaseEnemy target;
    private float speed;
    private float damage;

    public void Initialize(BaseEnemy target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetValues(float bulletSpeed, float bulletDamage)
    {
        speed = bulletSpeed;
        damage = bulletDamage;
    }
}
