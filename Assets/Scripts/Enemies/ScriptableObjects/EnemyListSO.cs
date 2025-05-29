using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lists/EnemyList")]
public class EnemyListSO : ScriptableObject
{
    public List<EnemySO> list;
}
