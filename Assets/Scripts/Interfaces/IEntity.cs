using UnityEngine;

public interface IEntity<T> where T : class
{
    void TakeDamage(float amount);

    void Die();
}
