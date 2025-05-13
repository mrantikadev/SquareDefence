using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlowEffect : BulletEffectBase
{
    [SerializeField] private float slowAmount = 0.5f; // The amount to slow the enemy by (0.5 = 50% slower)
    [SerializeField] private float slowDuration = 2f; // The duration of the slow effect in seconds

    public override void Apply(Enemy enemy)
    {
        enemy.ApplySlow(slowAmount, slowDuration);
    }
}
