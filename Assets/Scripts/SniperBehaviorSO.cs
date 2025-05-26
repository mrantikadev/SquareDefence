using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretBehavior/Sniper")]
public class SniperBehaviorSO : TurretBehaviorSO
{
    public override void Fire(Turret turret, Enemy target)
    {
        Debug.Log("Sniper turret");
    }
}
