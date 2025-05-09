using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseTurret", menuName = "Entities/Turrets/BaseTurret")]
public class TurretSO : EntitySO
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float fireRange;
    [SerializeField] private float fireCooldown;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float rotationSpeed;

    public float BulletSpeed => bulletSpeed;
    public float BulletDamage => bulletDamage;
    public float FireRange => fireRange;
    public float FireCooldown => fireCooldown;
    public GameObject ProjectilePrefab => projectilePrefab;
    public float RotationSpeed => rotationSpeed;
}

    //public string turretName;
    //public float range;
    //public float fireRate;
    //public GameObject projectilePrefab;
    //public TargetingStrategy targetingStrategy;
    //public DamageType damageType; // Enum: Bullet, Laser, Slow