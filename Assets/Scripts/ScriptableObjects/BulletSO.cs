using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Bullets/Bullet")]
public class BulletSO : ScriptableObject
{
    [SerializeField] private string bulletName;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    [SerializeReference] private List<BulletEffectBase> bulletEffects;

    public string BulletName => bulletName;
    public GameObject BulletPrefab => bulletPrefab;
    public float Speed => speed;
    public float Damage => damage;
    public List<BulletEffectBase> BulletEffects => bulletEffects;
}
