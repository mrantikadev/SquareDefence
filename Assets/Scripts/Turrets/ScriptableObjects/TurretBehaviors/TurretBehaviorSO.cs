using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBehaviorSO : ScriptableObject
{
    public abstract void Fire(Turret turret, Enemy target);
}
