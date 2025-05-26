using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretEffect/Burning")]
public class BurningEffectSO : TurretEffectSO
{
    public float damagePerSecond = 2f;
    public float duration = 3f;

    public override void ApplyEffect(Enemy target)
    {
        target.StartCoroutine(Burn(target));
    }

    private IEnumerator Burn(Enemy target)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            if (target == null) yield break;

            target.TakeDamage(damagePerSecond * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
