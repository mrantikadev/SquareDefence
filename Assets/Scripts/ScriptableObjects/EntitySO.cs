using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySO : ScriptableObject
{
    [SerializeField] private string nameString;
    [TextArea]
    [SerializeField]
    private string description;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float hitPoint;

    public string NameString => nameString;
    public string Description => description;
    public GameObject Prefab => prefab;
    public float HitPoint => hitPoint;
}
