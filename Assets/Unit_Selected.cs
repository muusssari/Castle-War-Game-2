using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Unit_Selected : MonoBehaviour {


	public List<GameObject> Units = new List<GameObject>();
	public List<GameObject> Buildings = new List<GameObject>();
	public List<GameObject> unitsOnScreen = new List<GameObject>();
	public List<GameObject> UnitsInDrag = new List<GameObject>();
	public bool finishedDragOnThisFrame;

	public int selected;
	public Camera cam;
	public LayerMask MovementMask;
	public LayerMask WorldLayer;
	public GUIStyle MouseDragSkin;
	public GameObject gm;

	private Vector3 mouseDownPoint;
	private Vector3 currentMousePoint;

	private bool draggingOn = false;
	public float dragCount = 0.15f;

	private Ray ray;
	public float boxWidth;
	public float boxHeight;
	public float boxTop;
	public float boxLeft;
	public Vector2 boxStart;
	public Vector2 boxFinish;
	public GameObject Order;


	void Start()
	{
		cam = Camera.main;
		gm = GameObject.Find("GameMaster");
	}

	void Update()
	{
		if (gm.GetComponent<GrindSystem>().building == null)
		{
			if (dragCount <= 0f)
			{
				draggingOn = true;
			}
			else
			{
				draggingOn = false;
			}

			if (Input.GetMouseButton(0))
			{
				dragCount -= Time.deltaTime;
			}
			else
			{
				dragCount = 0.15f;
			}

			if (Input.GetMouseButtonUp(0) && draggingOn)
			{
				finishedDragOnThisFrame = true;

			}
			else
			{
				if (!Input.GetKey(KeyCode.LeftControl))
				{
					UnLeashUnits();
				}
				SelectUnit();
			}
			OrderPosition();
			CheckMouseCurrentPos();
			MoveOrderToUnits();
			SelectedUnitsColor();
			OrderCommand();
			SelectBuilding();
			if (draggingOn)
			{
				boxWidth = Camera.main.WorldToScreenPoint(mouseDownPoint).x - Camera.main.WorldToScreenPoint(currentMousePoint).x;
				boxHeight = Camera.main.WorldToScreenPoint(mouseDownPoint).y - Camera.main.WorldToScreenPoint(currentMousePoint).y;

				boxLeft = Input.mousePosition.x;
				boxTop = (Screen.height - Input.mousePosition.y) - boxHeight;
				if (FloatToBool(boxWidth))
				{
					if (FloatToBool(boxHeight))
					{
						boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + boxHeight);
					}
					else
					{
						boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
					}
				}
				else
				{
					if (!FloatToBool(boxWidth))
					{
						if (FloatToBool(boxHeight))
						{
							boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y + boxHeight);
						}
						else
						{
							boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y);
						}
					}
				}
				boxFinish = new Vector2(boxStart.x + Unsigned(boxWidth), boxStart.y - Unsigned(boxHeight));
			}
		}
		

	}

	private void SelectBuilding()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, 1000000))
			{
				
				if (Units.Count <= 0 && hitInfo.transform.root.gameObject.layer == 11 && hitInfo.transform.root.gameObject.tag == "Player")
				{
					if (!Buildings.Contains(hitInfo.transform.root.gameObject))
					{
						Buildings.Add(hitInfo.transform.root.gameObject);
						hitInfo.transform.root.gameObject.GetComponent<buildingSelected>().Selected = true;
					}
					else
					{
						Buildings.Remove(hitInfo.transform.root.gameObject);
						hitInfo.transform.root.gameObject.GetComponent<buildingSelected>().Selected = false;
					}
					/*if (gm.GetComponent<GrindSystem>().wait != true)
					{
						if (!Buildings.Contains(hitInfo.transform.root.gameObject))
						{
							Buildings.Add(hitInfo.transform.root.gameObject);
							hitInfo.transform.root.gameObject.GetComponent<buildingSelected>().Selected = true;
						}
						else
						{
							Buildings.Remove(hitInfo.transform.root.gameObject);
							hitInfo.transform.root.gameObject.GetComponent<buildingSelected>().Selected = false;
						}
					}
					else
					{
						return;
					}*/
				}
			}
		}
		if (Input.GetMouseButtonDown(1) && Buildings.Count > 0)
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, 1000000))
			{
				if (hitInfo.transform.root.gameObject.tag == "World")
				{
					foreach (GameObject build in Buildings)
					{
						if (build.GetComponent<buildingSelected>().MakeUnits == true)
						{
							build.GetComponent<buildingSelected>().WaypointActive = true;
							build.GetComponent<buildingSelected>().waypoint.transform.position = hitInfo.point;
							build.GetComponent<buildingSelected>().waypoint.transform.Find("Flag2").transform.position = build.GetComponent<buildingSelected>().waypoint.transform.position;
							build.GetComponent<buildingSelected>().buildingOrder = null;
						}
						else
						{
							return;
						}
					}
				}
				if (hitInfo.transform.root.gameObject.tag == "Mine")
				{
					foreach (GameObject build in Buildings)
					{
						if (build.GetComponent<buildingSelected>().MakeUnits == true)
						{
							build.GetComponent<buildingSelected>().WaypointActive = true;
							build.GetComponent<buildingSelected>().waypoint.transform.position = hitInfo.transform.root.gameObject.transform.position;
							build.GetComponent<buildingSelected>().waypoint.transform.Find("Flag2").transform.position = hitInfo.transform.root.gameObject.transform.position + new Vector3(0,6,0);
							build.GetComponent<buildingSelected>().buildingOrder = hitInfo.transform.root.gameObject;

						}
						else
						{
							return;
						}
					}
				}
				if (hitInfo.transform.root.gameObject.tag == "Stone")
				{
					foreach (GameObject build in Buildings)
					{
						if (build.GetComponent<buildingSelected>().MakeUnits == true)
						{
							build.GetComponent<buildingSelected>().WaypointActive = true;
							build.GetComponent<buildingSelected>().waypoint.transform.position = hitInfo.transform.root.gameObject.transform.position;
							build.GetComponent<buildingSelected>().waypoint.transform.Find("Flag2").transform.position = hitInfo.transform.root.gameObject.transform.position + new Vector3(0, 3, 0);
							build.GetComponent<buildingSelected>().buildingOrder = hitInfo.transform.root.gameObject;

						}
						else
						{
							return;
						}
					}
				}
				if (hitInfo.transform.root.gameObject.tag == "Tree")
				{
					foreach (GameObject build in Buildings)
					{
						if (build.GetComponent<buildingSelected>().MakeUnits == true)
						{
							build.GetComponent<buildingSelected>().WaypointActive = true;
							build.GetComponent<buildingSelected>().waypoint.transform.position = hitInfo.transform.root.gameObject.transform.position;
							build.GetComponent<buildingSelected>().waypoint.transform.Find("Flag2").transform.position = hitInfo.transform.root.gameObject.transform.position + new Vector3(0, 8, 0);
							build.GetComponent<buildingSelected>().buildingOrder = hitInfo.transform.root.gameObject;
						} else
						{
							return;
						}
					}
				}
				if (hitInfo.transform.root.gameObject.tag == "Player" && hitInfo.transform.root.gameObject.layer == 11)
				{
					foreach (GameObject build in Buildings)
					{
						if (build.GetComponent<buildingSelected>().MakeUnits == true)
						{
							build.GetComponent<buildingSelected>().WaypointActive = true;
							build.GetComponent<buildingSelected>().waypoint.transform.position = hitInfo.transform.root.gameObject.transform.position;
							build.GetComponent<buildingSelected>().waypoint.transform.Find("Flag2").transform.position = hitInfo.transform.root.gameObject.transform.position + new Vector3(0, 5, 0);
							build.GetComponent<buildingSelected>().buildingOrder = hitInfo.transform.root.gameObject;
						}
						else
						{
							return;
						}
					}
				}
			}
		}
	}

	private void CheckMouseCurrentPos()
	{
		ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, 1000000))
		{
			
			currentMousePoint = hitInfo.point;
			if (Input.GetMouseButtonDown(0))
			{
				mouseDownPoint = hitInfo.point;
			}
		}
	}

	void LateUpdate()
	{
		UnitsInDrag.Clear();
		if (Buildings.Count > 0)
		{
			foreach (GameObject build in Buildings)
			{
				if (build == null)
				{
					Buildings.Remove(build);
					return;
				}
				else
				{
					return;
				}
			}
		}
		if ((draggingOn || finishedDragOnThisFrame) && unitsOnScreen.Count > 0)
		{
			foreach (GameObject Unit in unitsOnScreen)
			{
				if (Unit == null)
				{
					unitsOnScreen.Remove(Unit);
					return;
				}
				else
				{
					if (UnitInsideDrag(Unit.GetComponent<Unit>().ScreenPos))
					{
						Renderer r = Unit.GetComponentInChildren<Renderer>();
						Material m = r.material;
						m.color = Color.white;
						r.material = m;
						UnitsInDrag.Add(Unit);
					}
					else
					{
						Color c = Unit.GetComponentInChildren<Defaults>().c;
						Renderer r = Unit.GetComponentInChildren<Renderer>();
						Material m = r.material;
						m.color = c;
					}
				}
				
			}
		}
		if (finishedDragOnThisFrame)
		{
			finishedDragOnThisFrame = false;
			PutDraggedUnitsInCurrentlySelectedUnits();
		}
	}

	public void PutDraggedUnitsInCurrentlySelectedUnits()
	{
		if (UnitsInDrag.Count > 0)
		{
			foreach (GameObject unit in UnitsInDrag)
			{
				Units.Add(unit);
				unit.GetComponent<Unit>().Selected = true;
			}
			UnitsInDrag.Clear();
			finishedDragOnThisFrame = false;
		}
	}
	public bool UnitInsideDrag(Vector2 UnitScreenPos)
	{
		if ((UnitScreenPos.x > boxStart.x && UnitScreenPos.y < boxStart.y) && (UnitScreenPos.x < boxFinish.x && UnitScreenPos.y > boxFinish.y)) return true; else return false;
	}
	public bool UnitWithinScreenSpace(Vector2 UnitScreenPos)
	{
		if ((UnitScreenPos.x < Screen.width && UnitScreenPos.y < Screen.height) && (UnitScreenPos.x > 0f && UnitScreenPos.y > 0f)) return true; else return false;
	}

	public void RemoveFromOnScreenUnits(GameObject Unit)
	{
		foreach (GameObject unit in unitsOnScreen)
		{
			if (Unit == unit)
			{
				unitsOnScreen.Remove(unit);
				Unit.GetComponent<Unit>().OnScreen = false;
				return;
			}
		}
		return;
	}

	private bool FloatToBool(float Float)
	{
		if (Float < 0f) return false; else return true;
	}

	private float Unsigned(float val)
	{
		if (val < 0f) val *= -1;

		return val;
	}
	private void SelectedUnitsColor()
	{
		foreach (GameObject unit in Units)
		{
			if (unit != null)
			{
				Renderer r = unit.GetComponentInChildren<Renderer>();
				Material m = r.material;
				m.color = Color.green;
				r.material = m;
			}
		}
	}
	private void OrderPosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 10000, WorldLayer))
		{
			Order.transform.position = hit.point + Vector3.up * 0.1f;
		}
		if (Physics.Raycast(ray, out hit, 10000))
		{
			if (Units.Count > 0 && hit.transform.root.gameObject.name != "PlayerBase")
			{
				if (hit.transform.root.gameObject.tag == "World")
				{
					Order.transform.GetChild(0).gameObject.SetActive(true);
				}
				else
				{
					Order.transform.GetChild(0).gameObject.SetActive(false);
				}

				if (hit.transform.root.gameObject.tag == "Enemy")
				{
					foreach (GameObject unit in Units)
					{
						if (unit.name != "Player_Worker(Clone)")
						{
							Order.transform.GetChild(1).gameObject.SetActive(true);
							Order.transform.GetChild(1).gameObject.GetComponent<OrderPosition>().floor = hit.transform.root.gameObject.transform;
							Order.transform.GetChild(1).gameObject.GetComponent<AnimationScript>().PlayAnimation();
						}
						else
						{
							return;
						}
					}
				}
				else
				{
					Order.transform.GetChild(1).gameObject.SetActive(false);
				}

				if (hit.transform.root.gameObject.tag == "Mine")
				{
					foreach (GameObject unit in Units)
					{
						if (unit.name == "Player_Worker(Clone)")
						{
							Order.transform.GetChild(2).gameObject.SetActive(true);
							Order.transform.GetChild(2).gameObject.GetComponent<OrderPosition>().floor = hit.transform.root.gameObject.transform;
							Order.transform.GetChild(2).gameObject.GetComponent<AnimationScript>().PlayAnimation();
						}
						else
						{
							return;
						}
					}
				}
				else
				{
					Order.transform.GetChild(2).gameObject.SetActive(false);
				}

				if (hit.transform.root.gameObject.tag == "Stone")
				{
					foreach (GameObject unit in Units)
					{
						if (unit.name == "Player_Worker(Clone)")
						{
							Order.transform.GetChild(2).gameObject.SetActive(true);
							Order.transform.GetChild(2).gameObject.GetComponent<OrderPosition>().floor = hit.transform.root.gameObject.transform;
							Order.transform.GetChild(2).gameObject.GetComponent<AnimationScript>().PlayAnimation();
						}
						else
						{
							return;
						}
					}
				}
				else
				{
					Order.transform.GetChild(2).gameObject.SetActive(false);
				}

				if (hit.transform.root.gameObject.tag == "Player" && hit.transform.root.gameObject.layer == 11 && hit.transform.root.gameObject.name == "Tower(Clone)")
				{

					Order.transform.GetChild(3).gameObject.SetActive(true);
					Order.transform.GetChild(3).gameObject.GetComponent<OrderPosition>().floor = hit.transform.root.gameObject.transform;
				}
				else
				{
					Order.transform.GetChild(3).gameObject.SetActive(false);
				}

				if (hit.transform.root.gameObject.tag == "Tree")
				{
					foreach (GameObject unit in Units)
					{
						if (unit.name == "Player_Worker(Clone)")
						{
							Order.transform.GetChild(4).gameObject.SetActive(true);
							Order.transform.GetChild(4).gameObject.GetComponent<OrderPosition>().floor = hit.transform.root.gameObject.transform;
							Order.transform.GetChild(4).gameObject.GetComponent<AnimationScript>().PlayAnimation();
						}
						else
						{
							return;
						}
					}
				}
				else
				{
					Order.transform.GetChild(4).gameObject.SetActive(false);
				}

				if (hit.transform.root.gameObject.tag == "Player" && hit.transform.root.gameObject.layer == 11)
				{
					foreach (GameObject unit in Units)
					{
						if (unit.name == "Player_Worker(Clone)")
						{
							Order.transform.GetChild(5).gameObject.SetActive(true);
							Order.transform.GetChild(5).gameObject.GetComponent<OrderPosition>().floor = hit.transform.root.gameObject.transform;
							Order.transform.GetChild(5).gameObject.GetComponent<AnimationScript>().PlayAnimation();
						}
						else
						{
							return;
						}
					}
				}
				else
				{
					Order.transform.GetChild(5).gameObject.SetActive(false);
				}

				if (hit.transform.root.gameObject.tag == "Player" && !Units.Contains(hit.transform.root.gameObject) && hit.transform.root.gameObject.layer != 11)
				{
					Order.transform.GetChild(6).gameObject.SetActive(true);
				}
				else
				{
					Order.transform.GetChild(6).gameObject.SetActive(false);
				}

				
			}
			else if (hit.transform.root.gameObject.tag == "Player" && hit.transform.root.gameObject.layer != 11)
			{
				Order.transform.GetChild(6).gameObject.SetActive(true);
			}
			else
			{
				Order.transform.GetChild(6).gameObject.SetActive(false);
				Order.transform.GetChild(0).gameObject.SetActive(false);
			}
		}
	}
	
	private void SelectUnit()
	{
		if (Input.GetKey(KeyCode.LeftControl))
		{
			if (Input.GetMouseButtonDown(0) && Buildings.Count <= 0)
			{
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo, 1000000, MovementMask))
				{
					if (Buildings.Count > 0)
					{
						foreach (GameObject build in Buildings)
						{
							build.GetComponent<buildingSelected>().Selected = false;
						}
						Buildings.Clear();
					}
					if (!Units.Contains(hitInfo.transform.root.gameObject))
					{
						Units.Add(hitInfo.transform.root.gameObject);
						hitInfo.transform.root.gameObject.GetComponent<Unit>().Selected = true;
					}
					else
					{
						foreach (GameObject unit in Units)
						{
							if (unit != null)
							{
								unit.GetComponent<Unit>().Selected = false;
								Color c = unit.GetComponentInChildren<Defaults>().c;
								Renderer r = unit.GetComponentInChildren<Renderer>();
								Material m = r.material;
								m.color = c;
								r.material = m;
							}
						}
						Units.Remove(hitInfo.transform.root.gameObject);
					}
				}
			}
		}
		
		if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftControl))
		{
			
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, 1000000, MovementMask))
			{
				if (Buildings.Count > 0)
				{
					foreach (GameObject build in Buildings)
					{
						build.GetComponent<buildingSelected>().Selected = false;
					}
					Buildings.Clear();
				}
				
				foreach (GameObject unit in Units)
				{
					if (unit != null)
					{
						unit.GetComponent<Unit>().Selected = false;
						Color c = unit.GetComponentInChildren<Defaults>().c;
						Renderer r = unit.GetComponentInChildren<Renderer>();
						Material m = r.material;
						m.color = c;
						r.material = m;
					}
				}
				Units.Clear();
				Units.Add(hitInfo.transform.root.gameObject);
				hitInfo.transform.root.gameObject.GetComponent<Unit>().Selected = true;
			}
		}
	}
	private void OnGUI()
	{
		if(draggingOn)
		{
			GUI.Box(new Rect(boxLeft,
				boxTop,
				boxWidth, boxHeight), "", MouseDragSkin);
		}
	}
	private void OrderCommand()
	{
		if (Input.GetMouseButtonDown(1) && Units.Count>0)
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				
				if (hitInfo.transform.root.gameObject.tag == "Enemy")
				{
					//Debug.Log("Attack");
					foreach (GameObject unit in Units)
					{
						if(unit != null && unit.name != "Player_Miner(Clone)")
						{
							unit.GetComponent<SeachForEnemy>().target = hitInfo.transform.root.gameObject.transform;
							unit.GetComponent<SeachForEnemy>().targetThing = hitInfo.transform.root.gameObject;
							if (unit.name != "Player_Unit_Archer(Clone)")
							{
								unit.GetComponent<Unit_Moving>().MoveToPoint(hitInfo.transform.position);
							}
						}
					}
				}
				if(hitInfo.transform.root.gameObject.tag == "Mine")
				{
					//Debug.Log("Mine!");
					foreach (GameObject unit in Units)
					{
						if (unit != null)
						{
							if(unit.name == "Player_Worker(Clone)")
							{
								unit.GetComponent<Miner>().target = hitInfo.transform.root.gameObject.transform;
								unit.GetComponent<Miner>().targetMine = hitInfo.transform.root.gameObject.transform;
								unit.GetComponent<Miner>().mine = true;
							}
							
						}

					}
				}
				if (hitInfo.transform.root.gameObject.tag == "Stone")
				{
					//Debug.Log("Mine stone!");
					foreach (GameObject unit in Units)
					{
						if (unit != null)
						{
							if (unit.name == "Player_Worker(Clone)")
							{
								unit.GetComponent<Miner>().target = hitInfo.transform.root.gameObject.transform;
								unit.GetComponent<Miner>().targetStone = hitInfo.transform.root.gameObject;
								unit.GetComponent<Miner>().stoneMining = true;
							}
						}
					}
				}
				if (hitInfo.transform.root.gameObject.tag == "Tree")
				{
					//Debug.Log("Woood!");
					foreach (GameObject unit in Units)
					{
						if (unit != null)
						{
							if (unit.name == "Player_Worker(Clone)")
							{
								unit.GetComponent<Miner>().target = hitInfo.transform.root.gameObject.transform;
								unit.GetComponent<Miner>().targetTree = hitInfo.transform.root.gameObject;
								unit.GetComponent<Miner>().cut = true;
							}
						}
					}
				}
				if (hitInfo.transform.root.gameObject.layer == 11 && hitInfo.transform.root.gameObject.tag == "Player")
				{
					//Debug.Log("Go in to building");
					foreach (GameObject unit in Units)
					{
						if (unit != null)
						{
							if (unit.name == "Player_Unit_Archer(Clone)")
							{
								unit.GetComponent<Unit_Moving>().MoveToPoint(hitInfo.transform.position);
							}
							if (unit.name == "Player_Worker(Clone)")
							{
								if (hitInfo.transform.root.gameObject.name != "PlayerBase" && hitInfo.transform.root.gameObject.GetComponent<BuildingInProgress>().building == false)
								{
									//Debug.Log("build");
									unit.GetComponent<Miner>().build = true;
									unit.GetComponent<Miner>().targetBuilding = hitInfo.transform.root.gameObject;
									unit.GetComponent<Miner>().target = hitInfo.transform.root.transform;
								}
								else if (hitInfo.transform.root.gameObject.name == "AppleFarm(Clone)" && hitInfo.transform.root.gameObject.GetComponent<BuildingInProgress>().building == true)
								{
									//Debug.Log("gather");
									unit.GetComponent<Miner>().target = hitInfo.transform.root.gameObject.transform;
									unit.GetComponent<Miner>().TargetFarm = hitInfo.transform.root.gameObject;
									unit.GetComponent<Miner>().gather = true;
								}
							}
						}
					}
				}
			}
		}
	}

	public void UnLeashUnits()
	{
		if ((Input.GetMouseButtonDown(0) && (Units.Count > 0 || Buildings.Count > 0) && !draggingOn) && gm.GetComponent<GrindSystem>().building == null)
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo))
				{
					if ((hitInfo.collider.tag == "World" || hitInfo.transform.root.gameObject.layer == 11) && hitInfo.transform.root.gameObject.layer != 5)
					{
					
						foreach (GameObject build in Buildings)
						{
							if (build != null)
							{
								build.GetComponent<buildingSelected>().Selected = false;
							}
							else
							{
								return;
							}
						}
						foreach (GameObject unit in Units)
						{
							if (unit != null)
							{
								unit.GetComponent<Unit>().Selected = false;
								Color c = unit.GetComponentInChildren<Defaults>().c;
								Renderer r = unit.GetComponentInChildren<Renderer>();
								Material m = r.material;
								m.color = c;
								r.material = m;
							}
						
						}
						Units.Clear();
						Buildings.Clear();
					}
				}
			}
		}
	}

	private void MoveOrderToUnits()
	{
		if (Input.GetMouseButtonDown(1) && Units.Count > 0)
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				
				if (hitInfo.collider.tag == "World")
				{
					
					foreach (GameObject unit in Units)
					{
						if(unit != null)
						{
							if (unit.name != "Player_Worker(Clone)")
							{
								unit.GetComponent<SeachForEnemy>().target = null;
								unit.GetComponent<SeachForEnemy>().targetThing = null;
							}
							if (unit.name == "Player_Worker(Clone)")
							{
								unit.GetComponent<Miner>().mine = false;
								unit.GetComponent<Miner>().cut = false;
								unit.GetComponent<Miner>().build = false;
								unit.GetComponent<Miner>().gather = false;
								unit.GetComponent<Miner>().stoneMining = false;
							}
							unit.GetComponent<Unit_Moving>().MoveToPoint(hitInfo.point);
						}
					}
				}
			}
		}
	}
}
