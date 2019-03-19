using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Master : MonoBehaviour {

	private GameObject tower;
	private GameObject units;
	private GameObject workers;
	private GameObject upgrade;
	private GameObject doScreen;
	private GameObject WorkerInfo;
	private GameObject queue;
	public GameObject onOperation = null;
	public Text selectedBuilding;
	public GameObject navMesh;
	public bool d = false;
	public GameObject list;

	void Start ()
	{
		workers = transform.Find("BottomImage/bottomSlider/Workers").gameObject;
		tower = transform.Find("BottomImage/bottomSlider/Tower_shop").gameObject;
		units = transform.Find("BottomImage/bottomSlider/Unit_shop").gameObject;
		upgrade = transform.Find("Upgrade_shop").gameObject;
		doScreen = transform.Find("BottomImage/bottomSlider/DoScreen").gameObject;
		WorkerInfo = transform.Find("BottomImage/bottomSlider/WorkerInfo").gameObject;
		list = GameObject.Find("MouseSelecting");
		queue = transform.Find("BottomImage/bottomSlider/Queue").gameObject;
	}
	public void WorkerInfoScreen(bool a, GameObject unit)
	{
		WorkerInfo.SetActive(a);
		if (unit != null)
		{
			WorkerInfo.GetComponent<WorkerInfoScript>().Infos(unit.GetComponent<Miner>().gold, unit.GetComponent<Miner>().wood, unit.GetComponent<Miner>().food, unit.GetComponent<Miner>().stone);
		}
	}
	public void QueueShop(bool a)
	{
		queue.SetActive(a);
	}
	public void WorkersShop(bool a)
	{
		workers.SetActive(a);
	}
	public void UpgradeShop(bool a)
	{
		upgrade.SetActive(a);
	}
	public void TowerShop(bool a)
	{
		tower.SetActive(a);
	}
	public void UnitShop(bool a)
	{
		units.SetActive(a);
	}
	public void DoScreen(bool a)
	{
		doScreen.SetActive(a);
	}
	public void DestroyObject()
	{
		Destroy(onOperation);
		UpgradeShop(false);
		DoScreen(false);
		d = true;
		if (gameObject.name == "Hut(Clone)")
		{
			PlayerStats.Maxpop -= 10;
		}
	}
	public void BuildingName(string a)
	{
		selectedBuilding.text = a;
	}
	private void Update()
	{
		
		if (list.GetComponent<Unit_Selected>().Buildings.Count > 0)
		{
			WorkerInfoScreen(false, null);
			onOperation = list.GetComponent<Unit_Selected>().Buildings[0];

			if (onOperation.name == "PlayerBase" && onOperation.GetComponent<BuildingInProgress>().building == true)
			{
				QueueShop(true);
				WorkersShop(true);
				BuildingName("Base");
				DoScreen(false);
				UnitShop(false);
			}
			else if (onOperation.name == "Barraks(Clone)" && onOperation.GetComponent<BuildingInProgress>().building == true)
			{
				QueueShop(true);
				UnitShop(true);
				DoScreen(true);
				BuildingName("Barraks");
				WorkersShop(false);
				
			}
			else if (onOperation.name == "Tower(Clone)" && onOperation.GetComponent<BuildingInProgress>().building == true)
			{
				DoScreen(true);
				BuildingName("Tower");
				WorkersShop(false);
				UnitShop(false);
				WorkersShop(false);
				QueueShop(false);
			}
			else if (onOperation.name == "AppleFarm(Clone)" && onOperation.GetComponent<BuildingInProgress>().building == true)
			{
				DoScreen(true);
				BuildingName("Apple Farm");
				WorkersShop(false);
				UnitShop(false);
				WorkersShop(false);
				QueueShop(false);
			}
			else if (onOperation.name == "Forge(Clone)" && onOperation.GetComponent<BuildingInProgress>().building == true)
			{
				UpgradeShop(true);
				DoScreen(true);
				BuildingName("Forge");
				WorkersShop(false);
				UnitShop(false);
				WorkersShop(false);
				QueueShop(false);
			}
			else
			{
				onOperation = null;
				BuildingName("");
				DoScreen(false);
				UnitShop(false);
				UpgradeShop(false);
				WorkersShop(false);
				QueueShop(false);
			}
		}
		else if (list.GetComponent<Unit_Selected>().Units.Count > 0)
		{
			DoScreen(false);
			UnitShop(false);
			UpgradeShop(false);
			WorkersShop(false);
			QueueShop(false);

			onOperation = list.GetComponent<Unit_Selected>().Units[0];
			if (onOperation.name == "Player_Worker(Clone)")
			{
				WorkerInfoScreen(true, onOperation);
				BuildingName("Worker");
			}
			else
			{
				WorkerInfoScreen(false, null);
				BuildingName("");
			}
		}
		else
		{
			WorkerInfoScreen(false, null);
			onOperation = null;
			BuildingName("");
			DoScreen(false);
			UnitShop(false);
			UpgradeShop(false);
			WorkersShop(false);
			QueueShop(false);
		}

		if (list.GetComponent<Unit_Selected>().Units.Count > 0)
		{
			foreach (GameObject unit in list.GetComponent<Unit_Selected>().Units)
			{
				if (unit.name == "Player_Worker(Clone)")
				{
					TowerShop(true);
				}
				else
				{
					return;
				}
			}
		}
		else
		{
			TowerShop(false);
		}
	}

	private void LateUpdate()
	{
		if (d)
		{
			if (navMesh == null)
			{
				return;
			}
			else
			{
				navMesh.GetComponent<navmeshbaker>().RemakeNavMeshOnDestroy();
				d = false;
			}
		}
		if (list.GetComponent<Unit_Selected>().Buildings.Count <= 0)
		{
			DoScreen(false);
		}
	}
}
