using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySO enemyType;
    private float currentHitPoints;
    private float originalSpeed;
    private float currentSpeed;
    private Coroutine slowRoutine;

    private void Awake()
    {
        currentHitPoints = enemyType.HitPoints;
        originalSpeed = enemyType.Speed;
        currentSpeed = originalSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);

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

    public void ApplySlow(float slowAmount, float slowDuration)
    {
        if (slowRoutine != null)
        {
            StopCoroutine(slowRoutine);
        }

        slowRoutine = StartCoroutine(SlowDownRoutine(slowAmount, slowDuration));
    }

    private IEnumerator SlowDownRoutine(float slowAmount, float slowDuration)
    {
        currentSpeed = originalSpeed * (1 - slowAmount);
        yield return new WaitForSeconds(slowDuration);
        currentSpeed = originalSpeed;
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
