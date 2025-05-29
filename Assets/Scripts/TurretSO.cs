using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turrets/Turret")]
public class TurretSO : ScriptableObject
{
    [SerializeField] private string nameString;
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private TurretEffectSO effect;
    [SerializeField] private TurretBehaviorSO behavior;

    public string NameString => nameString;
    public float FireRate => fireRate;
    public float Damage => damage;
    public float Range => range;
    public float BulletSpeed => bulletSpeed;
    public GameObject TurretPrefab => turretPrefab;
    public GameObject BulletPrefab => bulletPrefab;
    public TurretEffectSO Effect => effect;
    public TurretBehaviorSO Behavior => behavior;
}
