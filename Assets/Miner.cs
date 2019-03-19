using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour {

	public Transform target = null;
	Unit_Moving move;
	public int gold = 0;
	public int wood = 0;
	public int food = 0;
	public int stone = 0;
	public bool mine = false;
	public bool cut = false;
	public bool build = false;
	public bool cutting = false;
	public bool gather = false;
	public bool stoneMining = false;
	public Transform OwnBase = null;
	public Transform targetMine = null;
	public GameObject targetTree = null;
	public GameObject targetStone = null;
	public GameObject targetBuilding = null;
	public GameObject TargetFarm = null;
	private GameObject mineaxe;
	private GameObject bag;
	private GameObject axe;
	private GameObject hammer;
	private GameObject log;
	private GameObject rock;

	public float count = 2;
	private float countReset = 2;

	void Start ()
	{
		mineaxe = transform.Find("MineAxe").gameObject;
		axe = transform.Find("Axe").gameObject;
		bag = transform.Find("GoldBag").gameObject;
		hammer = transform.Find("Hammer").gameObject;
		log = transform.Find("Logs").gameObject;
		rock = transform.Find("Rock").gameObject;
		bag.SetActive(false);
		move = GetComponent<Unit_Moving>();
		if (tag == "Player")
		{
			OwnBase = GameObject.Find("PlayerBase").transform;
		}
		else
		{
			OwnBase = GameObject.Find("EnemyBase").transform;
		}
		InvokeRepeating("UpdateWood", 0f, 2f);
		InvokeRepeating("UpdateStone", 0f, 2f);
		InvokeRepeating("UpdateBuilding", 0f, 2f);
	}
	
	void Update ()
	{
		if (cut)
		{
			if (targetTree == null)
			{
				axe.GetComponent<Animation>().Stop("gettingwood");
				axe.GetComponent<Animation>().Stop("AxeCut");
			}
			if (targetTree != null)
			{
				float range = (transform.position - targetTree.transform.position).sqrMagnitude;
				//float range = Vector3.Distance(transform.position, targetTree.transform.position);
				axe.SetActive(true);
				stoneMining = false;

				if (wood >= 20)
				{
					axe.GetComponent<Animation>().Stop("gettingwood");
					axe.GetComponent<Animation>().Stop("AxeCut");
					if (OwnBase != null)
					{
						target = OwnBase.transform;
					}
					
					log.SetActive(true);
					log.GetComponent<Animation>().Play("LogsAnim");
				}
				else
				{

					target = targetTree.transform;
					if (targetTree.GetComponent<Tree>().wood <= 0)
					{
						target = OwnBase.transform;
						targetTree = null;
						return;
					}
					log.SetActive(false);
					if (range < 3f*3f)
					{
						targetTree.GetComponent<Tree>().cuttingtree = true;
						if (targetTree.GetComponent<Tree>().cutted == false)
						{
							axe.GetComponent<Animation>().Play("AxeCut");
						}
						else
						{
							axe.GetComponent<Animation>().Play("gettingwood");
						}

						if (count <= 0 && targetTree.GetComponent<Tree>().cutted == true)
						{
							wood += 2;
							targetTree.GetComponent<Tree>().wood -= 2;
							count = countReset;
							
						}

						if (targetTree.GetComponent<Tree>().cutted == true)
						{
							count -= Time.deltaTime;
						}
					}
				}
			}
			if (target != null && Vector3.Distance(transform.position, target.transform.position) > 4f)
			{
				FaceTarget();
				move.MoveToPoint(target.position);
			}
			
		}
		else if (stoneMining)
		{
			if (targetStone != null)
			{
				FaceTarget();
				//float range = (transform.position - targetStone.transform.position).sqrMagnitude;
				float range = Vector3.Distance(transform.position, targetStone.transform.position);
				
				mineaxe.SetActive(true);
				cut = false;
				if (stone > 0)
				{
					rock.SetActive(true);
				}
				else
				{
					rock.SetActive(false);
				}

				if (stone >= 20)
				{
					
					if (OwnBase != null)
					{
						target = OwnBase.transform;
						mineaxe.GetComponent<Animation>().Stop("SwingPicAxe");
					}
					move.MoveToPoint(target.position);
				}
				else
				{
					if (range >= targetStone.transform.localScale.x + 0.5f)
					{
						move.MoveToPoint(target.position);
					}
					else
					{
						move.MoveToPoint(transform.position);
					}

					target = targetStone.transform;

					if (targetStone.GetComponent<stone>().Stone <= 0)
					{
						target = OwnBase.transform;
						targetStone = null;
						return;
					}

					if (range <= 1.5f + targetStone.transform.localScale.x)
					{
						mineaxe.GetComponent<Animation>().Play("SwingPicAxe");
						if (count <= 0)
						{
							stone += 2;
							targetStone.GetComponent<stone>().Stone -= 2;
							count = countReset;

						}
						count -= Time.deltaTime;
					}
					else
					{
						mineaxe.GetComponent<Animation>().Stop("SwingPicAxe");
					}
				}
			}
		}
		else if (mine)
		{
			FaceTarget();
			move.MoveToPoint(target.position);
			mineaxe.SetActive(true);
			if (gold > 0)
			{
				target = OwnBase.transform;
				bag.SetActive(true);
				bag.GetComponent<Animation>().Play("GoldBag");
			} 
			else
			{
				target = targetMine;
				bag.SetActive(false);
			}
		}
		else if (build)
		{
			hammer.SetActive(true);
			if (target != null)
			{
				if (targetBuilding.GetComponent<BuildingInProgress>().count < 101)
				{
					FaceTarget();
					//float range = (transform.position - targetBuilding.transform.position).sqrMagnitude;
					float range = Vector3.Distance(transform.position, targetBuilding.transform.position);
					if (range >= targetBuilding.GetComponent<buildingSelected>().range+0.5f)
					{
						move.MoveToPoint(target.position);
					}
					else
					{
						move.MoveToPoint(transform.position);
					}

					if (range <= targetBuilding.GetComponent<buildingSelected>().range+1f && count <= 0)
					{
						hammer.GetComponent<Animation>().Play("BuildAnim");
						targetBuilding.GetComponent<BuildingInProgress>().count += 2.5f;
						count = 1f;
					}
					count -= Time.deltaTime;
				}
				else
				{
					if (targetBuilding.name == "AppleFarm(Clone)")
					{
						target = targetBuilding.transform;
						TargetFarm = targetBuilding;
						targetBuilding = null;
						build = false;
						gather = true;
						hammer.SetActive(false);
					}
					else
					{
						target = null;
						targetBuilding = null;
					}
				}	
			}	
		}
		else if (gather)
		{
			build = false;
			if (target != null)
			{
				FaceTarget();
				float distanceToTarget = Vector3.Distance(transform.position, target.position);
				if (distanceToTarget >= 1f)
				{
					move.MoveToPoint(target.position);
				}
				else
				{
					move.MoveToPoint(transform.position);
				}
				
			}
			TargetFarm.GetComponent<FarmApples>().farmerOnDuty = true;
			if (food < 20)
			{

				//float distanceToFarm = Vector3.Distance(transform.position, TargetFarm.transform.position);
				float distanceToFarm = (transform.position - TargetFarm.transform.position).sqrMagnitude;
				if (distanceToFarm > 12f*12f)
				{
					target = TargetFarm.transform;
				}
				if (distanceToFarm <= 8f*8f)
				{
					if (TargetFarm.GetComponent<FarmApples>().x >= 8)
					{
						if (TargetFarm.GetComponent<CheckApples>().apples == true)
						{
							if (TargetFarm.GetComponent<FarmApples>().Trees[0].GetComponent<GotApples>().Apples == true)
							{
								target = TargetFarm.GetComponent<FarmApples>().Trees[0].transform;
							}
							else if (TargetFarm.GetComponent<FarmApples>().Trees[1].GetComponent<GotApples>().Apples == true)
							{
								target = TargetFarm.GetComponent<FarmApples>().Trees[1].transform;
							}
							else if (TargetFarm.GetComponent<FarmApples>().Trees[2].GetComponent<GotApples>().Apples == true)
							{
								target = TargetFarm.GetComponent<FarmApples>().Trees[2].transform;
							}
							else if (TargetFarm.GetComponent<FarmApples>().Trees[3].GetComponent<GotApples>().Apples == true)
							{
								target = TargetFarm.GetComponent<FarmApples>().Trees[3].transform;
							}
							else if (TargetFarm.GetComponent<FarmApples>().Trees[4].GetComponent<GotApples>().Apples == true)
							{
								target = TargetFarm.GetComponent<FarmApples>().Trees[4].transform;
							}
							else if (TargetFarm.GetComponent<FarmApples>().Trees[5].GetComponent<GotApples>().Apples == true)
							{
								target = TargetFarm.GetComponent<FarmApples>().Trees[5].transform;
							}
							else if (TargetFarm.GetComponent<FarmApples>().Trees[6].GetComponent<GotApples>().Apples == true)
							{
								target = TargetFarm.GetComponent<FarmApples>().Trees[6].transform;
							}
							else if (TargetFarm.GetComponent<FarmApples>().Trees[7].GetComponent<GotApples>().Apples == true)
							{
								target = TargetFarm.GetComponent<FarmApples>().Trees[7].transform;
							}
							else
							{
								target = TargetFarm.transform;
							}
						}
						else
						{
							target = TargetFarm.transform;
						}
					}
					else
					{
						target = TargetFarm.transform;
					}
				}
			}
			else
			{
				target = OwnBase.transform;
			}
		}
		else
		{
			mineaxe.SetActive(false);
			axe.SetActive(false);
			hammer.SetActive(false);
			if (gold <= 0)
			{
				bag.SetActive(false);
			}
		}
	}
	void UpdateBuilding()
	{
		if (build)
		{
			if (targetBuilding == null)
			{
				GameObject[] buildings = GameObject.FindGameObjectsWithTag("Player");
				float shortestDistance = Mathf.Infinity;
				GameObject nearestBuild = null;

				foreach (GameObject b in buildings)
				{
					float distanceToEnemy = (transform.position - b.transform.position).sqrMagnitude;
					//float distanceToEnemy = Vector3.Distance(transform.position, tree.transform.position);
					if (distanceToEnemy < shortestDistance)
					{
						if (b.gameObject.GetComponent<BuildingInProgress>() != null)
						{
							if (b.gameObject.GetComponent<BuildingInProgress>().building == false)
							{
								shortestDistance = distanceToEnemy;
								nearestBuild = b;
							}
						}
					}
				}
				if (nearestBuild != null && shortestDistance <= 1000f)
				{
					targetBuilding = nearestBuild;
					target = targetBuilding.transform;
				}
				else
				{
					targetBuilding = null;
					build = false;
				}
			}
		}
	}
	void UpdateWood()
	{
		if (cut)
		{
			if (targetTree == null && wood <= 0)
			{
				GameObject[] Trees = GameObject.FindGameObjectsWithTag("Tree");
				float shortestDistance = Mathf.Infinity;
				GameObject nearestTree = null;

				foreach (GameObject tree in Trees)
				{
					//float distanceToEnemy = (transform.position - tree.transform.position).sqrMagnitude;
					float distanceToEnemy = Vector3.Distance(transform.position, tree.transform.position);
					if (distanceToEnemy < shortestDistance)
					{
						if (tree.gameObject.GetComponent<Tree>().wood > 0)
						{
							shortestDistance = distanceToEnemy;
							nearestTree = tree;
						}
					}
				}
				if (nearestTree != null && shortestDistance <= 500f)
				{
					targetTree = nearestTree;
				}
				else
				{
					targetTree = null;
					cut = false;
				}
			}
		}
	}
	void UpdateStone()
	{
		if (stoneMining && targetStone == null && stone <= 0)
		{
			GameObject[] Stones = GameObject.FindGameObjectsWithTag("Stone");
			float shortestDistance = Mathf.Infinity;
			GameObject nearestStone = null;

			foreach (GameObject stone in Stones)
			{
				//float distanceToStone = (transform.position - stone.transform.position).sqrMagnitude;
				float distanceToStone = Vector3.Distance(transform.position, stone.transform.position);
				if (distanceToStone < shortestDistance)
				{
					if (stone.gameObject.GetComponent<stone>().Stone > 0)
					{
						shortestDistance = distanceToStone;
						nearestStone = stone;
					}
				}
			}
			if (nearestStone != null && shortestDistance <= 500f)
			{
				targetStone = nearestStone;
			}
			else
			{
				targetStone = null;
				stoneMining = false;
			}
		}
	}
	void FaceTarget()
	{
		if (target != null)
		{
			if ((transform.position - target.transform.position).sqrMagnitude > 0.5f*0.5f)
			{
				Vector3 direction = (target.position - transform.position).normalized;
				Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
				transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
			}
			
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 1.5f);
	}
}
