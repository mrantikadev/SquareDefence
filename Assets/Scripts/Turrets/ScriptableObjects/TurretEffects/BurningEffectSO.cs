using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretEffect/Burning")]
public class BurningEffectSO : TurretEffectSO
{
    public float damagePerSecond = 2f;
    public float duration = 3f;
    public GameObject burnEffectPrefab;
    
    public override void ApplyEffect(Enemy target, Vector3 sourcePosition)
    {
        target.StartCoroutine(Burn(target));
    }

    private IEnumerator Burn(Enemy target)
    {
        GameObject fireFX = null;

        if (burnEffectPrefab != null)
        {
            fireFX = Instantiate(burnEffectPrefab, target.transform.position, Quaternion.identity);
            fireFX.transform.SetParent(target.transform);
            //fireFX.transform.localPosition = Vector3.zero;
            Destroy(fireFX, duration);
        }

        SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            if (target == null) yield break;

            target.TakeDamage(damagePerSecond * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
