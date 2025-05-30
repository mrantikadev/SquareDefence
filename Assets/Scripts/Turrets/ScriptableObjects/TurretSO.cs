using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turrets/Turret")]
public class TurretSO : ScriptableObject
{
    [Header("Turret Info")]
    [SerializeField] private string nameString;

    [Header("Turret Stats")]
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float rotationSpeed;

    [Header("Prefabs")]
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Effects and Behaviors")]
    [SerializeField] private TurretEffectSO effect;
    [SerializeField] private TurretBehaviorSO behavior;

    [Header("Targeting Settings")]
    [SerializeField] private float lockOnDelay;
    [SerializeField] private float rotationTolerance;

    public string NameString => nameString;
    public float FireRate => fireRate;
    public float Damage => damage;
    public float Range => range;
    public float BulletSpeed => bulletSpeed;
    public float RotationSpeed => rotationSpeed;
    public GameObject TurretPrefab => turretPrefab;
    public GameObject BulletPrefab => bulletPrefab;
    public TurretEffectSO Effect => effect;
    public TurretBehaviorSO Behavior => behavior;
    public float LockOnDelay => lockOnDelay;
    public float RotationTolerance => rotationTolerance;
}
