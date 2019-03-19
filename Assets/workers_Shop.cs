using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class workers_Shop : MonoBehaviour {

	public UnitBlueprints worker;
	public GameObject MaterialInfo;
	public Text gold;
	public Text wood;
	public Text food;
	public Text stone;

	public BuildingManager buildingManager;


	void Start()
	{
		buildingManager = BuildingManager.instance;
	}

	public void SelectWorker()
	{
		buildingManager.SelectUnitToBuild(worker);
	}

	public void HowerOnButtonWorker()
	{
		MaterialInfo.SetActive(true);
		if (worker.gold > 0)
		{
			gold.text = " Gold: " + worker.gold.ToString();
			gold.gameObject.SetActive(true);
		}
		else
		{
			gold.gameObject.SetActive(false);
		}
		
		if (worker.wood > 0)
		{
			wood.text = " Wood: " + worker.wood.ToString();
			wood.gameObject.SetActive(true);
		}
		else
		{
			wood.gameObject.SetActive(false);
		}
		
		if (worker.food > 0)
		{

			food.text = " Food: " + worker.food.ToString();
			food.gameObject.SetActive(true);
		}
		else
		{
			food.gameObject.SetActive(false);
		}
		
		if (worker.stone > 0)
		{
			stone.text = " Stone: " + worker.stone.ToString();
			stone.gameObject.SetActive(true);
		}
		else
		{
			stone.gameObject.SetActive(false);
		}
		
	}
	public void HowerOffButtonWorker()
	{
		MaterialInfo.SetActive(false);
	}
}
