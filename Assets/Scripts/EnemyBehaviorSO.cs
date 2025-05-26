using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviorSO : ScriptableObject
{
    public abstract void OnDeath(Enemy enemy);
}
