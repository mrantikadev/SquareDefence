using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform turretSpawnPoint;
    public Transform enemySpawnPoint;

    //[Header("Prefabs")]
    //public GameObject turretPrefab;
    //public GameObject enemyPrefab;

    [Header("Scriptable Objects")]
    public List<TurretSO> availableTurrets;
    public List<EnemySO> availableEnemies;

    [Header("UI")]
    public GameObject turretPanelObject;
    public GameObject enemyPanelObject;
    public Transform turretPanelContent;
    public Transform enemyPanelContent;
    public Button turretButtonPrefab;
    public Button enemyButtonPrefab;
    public Button turretToggleButton;
    public Button enemyToggleButton;

    private GameObject currentTurret;

    private void Start()
    {
        turretPanelObject.SetActive(false);
        enemyPanelObject.SetActive(false);

        turretToggleButton.onClick.AddListener(ToggleTurretPanel);
        enemyToggleButton.onClick.AddListener(ToggleEnemyPanel);
    }

    private void ToggleTurretPanel()
    {
        bool isActive = turretPanelObject.activeSelf;
        turretPanelObject.SetActive(!isActive);

        if (!isActive)
        {
            ClearChildren(turretPanelContent);
            BuildTurretButtons();
        }
    }

    private void ToggleEnemyPanel()
    {
        bool isActive = enemyPanelObject.activeSelf;
        enemyPanelObject.SetActive(!isActive);

        if (!isActive)
        {
            ClearChildren(enemyPanelContent);
            BuildEnemyButtons();
        }
    }

    private void BuildTurretButtons()
    {
        foreach (var turret in availableTurrets)
        {
            var button = Instantiate(turretButtonPrefab, turretPanelContent);
            button.GetComponentInChildren<TextMeshProUGUI>().text = turret.NameString;
            button.onClick.AddListener(() => SpawnTurret(turret));
        }
    }

    private void BuildEnemyButtons()
    {
        foreach (var enemy in availableEnemies)
        {
            var button = Instantiate(enemyButtonPrefab, enemyPanelContent);
            button.GetComponentInChildren<TextMeshProUGUI>().text = enemy.NameString;
            button.onClick.AddListener(() => SpawnEnemy(enemy));
        }
    }

    private void SpawnTurret(TurretSO config)
    {
        if (currentTurret != null)
            Destroy(currentTurret);

        currentTurret = Instantiate(config.TurretPrefab, turretSpawnPoint.position, Quaternion.identity);
        currentTurret.GetComponent<Turret>().Config = config;
    }

    private void SpawnEnemy(EnemySO config)
    {
        var enemy = Instantiate(config.Prefab, enemySpawnPoint.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().config = config;
    }

    private void ClearChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
}
