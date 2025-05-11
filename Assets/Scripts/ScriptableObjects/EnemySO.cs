using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Entities/Enemies/Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] [TextArea] private string description;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float hitPoints;
    [SerializeField] private float damage;

    public string EnemyName => enemyName;
    public string Description => description;
    public GameObject EnemyPrefab => enemyPrefab;
    public float Speed => speed;
    public float HitPoints => hitPoints;
    public float Damage => damage;
}
