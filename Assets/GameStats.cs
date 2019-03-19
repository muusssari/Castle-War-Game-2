using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour {

	static public int UnitsAmount;
	static public int WorkerAmount;
	static public int SoldiersAmount;
	static public int ArchersAmount;
	static public int CatapultAmount;

	static public int GetGold;
	static public int GetWood;
	static public int GetStone;
	static public int GetFood;

	static public int UnitKilled;
	static public int UnitsDied;

	static public string Win;


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			Stats();
		}
	}
	void Stats()
	{
		Debug.Log("allunits: " + UnitsAmount);
		Debug.Log("UnitKilled: " + UnitKilled);
		Debug.Log("unitsdied: " + UnitsDied);

		Debug.Log("soldiers: " + SoldiersAmount);
		Debug.Log("arhcers: " + ArchersAmount);
		Debug.Log("workers: " + WorkerAmount);
		Debug.Log("catapult: " + CatapultAmount);

	}
}
