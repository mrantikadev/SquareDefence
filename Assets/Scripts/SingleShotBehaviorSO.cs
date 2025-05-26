using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretBehavior/Single Shot")]
public class SingleShotBehaviorSO : TurretBehaviorSO
{
    public override void Fire(Turret turret, Enemy target)
    {
        if (target == null || turret == null) return;

        Vector3 direction = (target.transform.position - turret.transform.position).normalized;

        GameObject bulletGO = Object.Instantiate(
            turret.Config.BulletPrefab,
            new Vector3(turret.FirePoint.position.x, turret.FirePoint.position.y, 0f),
            Quaternion.identity
        );

        bulletGO.transform.localScale = Vector3.one;

        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if (bullet != null)
        {
            bullet.SetDamage(turret.Config.Damage);
            bullet.SetEffect(turret.Config.Effect);
            bullet.SetDirection(direction);
        }
    }
}
