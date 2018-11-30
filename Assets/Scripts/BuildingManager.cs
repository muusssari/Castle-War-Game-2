using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one buildmanager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject standardBarricadePrefab;
    public GameObject standardAcherTowerPrefab;
	

	public GameObject soldierPrefab;
	public GameObject archerPrefab;
	public GameObject minerPrefab;
	public GameObject catapultPrefab;

	public TowerBlueprints towerToBuild;
    public UnitBlueprints unitToBuild;
    public Transform bases;

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			towerToBuild = null;
		}
	}


	public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasGold { get { return PlayerStats.Gold >= towerToBuild.cost; } }

    public void BuildTowerOn(Platform platform)
    {
        if (PlayerStats.Gold < towerToBuild.cost)
        {
            Debug.Log("Not enought gold");
            return;
        }
        if (towerToBuild.prefab == standardBarricadePrefab && !platform.road)
        {
            Debug.Log("Cant build here");
            return;
        }
        if (platform.tower != null) {
            return;
        }
        
        GameObject tower = (GameObject)Instantiate(towerToBuild.prefab, platform.GetBuildPosition(), Quaternion.identity);
        platform.tower = tower;
		PlayerStats.Gold -= towerToBuild.cost;
    }
    public void SelectTowerToBuild(TowerBlueprints tower)
    {
        towerToBuild = tower;
    }


    public void BuildUnitOn()
    {
        if (PlayerStats.Gold < unitToBuild.cost)
        {
            Debug.Log("Not enought gold");
            return;
        }
        PlayerStats.Gold -= unitToBuild.cost;
        Instantiate(unitToBuild.prefab, bases.transform.position, Quaternion.identity);
        unitToBuild = null;
    }
    public void SelectUnitToBuild(UnitBlueprints unit)
    {
        unitToBuild = unit;
        BuildUnitOn();
    }
}
