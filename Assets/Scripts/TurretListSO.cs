using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lists/TurretList")]
public class TurretListSO : ScriptableObject
{
    public List<TurretSO> list;
}
