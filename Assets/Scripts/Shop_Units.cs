using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Units : MonoBehaviour {

    public UnitBlueprints soldier;
    public UnitBlueprints archer;
	public UnitBlueprints Catapult;

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

	public void SelectSoldier()
    { 
        buildingManager.SelectUnitToBuild(soldier);
    }
    public void SelectArcher()
    {
        buildingManager.SelectUnitToBuild(archer);
    }
	public void SelectCatapult()
	{
		buildingManager.SelectUnitToBuild(Catapult);
	}

	public void HowerOnButtonsoldier()
	{
		MaterialInfo.SetActive(true);
		if (soldier.gold > 0)
		{
			gold.text = " Gold: " + soldier.gold.ToString();
			gold.gameObject.SetActive(true);
		}
		else
		{
			gold.gameObject.SetActive(false);
		}

		if (soldier.wood > 0)
		{
			wood.text = " Wood: " + soldier.wood.ToString();
			wood.gameObject.SetActive(true);
		}
		else
		{
			wood.gameObject.SetActive(false);
		}

		if (soldier.food > 0)
		{

			food.text = " Food: " + soldier.food.ToString();
			food.gameObject.SetActive(true);
		}
		else
		{
			food.gameObject.SetActive(false);
		}

		if (soldier.stone > 0)
		{
			stone.text = " Stone: " + soldier.stone.ToString();
			stone.gameObject.SetActive(true);
		}
		else
		{
			stone.gameObject.SetActive(false);
		}
	}
	public void HowerOnButtonarcher()
	{
		MaterialInfo.SetActive(true);
		if (archer.gold > 0)
		{
			gold.text = " Gold: " + archer.gold.ToString();
			gold.gameObject.SetActive(true);
		}
		else
		{
			gold.gameObject.SetActive(false);
		}

		if (archer.wood > 0)
		{
			wood.text = " Wood: " + archer.wood.ToString();
			wood.gameObject.SetActive(true);
		}
		else
		{
			wood.gameObject.SetActive(false);
		}

		if (archer.food > 0)
		{

			food.text = " Food: " + archer.food.ToString();
			food.gameObject.SetActive(true);
		}
		else
		{
			food.gameObject.SetActive(false);
		}

		if (archer.stone > 0)
		{
			stone.text = " Stone: " + archer.stone.ToString();
			stone.gameObject.SetActive(true);
		}
		else
		{
			stone.gameObject.SetActive(false);
		}
	}
	public void HowerOnButtonCatapult()
	{
		MaterialInfo.SetActive(true);
		if (Catapult.gold > 0)
		{
			gold.text = " Gold: " + Catapult.gold.ToString();
			gold.gameObject.SetActive(true);
		}
		else
		{
			gold.gameObject.SetActive(false);
		}

		if (Catapult.wood > 0)
		{
			wood.text = " Wood: " + Catapult.wood.ToString();
			wood.gameObject.SetActive(true);
		}
		else
		{
			wood.gameObject.SetActive(false);
		}

		if (Catapult.food > 0)
		{

			food.text = " Food: " + Catapult.food.ToString();
			food.gameObject.SetActive(true);
		}
		else
		{
			food.gameObject.SetActive(false);
		}

		if (Catapult.stone > 0)
		{
			stone.text = " Stone: " + Catapult.stone.ToString();
			stone.gameObject.SetActive(true);
		}
		else
		{
			stone.gameObject.SetActive(false);
		}
	}
	public void HowerOffButton()
	{
		MaterialInfo.SetActive(false);
	}
}
