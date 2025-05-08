using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseEnemy", menuName = "Entities/Enemies/BaseEnemySO")]
public class BaseEnemySO : EntitySO
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    public float Damage => damage;
    public float Speed => speed;
}
