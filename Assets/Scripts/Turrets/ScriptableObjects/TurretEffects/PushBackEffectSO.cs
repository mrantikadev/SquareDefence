using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretEffect/PushBack")]
public class PushBackEffectSO : TurretEffectSO
{
    //public float force;

    public override void ApplyEffect(Enemy target, Vector3 sourcePosition)
    {
        var pushable = target.GetComponent<IPushable>();
        if (pushable == null) return;

        Vector2 direction = (target.transform.position - sourcePosition).normalized;
        pushable.ApplyPushBack(sourcePosition);
    }
}
