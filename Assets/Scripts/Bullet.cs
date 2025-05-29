using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private Vector3 direction;
    private float speed;
    private TurretEffectSO effect;

    public void SetDamage(float dmg) => damage = dmg;
    public void SetDirection(Vector3 dir) => direction = dir;
    public void SetEffect(TurretEffectSO eff) => effect = eff;
    public void SetSpeed(float spd) => speed = spd;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            effect?.ApplyEffect(enemy);
        }

        Destroy(gameObject);
    }
}
