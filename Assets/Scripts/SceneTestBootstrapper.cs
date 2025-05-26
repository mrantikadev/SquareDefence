using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTestBootstrapper : MonoBehaviour
{
    public TurretSO turretConfig;
    public GameObject turretPrefab;
    public GameObject enemyPrefab;

    private void Start()
    {
        GameObject turret = Instantiate(turretPrefab, new Vector3(0, -4, 0), Quaternion.identity);
        Turret turretScript = turret.GetComponent<Turret>();
        turretScript.Config = turretConfig;

        Transform firePoint = turret.transform.Find("FirePoint");
        if (firePoint != null)
            firePoint.forward = Vector3.up;

        Instantiate(enemyPrefab, new Vector3(0, 7, 0), Quaternion.identity);
    }
}
