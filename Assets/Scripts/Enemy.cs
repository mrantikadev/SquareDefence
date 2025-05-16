using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySO enemyType;
    [SerializeField] private Transform particleEffectPrefab;
    private float currentHitPoints;
    private float originalSpeed;
    private float currentSpeed;
    private float originalRotationSpeed;
    private float rotationSpeed;
    private Coroutine slowRoutine;

    private void Awake()
    {
        currentHitPoints = enemyType.HitPoints;
        originalSpeed = enemyType.Speed;
        currentSpeed = originalSpeed;
        originalRotationSpeed = enemyType.RotationSpeed;
        rotationSpeed = originalRotationSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime, Space.World);
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

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
        rotationSpeed = originalRotationSpeed * (1 - slowAmount);
        yield return new WaitForSeconds(slowDuration);
        currentSpeed = originalSpeed;
        rotationSpeed = originalRotationSpeed;
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
        Transform particleEffet = Instantiate(particleEffectPrefab, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
        Destroy(particleEffet.gameObject, 0.6f);
        Destroy(gameObject);
    }
}
