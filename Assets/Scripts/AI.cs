using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public GameObject Enemy_Unit_Soldier;
	public GameObject Enemy_Unit_Archer;
	public GameObject standardBarricadePrefab;
	public GameObject standardAcherTowerPrefab;
	public GameObject Enemy_Miner;
	public GameObject Enemy_Catapult;

	public float timeBetweenUnits = 5f;
    public float countdown = 15f;
    public int amountOfUnits;
    public float timeBetween = 20f;
	public float enemyRange = 500;
	public int x;
	public Transform bases;
	public int towers;
	public int catapults;
	public List<GameObject> platforms = new List<GameObject>();
	public List<GameObject> player_Towers = new List<GameObject>();
	public List<GameObject> enemy_catapults = new List<GameObject>();


	private void Start()
	{
		foreach (GameObject platform in GameObject.FindGameObjectsWithTag("PlatformE"))
		{
			platforms.Add(platform);
		}
	}

	/*void Update ()
    {
        amountOfUnits = Random.Range(1, 3);
        if (countdown <= 0f)
        {
			PlatformCalls();
			x = Random.Range(1, 1000);
			if (x < 500)
			{
				if (EnemyStats.Gold < 20 * amountOfUnits)
				{
					countdown = timeBetweenUnits;
					return;
				}
				StartCoroutine(SpawnUnits());
				countdown = timeBetweenUnits;
			}
			else if (x > 500)
			{
				if (EnemyStats.Gold < 20 * amountOfUnits)
				{
					countdown = timeBetweenUnits*2;
					return;
				}
				
				countdown = timeBetweenUnits*2;
			}
        }
        countdown -= Time.deltaTime;
	}*/


	private void Update()
	{
		amountOfUnits = Random.Range(1, 4);
		CheckUnitsOnMap();
		towers = player_Towers.Count;
		catapults = enemy_catapults.Count;
		if (countdown <= 0f)
		{	
			if(towers >= 1 && catapults < 2)
			{
				Instantiate(Enemy_Catapult, bases.transform.position, Quaternion.identity);
				countdown = timeBetween;
			}
			
			x = Random.Range(1, 1000);
			if (x <= 800)
			{
				StartCoroutine(SpawnUnits());
				countdown = timeBetween;
			}
			if (x > 800)
			{
				PlatformCalls();
				countdown = timeBetween;
			}
		}
		countdown -= Time.deltaTime;
	}

	IEnumerator SpawnUnits ()
    {
        

        for (int i = 0; i < amountOfUnits; i++)
        {
            SpawnUnit();
            yield return new WaitForSeconds(0.9f);
        }
    }

    void SpawnUnit()
    {
		int ran = Random.Range(1, 5);
		if (ran %2 == 0)
		{
			Instantiate(Enemy_Unit_Archer, bases.transform.position, Quaternion.identity);
		}
		else
		{
			Instantiate(Enemy_Unit_Soldier, bases.transform.position, Quaternion.identity);
		}
        
    }

	public void CheckUnitsOnMap()
	{
		foreach (GameObject catapults in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			if (!enemy_catapults.Contains(catapults) && catapults.name == "Enemy_Catapult(Clone)")
			{
				enemy_catapults.Add(catapults);
			}
			else
			{

			}
		}

		foreach (GameObject Player_tower in GameObject.FindGameObjectsWithTag("Tower"))
		{
			if (!player_Towers.Contains(Player_tower))
			{
				player_Towers.Add(Player_tower);
			}
			else
			{
				
			}
		}
	}
	public void PlatformCalls()
	{
		int ran = Random.Range(0, platforms.Count);
		GameObject platform = platforms[ran]; 
			if (platform.GetComponent<Platform>().road == true && platform.GetComponent<Platform>().tower == null)
			{
				platform.GetComponent<Platform>().tower = (GameObject)Instantiate(standardBarricadePrefab, platform.GetComponent<Platform>().GetBuildPosition(), Quaternion.identity);
				
			}
			if (platform.GetComponent<Platform>().road == false && platform.GetComponent<Platform>().tower == null)
			{
				platform.GetComponent<Platform>().tower = (GameObject)Instantiate(standardAcherTowerPrefab, platform.GetComponent<Platform>().GetBuildPosition(), Quaternion.identity);
			}
	}

	

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, enemyRange);
	}
}
