using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager instance;
	public GrindSystem grind;
    private void Awake()
    {
		grind = GetComponent<GrindSystem>();
        if (instance != null)
        {
            Debug.LogError("More than one buildmanager in scene!");
            return;
        }
        instance = this;
	}

    public GameObject standardBarricadePrefab;
    public GameObject standardAcherTowerPrefab;
	public GameObject standardHutTowerPrefab;
	public GameObject standardBarraksTowerPrefab;
	public GameObject standardAppleFarmPrefab;
	public GameObject standardWallPrefab;



	public GameObject soldierPrefab;
	public GameObject archerPrefab;
	public GameObject workerPrefab;
	public GameObject catapultPrefab;

	public static GameObject soldier;
	public static GameObject archer;
	public static GameObject worker;
	public static GameObject catapult;

	public GameObject sigh;
	public Text warningtext;

	public TowerBlueprints towerToBuild;
    public UnitBlueprints unitToBuild;
	public GameObject spawnPoint;
	public float count = 0;


	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			towerToBuild = null;
		}
		if (count <= 0)
		{
			sigh.SetActive(false);
		}
		if (sigh.activeSelf == true)
		{
			count -= Time.deltaTime;
		}
	}

	public void WarningText(string t)
	{
		warningtext.text = t;
		if (sigh.activeSelf == false)
		{
			sigh.SetActive(true);
		}
		count = 2;
	}

	public void TowerToBuild()
	{
		if (towerToBuild.prefab.name == "Wall")
		{
			GameObject building1 = Instantiate(towerToBuild.prefab);
			GameObject building2 = Instantiate(towerToBuild.prefab);
			GameObject building3 = Instantiate(towerToBuild.prefab);
			grind.SetBuildingWall(building1, building2, building3);
		}
		else
		{
			GameObject building = Instantiate(towerToBuild.prefab);
			grind.SetBuilding(building);
		}
		
	}
    public void SelectTowerToBuild(TowerBlueprints tower)
    {
		if (PlayerStats.Wood < tower.wood)
		{
			WarningText("Not enought wood");
			return;
		}
		if (PlayerStats.Gold < tower.gold)
		{
			WarningText("Not enought gold");
			return;
		}
		if (PlayerStats.Food < tower.food)
		{
			WarningText("Not enought food");
			return;
		}
		if (PlayerStats.Stone < tower.stone)
		{
			WarningText("Not enought stone");
			return;
		}
		else
		{
			towerToBuild = tower;
			TowerToBuild();
		}
	}


    public void BuildUnitOn()
    {
        if (PlayerStats.Food < unitToBuild.food)
        {
			WarningText("Not enought food");
            return;
        }
		if (PlayerStats.Gold < unitToBuild.gold)
		{
			WarningText("Not enought gold");
			return;
		}
		if (PlayerStats.Wood < unitToBuild.wood)
		{
			WarningText("Not enought wood");
			return;
		}
		if (PlayerStats.Stone < unitToBuild.stone)
		{
			WarningText("Not enought stone");
			return;
		}
		if (PlayerStats.Popnow >= PlayerStats.Maxpop)
		{
			WarningText("Not enought people");
			return;
		}
		if (spawnPoint.GetComponent<buildingSelected>().UnitQueue.Count > 11)
		{
			WarningText("building unit list is full");
			return;
		}
		PlayerStats.Gold -= unitToBuild.gold;
		PlayerStats.Wood -= unitToBuild.wood;
		PlayerStats.Food -= unitToBuild.food;
		PlayerStats.Stone -= unitToBuild.stone;
		if (unitToBuild.prefab == workerPrefab)
		{
			spawnPoint.GetComponent<buildingSelected>().UnitQueue.Add(unitToBuild);
		}
		else
		{
			spawnPoint.GetComponent<buildingSelected>().UnitQueue.Add(unitToBuild);
		}
		
		unitToBuild = null;
    }

	public void SelectUnitToBuild(UnitBlueprints unit)
    {
        unitToBuild = unit;
        BuildUnitOn();
    }
	public void BuildingToBuild()
	{
		PlayerStats.Gold -= towerToBuild.gold;
		PlayerStats.Wood -= towerToBuild.wood;
		PlayerStats.Food -= towerToBuild.food;
		PlayerStats.Stone -= towerToBuild.stone;
		towerToBuild = null;
	}
	public static void GameStatsUpdateUnits(GameObject unit)
	{
		if (unit.name == "Player_Unit_Soldier")
		{
			GameStats.SoldiersAmount += 1;
		}
		if (unit.name == "Player_Unit_Archer")
		{
			GameStats.ArchersAmount += 1;
		}
		if (unit.name == "Player_Worker")
		{
			GameStats.WorkerAmount += 1;
		}
		if (unit.name == "Player_Unit_Catapult")
		{
			GameStats.CatapultAmount += 1;
		}
		GameStats.UnitsAmount += 1;
	}
}
