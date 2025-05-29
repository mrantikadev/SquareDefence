using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private string nameString;
    [SerializeField] private float health;
    [SerializeField] private float speed;
	[SerializeField] private GameObject prefab;
    [SerializeField] private List<EnemyBehaviorSO> behaviors;

    public string NameString => nameString;
    public float Health => health;
    public float Speed => speed;
    public GameObject Prefab => prefab;
    public List<EnemyBehaviorSO> Behaviors => behaviors;
}
