using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletSO bulletType;
    private Enemy target;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, bulletType.Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(bulletType.Damage);

                foreach (var effect in bulletType.BulletEffects)
                {
                    effect.Apply(enemy);
                }
            }

            Destroy(gameObject);
        }
    }

    public void Initialize(Enemy target, BulletSO bulletType)
    {
        this.target = target;
        this.bulletType = bulletType;
    }
}
