using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretEffect/PushBack")]
public class PushBackEffectSO : TurretEffectSO
{
    public float force;

    public override void ApplyEffect(Enemy target, Vector3 sourcePosition)
    {
        Vector2 direction = (target.transform.position - sourcePosition).normalized;
        target.ApplyPushBack(direction * force);
    }
}
