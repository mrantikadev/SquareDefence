using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretEffectSO : ScriptableObject
{
    public abstract void ApplyEffect(Enemy target);
}
