using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	//Prefabs
    public GameObject Enemy_Unit_Soldier;
	public GameObject Enemy_Unit_Archer;
	public GameObject standardBarricadePrefab;
	public GameObject standardAcherTowerPrefab;
	public GameObject Enemy_Miner;
	public GameObject Enemy_Catapult;
	//spawn timers
	public float timeBetweenUnits = 5f;
	public float timeBetween = 20f;
	public float countdown = 15f;
	//spawn meters
    public int amountOfUnits;
	public int x;
	public Vector3 offset;

    //Player Units
	public float enemyRange = 500;



	private void Update()
	{
		
		if (countdown <= 0f)
		{
			amountOfUnits = Random.Range(1, 4);
			x = Random.Range(1, 1000);
			if (x <= 800)
			{
				StartCoroutine(SpawnUnits());
				countdown = timeBetween;
			}
			if (x > 800)
			{
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
		Instantiate(Enemy_Unit_Soldier, (transform.position + offset), Quaternion.identity);
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, enemyRange);
	}
}
