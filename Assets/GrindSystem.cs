using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrindSystem : MonoBehaviour {

	public GameObject building;
	public GameObject navMesh;
	public GameObject player_base;
	public GameObject MaterialsNeeded;

	public GameObject WStart;
	public GameObject WEnd;
	public GameObject WallPrefab;
	GameObject Wall;
	GameObject WallModel;
	private bool creating;
	private bool inBuilding1;
	private bool inBuilding2;
	private bool inBuilding;
	private GameObject UnderTower;

	Transform[] allChildren;
	public List<GameObject> Build = new List<GameObject>();
	public List<GameObject> Stones = new List<GameObject>();
	public List<GameObject> Player = new List<GameObject>();
	public List<GameObject> FarmTiles = new List<GameObject>();
	public List<GameObject> Tiles = new List<GameObject>();
	public LayerMask MovementMask;
	public LayerMask BuildingMask;
	Vector3 truePos;
	public float grindSize;
	float wallDistance;
	float wallDistanceStone;

	private float mouseWheelRotation;
	public GameObject selectedunits;
	public List<GameObject> Units = new List<GameObject>();

	public bool CanBuildhere = false;

	

	private void Start()
	{
		player_base = GameObject.Find("PlayerBase");
		InvokeRepeating("CheckBuildings", 0f, 1f);
		InvokeRepeating("CheckStones", 0f, 1f);
		GameObject[] FarmTiles = GameObject.FindGameObjectsWithTag("FarmTile");
		foreach (GameObject t in FarmTiles)
		{
			if (t == null)
			{
				Tiles.Remove(t);
			}
			if (!Tiles.Contains(t))
			{
				Tiles.Add(t);
			}
		}
	}
	void Update ()
	{
		if (building != null)
		{
			if (building.name == "Wall(Clone)")
			{
				MoveCurrrentBuilding();
				BuildingWall();
				
			}
			else
			{
				MoveCurrrentBuilding();
				RotateBuilding();
				ReleseIfClicked();
			}
			
			if (Input.GetMouseButtonDown(1))
			{
				Destroy(building);
				building = null;
				Array.Clear(allChildren, 0, allChildren.Length);
			}
		}
	}

	private void BuildingWall()
	{
		if (Input.GetMouseButtonDown(0) && CanBuildhere && creating == false)
		{
			SetStart();
			if (inBuilding)
			{
				inBuilding1 = true;
			}
			else
			{
				inBuilding1 = false;
			}
		}
		else if (Input.GetMouseButtonDown(0) && creating == true && PlayerStats.Stone >= (int)wallDistanceStone * 2 + 4)
		{
			if (inBuilding)
			{
				inBuilding2 = true;
			}
			else
			{
				inBuilding2 = false;
			}
			SetEnd();
			PlayerStats.Stone -= (int)wallDistanceStone * 2;
			MaterialsNeeded.GetComponent<MaterialsNeeded>().SetMaterialInfoActive(false);
		}
		else if (Input.GetMouseButtonDown(0) && creating == true && PlayerStats.Stone < (int)wallDistanceStone * 2 + 4)
		{
			SetStop();
			transform.GetComponent<BuildingManager>().WarningText("Not Enought Stone");
		}
		else if (Input.GetMouseButtonDown(1))
		{
			SetStop();
		}
		else
		{
			if (creating)
			{
				Adjust();
				MaterialsNeeded.GetComponent<MaterialsNeeded>().StoneToWall = (int)wallDistanceStone * 2 +4;
			}
		}
	}
	private void SetStop()
	{
		creating = false;
		Destroy(WStart);
		Destroy(WEnd);
		Destroy(building);
		Destroy(Wall);
		building = null;
		MaterialsNeeded.GetComponent<MaterialsNeeded>().SetMaterialInfoActive(false);
	}
	private void SetStart()
	{
		MaterialsNeeded.GetComponent<MaterialsNeeded>().SetMaterialInfoActive(true);
		creating = true;
		WStart.transform.position = truePos;
		WStart.SetActive(true);
		Wall = (GameObject)Instantiate(WallPrefab, WStart.transform.position, Quaternion.identity);
		Wall.transform.Find("Model/").gameObject.SetActive(false);
		building.SetActive(false);
		WEnd.transform.position = truePos;
		WEnd.SetActive(true);
		WallModel = Wall.transform.Find("Model/wall").gameObject;
	}
	private void SetEnd()
	{
		creating = false;
		WEnd.GetComponent<BoxCollider>().enabled = true;
		WEnd.GetComponent<buildingSelected>().Selected = false;
		WEnd.transform.Find("CanBuild/").gameObject.SetActive(false);
		WStart.GetComponent<BoxCollider>().enabled = true;
		WStart.GetComponent<buildingSelected>().Selected = false;
		WStart.transform.Find("CanBuild/").gameObject.SetActive(false);
		Wall.GetComponent<BoxCollider>().enabled = true;
		Wall.GetComponent<buildingSelected>().Selected = false;
		Wall.transform.Find("CanBuild/").gameObject.SetActive(false);
		
		if (inBuilding1)
		{
			Destroy(WStart);
		}
		if (inBuilding2)
		{
			Destroy(WEnd);
		}
		foreach (GameObject unit in Units)
		{
			if (unit.name == "Player_Worker(Clone)")
			{
				unit.GetComponent<Miner>().build = true;
				unit.GetComponent<Miner>().targetBuilding = Wall;
				unit.GetComponent<Miner>().target = Wall.transform;
			}
		}
		WEnd = null;
		WStart = null;
		Wall = null;
		WallModel = null;
		Destroy(building);
		building = null;
		MaterialsNeeded.GetComponent<MaterialsNeeded>().SetMaterialInfoActive(false);
	}
	private void Adjust()
	{
		
		wallDistance = Vector3.Distance(WStart.transform.position, building.transform.position);
		if (wallDistance <= 20f)
		{
			wallDistanceStone = wallDistance;
			WEnd.transform.position = truePos;
			WStart.transform.LookAt(WEnd.transform.position);
			WEnd.transform.LookAt(WStart.transform.position);
			Wall.transform.rotation = WStart.transform.rotation;
			Wall.transform.position = WStart.transform.position + wallDistance / 2f * WStart.transform.forward;
			WallModel.transform.localScale = new Vector3(WallModel.transform.localScale.x, WallModel.transform.localScale.y, wallDistance / 1.25f);
			Wall.GetComponent<BoxCollider>().size = new Vector3(1, 2, wallDistance / 1.1f);
			Wall.GetComponent<NavMeshObstacle>().size = new Vector3(1, 2, wallDistance / 1.2f);
		}
		else if (wallDistance > 20f)
		{
			wallDistanceStone = 20f;
			WallModel.transform.localScale = new Vector3(WallModel.transform.localScale.x, WallModel.transform.localScale.y, 20f / 1.25f);
			Wall.transform.position = WStart.transform.position + 20f / 2f * WStart.transform.forward;
			WEnd.transform.position = WStart.transform.position + 20f * WStart.transform.forward;
			Wall.GetComponent<BoxCollider>().size = new Vector3(1, 2, 20f / 1.1f);
			Wall.GetComponent<NavMeshObstacle>().size = new Vector3(1, 2, 20f / 1.1f);
		}
	}

	private void CheckStones()
	{
		//GameObject[] Stones = GameObject.FindGameObjectsWithTag("Stone");
	}
	private void CheckBuildings()
	{
		GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject p in Player)
		{
			if (p == null)
			{
				Build.Remove(p);
			}
			if (p.layer == 11 && !Build.Contains(p) && p != building)
			{
				Build.Add(p);
			}
		}
	}
	private void ReleseIfClicked()
	{
		if (Input.GetMouseButtonDown(0) && CanBuildhere)
		{
			Color c = building.GetComponentInChildren<Defaults>().c;
			foreach (Transform child in allChildren)
			{
				child.GetComponentInChildren<Renderer>().material.color = c;
			}
			building.transform.Find("CanBuild/").gameObject.SetActive(false);
			building.GetComponent<BoxCollider>().enabled = true;
			building.GetComponent<buildingSelected>().Selected = false;
			if (building.transform.Find("ColliderFarm") != null)
			{
				building.transform.Find("ColliderFarm").gameObject.SetActive(true);
			}
			if (building.name == "Tower(Clone)" && UnderTower != null)
			{
				Destroy(UnderTower);
			}
			foreach (GameObject unit in Units)
			{
				if (unit.name == "Player_Worker(Clone)")
				{
					unit.GetComponent<Miner>().build = true;
					unit.GetComponent<Miner>().targetBuilding = building;
					unit.GetComponent<Miner>().target = building.transform;
				}
			}

			building = null;
			GetComponent<BuildingManager>().BuildingToBuild();
		}
		else if (Input.GetMouseButtonDown(0))
		{
			GetComponent<BuildingManager>().WarningText("Can't Build Here");
		}
		else
		{
			return;
		}
	}

	private void RotateBuilding()
	{
		mouseWheelRotation = Input.mouseScrollDelta.y;
		building.transform.Rotate(Vector3.up, (mouseWheelRotation * 800f * Time.deltaTime / grindSize) * grindSize );
	}

	private void MoveCurrrentBuilding()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;
		if (building.name == "Wall(Clone)")
		{
			if (Physics.Raycast(ray, out hitInfo, 1000, BuildingMask))
			{
				if (hitInfo.transform.root.gameObject.name == "Tower(Clone)" || hitInfo.transform.root.gameObject.name == "Wall(Clone)")
				{
					truePos = hitInfo.transform.root.gameObject.transform.position;
					building.transform.position = truePos;
					CanBuildhere = true;
					inBuilding = true;
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.green;
					}
				}
				else if (hitInfo.transform.root.gameObject.name == "WallMiddle(Clone)")
				{
					truePos = hitInfo.point;
					building.transform.position = truePos;
					CanBuildhere = false;
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.red;
					}
				}
				else
				{
					CanBuildhere = false;
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.red;
					}
				}
			}
			else if (Physics.Raycast(ray, out hitInfo, 1000, MovementMask))
			{
				inBuilding = false;
				if (building.transform.position.x < player_base.transform.position.x + 45 && building.transform.position.x > player_base.transform.position.x - 45)
				{
					if (building.transform.position.z < player_base.transform.position.z + 45 && building.transform.position.z > player_base.transform.position.z - 45)
					{
						float shortestDistance = Mathf.Infinity;

						foreach (GameObject b in Build)
						{
							if (b != null)
							{
								float distanceToBuildings = Vector3.Distance(b.transform.position, building.transform.position);
								distanceToBuildings -= b.GetComponent<buildingSelected>().range + building.GetComponent<buildingSelected>().range;
								if (distanceToBuildings < shortestDistance)
								{
									shortestDistance = distanceToBuildings;
								}
							}
							else
							{
								Build.Remove(b);
								return;
							}
						}
						if (shortestDistance >= 0.2f)
						{
							foreach (Transform child in allChildren)
							{
								child.GetComponentInChildren<Renderer>().material.color = Color.green;
							}
							CanBuildhere = true;
						}
						else
						{
							foreach (Transform child in allChildren)
							{
								child.GetComponentInChildren<Renderer>().material.color = Color.red;
							}
							CanBuildhere = false;
						}
					}
					else
					{
						foreach (Transform child in allChildren)
						{
							child.GetComponentInChildren<Renderer>().material.color = Color.red;
						}
						CanBuildhere = false;
					}
				}
				else
				{
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.red;
					}
					CanBuildhere = false;
				}

				truePos.x = Mathf.Floor(hitInfo.point.x / grindSize) * grindSize;
				truePos.y = Mathf.Floor(hitInfo.point.y / grindSize) * grindSize;
				truePos.z = Mathf.Floor(hitInfo.point.z / grindSize) * grindSize;
				building.transform.position = truePos;
			}
			else
			{
				CanBuildhere = false;
				inBuilding = false;
			}
		}
		else if (building.name == "Tower(Clone)")
		{
			if (Physics.Raycast(ray, out hitInfo, 1000, BuildingMask))
			{

				if (hitInfo.transform.root.gameObject.name == "Wall(Clone)")
				{
					truePos = hitInfo.transform.root.gameObject.transform.position;
					building.transform.position = truePos;
					CanBuildhere = true;
					inBuilding = true;
					UnderTower = hitInfo.transform.root.gameObject;
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.green;
					}
				}
				else
				{
					UnderTower = null;
					CanBuildhere = false;
				}
			}
			else if (Physics.Raycast(ray, out hitInfo, 1000, MovementMask))
			{
				UnderTower = null;
				inBuilding = false;
				if (building.transform.position.x < player_base.transform.position.x + 45 && building.transform.position.x > player_base.transform.position.x - 45)
				{
					if (building.transform.position.z < player_base.transform.position.z + 45 && building.transform.position.z > player_base.transform.position.z - 45)
					{
						float shortestDistance = Mathf.Infinity;

						foreach (GameObject b in Build)
						{
							if (b != null)
							{
								float distanceToBuildings = Vector3.Distance(b.transform.position, building.transform.position);
								distanceToBuildings -= b.GetComponent<buildingSelected>().range + building.GetComponent<buildingSelected>().range;
								if (distanceToBuildings < shortestDistance)
								{
									shortestDistance = distanceToBuildings;
								}
							}
							else
							{
								Build.Remove(b);
								return;
							}
						}
						if (shortestDistance >= 0.5f)
						{
							foreach (Transform child in allChildren)
							{
								child.GetComponentInChildren<Renderer>().material.color = Color.green;
							}
							CanBuildhere = true;
						}
						else
						{
							foreach (Transform child in allChildren)
							{
								child.GetComponentInChildren<Renderer>().material.color = Color.red;
							}
							CanBuildhere = false;
						}
					}
					else
					{
						foreach (Transform child in allChildren)
						{
							child.GetComponentInChildren<Renderer>().material.color = Color.red;
						}
						CanBuildhere = false;
					}
				}
				else
				{
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.red;
					}
					CanBuildhere = false;
				}

				truePos.x = Mathf.Floor(hitInfo.point.x / grindSize) * grindSize;
				truePos.y = Mathf.Floor(hitInfo.point.y / grindSize) * grindSize;
				truePos.z = Mathf.Floor(hitInfo.point.z / grindSize) * grindSize;
				building.transform.position = truePos;
			}
			else
			{
				CanBuildhere = false;
				inBuilding = false;
			}
		}
		else if (Physics.Raycast(ray, out hitInfo, 1000, MovementMask))
		{

			if (building.name == "AppleFarm(Clone)")
			{
				float shortestDistance = Mathf.Infinity;
				List<GameObject> TileInRange = new List<GameObject>();
				foreach (GameObject tile in Tiles)
				{
					if (tile.activeSelf == true)
					{
						float distanceToTile = Vector3.Distance(tile.transform.position, building.transform.position);
						distanceToTile -= 1 + building.GetComponent<buildingSelected>().range;

						if (distanceToTile <= 0)
						{
							TileInRange.Add(tile);
						}
					}

				}
				foreach (GameObject b in Build)
				{
					if (b != null)
					{
						float distanceToBuildings = Vector3.Distance(b.transform.position, building.transform.position);
						distanceToBuildings -= b.GetComponent<buildingSelected>().range + building.GetComponent<buildingSelected>().range;
						if (distanceToBuildings < shortestDistance)
						{
							shortestDistance = distanceToBuildings;
						}
					}
					else
					{
						Build.Remove(b);
						return;
					}
				}
				//Debug.Log(TileInRange.Count);
				if (shortestDistance >= 0.5f && TileInRange.Count >= 4)
				{
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.green;
					}
					CanBuildhere = true;
				}
				else
				{
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.red;
					}
					CanBuildhere = false;
				}


				building.transform.position = hitInfo.point;
			}
			else if (building.name == "StoneMine(Clone)")
			{
				float shortestDistance = Mathf.Infinity;
				float shortestStoneDist = Mathf.Infinity;

				foreach (GameObject b in Build)
				{
					if (b != null)
					{
						float distanceToBuildings = Vector3.Distance(b.transform.position, building.transform.position);
						distanceToBuildings -= b.GetComponent<buildingSelected>().range + building.GetComponent<buildingSelected>().range;
						if (distanceToBuildings < shortestDistance)
						{
							shortestDistance = distanceToBuildings;
						}
					}
					else
					{
						Build.Remove(b);
						return;
					}
				}
				if (shortestDistance >= 0.5f)
				{
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.green;
					}
					CanBuildhere = true;
				}
				else
				{
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.red;
					}
					CanBuildhere = false;
				}
				foreach (GameObject stone in Stones)
				{
					if (stone != null)
					{
						float distanceToStones = Vector3.Distance(stone.transform.position, building.transform.position);
						distanceToStones -= stone.GetComponent<buildingSelected>().range + building.GetComponent<buildingSelected>().range;
						if (distanceToStones < shortestDistance)
						{
							shortestStoneDist = distanceToStones;
						}
					}
					else
					{
						Build.Remove(stone);
						return;
					}
				}
				if (shortestStoneDist <= 1.2f)
				{
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.green;
					}
					CanBuildhere = true;
				}
				building.transform.position = hitInfo.point;
			}
			else
			{
				float shortestDistance = Mathf.Infinity;

				foreach (GameObject b in Build)
				{
					if (b != null)
					{
						float distanceToBuildings = Vector3.Distance(b.transform.position, building.transform.position);
						distanceToBuildings -= b.GetComponent<buildingSelected>().range + building.GetComponent<buildingSelected>().range;
						if (distanceToBuildings < shortestDistance)
						{
							shortestDistance = distanceToBuildings;
						}
					}
					else
					{
						Build.Remove(b);
						return;
					}
				}
				if (shortestDistance >= 0.5f)
				{
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.green;
					}
					CanBuildhere = true;
				}
				else
				{
					foreach (Transform child in allChildren)
					{
						child.GetComponentInChildren<Renderer>().material.color = Color.red;
					}
					CanBuildhere = false;
				}
				truePos.x = Mathf.Floor(hitInfo.point.x / grindSize) * grindSize;
				truePos.y = Mathf.Floor(hitInfo.point.y / grindSize) * grindSize;
				truePos.z = Mathf.Floor(hitInfo.point.z / grindSize) * grindSize;
				building.transform.position = truePos;
			}

		}
	}

	public void SetBuilding(GameObject a)
	{
		building = a;
		building.transform.Find("Model/").gameObject.SetActive(false);
		allChildren = building.transform.Find("CanBuild/").GetComponentsInChildren<Transform>();
		Units = selectedunits.GetComponent<Unit_Selected>().Units;
	}
	public void SetBuildingWall(GameObject w1, GameObject w2, GameObject w)
	{
		building = w;
		WStart = w1;
		WEnd = w2;
		WStart.SetActive(false);
		WEnd.SetActive(false);
		building.transform.Find("Model/").gameObject.SetActive(false);
		WStart.transform.Find("Model/").gameObject.SetActive(false);
		WEnd.transform.Find("Model/").gameObject.SetActive(false);
	
		allChildren = building.transform.Find("CanBuild/").GetComponentsInChildren<Transform>();
		Units = selectedunits.GetComponent<Unit_Selected>().Units;
	}
}
