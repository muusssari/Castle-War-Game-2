using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class buildingSelected : MonoBehaviour {

	public bool Selected = false;
	public bool MakeUnits = false;
	public bool WaypointActive = false;
	public GameObject waypoint;
	public GameObject ring;
	public GameObject canvas;
	public float range;
	public GameObject spawn;
	public BuildingManager buildingManager;
	public float count = 10;
	private float countReset;
	public bool makingUnits = false;
	public List<UnitBlueprints> UnitQueue = new List<UnitBlueprints>();
	public GameObject Queuelist;
	public GameObject spawned;
	public GameObject buildingOrder;

	private void Start()
	{
		if (MakeUnits)
		{
			waypoint = transform.Find("SpawnPoint/Waypoint").gameObject;
		}
		ring = transform.Find("SelectRing/Panel").gameObject;
		canvas = transform.Find("SelectRing").gameObject;
		buildingManager = BuildingManager.instance;
		countReset = count;
		Queuelist = GameObject.Find("ShopUI/BottomImage/bottomSlider/Queue");
	}

	private void Update()
	{
		if (UnitQueue.Count > 0)
		{
			makingUnits = true;
		}
		else
		{
			makingUnits = false;
		}
		if (MakeUnits)
		{
			if (makingUnits)
			{
				float procent = Mathf.Clamp(count, 0, countReset);
				float ab = ((procent / countReset) * 100);
				Queuelist.GetComponent<UnitQueue>().UnitProcent(Mathf.Floor(ab));
				if (count <= 0)
				{
					if (PlayerStats.Popnow < PlayerStats.Maxpop)
					{
						spawned = Instantiate(UnitQueue[0].prefab, spawn.transform.position, Quaternion.identity) as GameObject;
						BuildingManager.GameStatsUpdateUnits(UnitQueue[0].prefab);
						PlayerStats.Popnow += 1;
						UnitQueue.Remove(UnitQueue[0]);
						count = countReset;
					}
					spawned.GetComponent<Unit_Moving>().TargetDestination(waypoint);
					if (spawned.name == "Player_Worker(Clone)" && buildingOrder != null)
					{
						if (buildingOrder.transform.root.gameObject.tag == "Tree")
						{
							spawned.GetComponent<Miner>().targetTree = buildingOrder;
							spawned.GetComponent<Miner>().cut = true;
						}
						else if (buildingOrder.tag == "Mine")
						{
							spawned.GetComponent<Miner>().target = buildingOrder.transform;
							spawned.GetComponent<Miner>().targetMine = buildingOrder.transform;
							spawned.GetComponent<Miner>().mine = true;
						}
						else if (buildingOrder.tag == "Stone")
						{
							spawned.GetComponent<Miner>().target = buildingOrder.transform;
							spawned.GetComponent<Miner>().targetStone = buildingOrder;
							spawned.GetComponent<Miner>().stoneMining = true;
						}
						else if (buildingOrder.tag == "Player" && buildingOrder.layer == 11)
						{
							if (buildingOrder.GetComponent<BuildingInProgress>().count < 100)
							{
								spawned.GetComponent<Miner>().targetBuilding = buildingOrder;
								spawned.GetComponent<Miner>().target = buildingOrder.transform;
								spawned.GetComponent<Miner>().build = true;
							}
							else
							{
								if (buildingOrder.name == "AppleFarm(Clone)")
								{
									spawned.GetComponent<Miner>().TargetFarm = buildingOrder;
									spawned.GetComponent<Miner>().target = buildingOrder.transform;
									spawned.GetComponent<Miner>().gather = true;
								}
							}
						}
						else
						{
							return;
						}
					}
					
				}
				count -= Time.deltaTime;
				
			}
			else
			{
				count = countReset;
			}
		}
		

		if (Selected)
		{
			canvas.SetActive(true);
			if (MakeUnits)
			{
				Queuelist.GetComponent<UnitQueue>().AddQues(UnitQueue, gameObject);
				buildingManager.spawnPoint = gameObject;
				waypoint.SetActive(true);
			}
		}
		else
		{
			canvas.SetActive(false);
			if (MakeUnits)
			{
				waypoint.SetActive(false);
			}
			
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
