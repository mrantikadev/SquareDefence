using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyBehaviors/Explode")]
public class ExplodeOnDeathSO : EnemyBehaviorSO
{
    public override void OnDeath(Enemy enemy)
    {
        Debug.Log("Enemy exploded on death!");
    }
}
