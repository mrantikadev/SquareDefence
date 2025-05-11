using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Entities/Turrets/Turret")]
public class TurretSO : ScriptableObject
{
    [SerializeField] private string turretName;
    [SerializeField] [TextArea] private string description;
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private float fireRange;
    [SerializeField] private float fireRate;
    [SerializeField] private BulletSO bulletType;
    [SerializeField] private float hitPoints;
    [SerializeField] private float rotationSpeed;

    public string TurretName => turretName;
    public string Description => description;
    public GameObject TurretPrefab => turretPrefab;
    public float FireRange => fireRange;
    public float FireRate => fireRate;
    public BulletSO BulletType => bulletType;
    public float HitPoints => hitPoints;
    public float RotationSpeed => rotationSpeed;
}
