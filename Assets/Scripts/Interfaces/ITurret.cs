using System.Linq;
using UnityEngine;

public interface ITurret : IEntity<TurretSO>
{
    void RotateTowards(Vector3 targetPosition);
    void RotateBackToDefault();
    void FireAt(BaseEnemy target);
    BaseEnemy GetClosestEnemy();
}
