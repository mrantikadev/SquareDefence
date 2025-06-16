using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretBehavior/Laser")]
public class LaserBehaviorSO : TurretBehaviorSO
{
    public float laserWidth = 0.2f;
    public float damagePerSecond = 5f;

    public override void Fire(Turret turret, Enemy target)
    {
        if (turret == null || target == null) return;

        Vector3 direction = (target.transform.position - turret.transform.position).normalized;
        Vector3 origin = turret.FirePoint.position;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, turret.Config.Range, LayerMask.GetMask("Enemy"));

        if (turret.LaserLine != null)
        {
            turret.LaserLine.enabled = true;
            turret.LaserLine.SetPosition(0, origin);
            turret.LaserLine.SetPosition(1, origin + direction * turret.Config.Range);
        }

        foreach (RaycastHit2D hit in hits)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damagePerSecond * Time.deltaTime);
                turret.Config.Effect?.ApplyEffect(enemy, hit.point);
            }
        }
    }
}
