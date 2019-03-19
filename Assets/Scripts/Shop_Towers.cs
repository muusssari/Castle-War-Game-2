using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Towers : MonoBehaviour {

    public TowerBlueprints forge;
    public TowerBlueprints archertower;
	public TowerBlueprints hut;
	public TowerBlueprints barraks;
	public TowerBlueprints AppleFarm;
	public TowerBlueprints Wall;

	public GameObject MaterialInfo;
	public Text gold;
	public Text wood;
	public Text food;
	public Text stone;

	BuildingManager buildingManager;

	void Start ()
	{
        buildingManager = BuildingManager.instance;
	}

	public void SelectBarraks()
	{
		buildingManager.SelectTowerToBuild(barraks);
	}
	public void SelectForge()
    {
        buildingManager.SelectTowerToBuild(forge);
    }
    public void SelectArcherTower()
    {
        buildingManager.SelectTowerToBuild(archertower);
    }
	public void SelectHouse()
	{
		buildingManager.SelectTowerToBuild(hut);
	}
	public void SelectAppleFarm()
	{
		buildingManager.SelectTowerToBuild(AppleFarm);
	}
	public void SelectWall()
	{
		buildingManager.SelectTowerToBuild(Wall);
	}
	
	public void HowerOnButtonforge()
	{
		MaterialInfo.SetActive(true);
		if (forge.gold > 0)
		{
			gold.text = " Gold: " + forge.gold.ToString();
			gold.gameObject.SetActive(true);
		}
		else
		{
			gold.gameObject.SetActive(false);
		}

		if (forge.wood > 0)
		{
			wood.text = " Wood: " + forge.wood.ToString();
			wood.gameObject.SetActive(true);
		}
		else
		{
			wood.gameObject.SetActive(false);
		}

		if (forge.food > 0)
		{

			food.text = " Food: " + forge.food.ToString();
			food.gameObject.SetActive(true);
		}
		else
		{
			food.gameObject.SetActive(false);
		}

		if (forge.stone > 0)
		{
			stone.text = " Stone: " + forge.stone.ToString();
			stone.gameObject.SetActive(true);
		}
		else
		{
			stone.gameObject.SetActive(false);
		}
	}
	public void HowerOnButtonWall()
	{
		MaterialInfo.SetActive(true);
		if (Wall.gold > 0)
		{
			gold.text = " Gold: " + Wall.gold.ToString();
			gold.gameObject.SetActive(true);
		}
		else
		{
			gold.gameObject.SetActive(false);
		}

		if (Wall.wood > 0)
		{
			wood.text = " Wood: " + Wall.wood.ToString();
			wood.gameObject.SetActive(true);
		}
		else
		{
			wood.gameObject.SetActive(false);
		}

		if (Wall.food > 0)
		{

			food.text = " Food: " + Wall.food.ToString();
			food.gameObject.SetActive(true);
		}
		else
		{
			food.gameObject.SetActive(false);
		}

		if (Wall.stone > 0)
		{
			stone.text = " Stone: " + Wall.stone.ToString();
			stone.gameObject.SetActive(true);
		}
		else
		{
			stone.gameObject.SetActive(false);
		}
	}

	public void HowerOnButtonarchertower()
	{
		MaterialInfo.SetActive(true);
		if (archertower.gold > 0)
		{
			gold.text = " Gold: " + archertower.gold.ToString();
			gold.gameObject.SetActive(true);
		}
		else
		{
			gold.gameObject.SetActive(false);
		}

		if (archertower.wood > 0)
		{
			wood.text = " Wood: " + archertower.wood.ToString();
			wood.gameObject.SetActive(true);
		}
		else
		{
			wood.gameObject.SetActive(false);
		}

		if (archertower.food > 0)
		{

			food.text = " Food: " + archertower.food.ToString();
			food.gameObject.SetActive(true);
		}
		else
		{
			food.gameObject.SetActive(false);
		}

		if (archertower.stone > 0)
		{
			stone.text = " Stone: " + archertower.stone.ToString();
			stone.gameObject.SetActive(true);
		}
		else
		{
			stone.gameObject.SetActive(false);
		}
	}

	public void HowerOnButtonhut()
	{
		MaterialInfo.SetActive(true);
		if (hut.gold > 0)
		{
			gold.text = " Gold: " + hut.gold.ToString();
			gold.gameObject.SetActive(true);
		}
		else
		{
			gold.gameObject.SetActive(false);
		}

		if (hut.wood > 0)
		{
			wood.text = " Wood: " + hut.wood.ToString();
			wood.gameObject.SetActive(true);
		}
		else
		{
			wood.gameObject.SetActive(false);
		}

		if (hut.food > 0)
		{

			food.text = " Food: " + hut.food.ToString();
			food.gameObject.SetActive(true);
		}
		else
		{
			food.gameObject.SetActive(false);
		}

		if (hut.stone > 0)
		{
			stone.text = " Stone: " + hut.stone.ToString();
			stone.gameObject.SetActive(true);
		}
		else
		{
			stone.gameObject.SetActive(false);
		}
	}

	public void HowerOnButtonbarraks()
	{
		MaterialInfo.SetActive(true);
		if (barraks.gold > 0)
		{
			gold.text = " Gold: " + barraks.gold.ToString();
			gold.gameObject.SetActive(true);
		}
		else
		{
			gold.gameObject.SetActive(false);
		}

		if (barraks.wood > 0)
		{
			wood.text = " Wood: " + barraks.wood.ToString();
			wood.gameObject.SetActive(true);
		}
		else
		{
			wood.gameObject.SetActive(false);
		}

		if (barraks.food > 0)
		{

			food.text = " Food: " + barraks.food.ToString();
			food.gameObject.SetActive(true);
		}
		else
		{
			food.gameObject.SetActive(false);
		}

		if (barraks.stone > 0)
		{
			stone.text = " Stone: " + barraks.stone.ToString();
			stone.gameObject.SetActive(true);
		}
		else
		{
			stone.gameObject.SetActive(false);
		}
	}

	public void HowerOnButtonAppleFarm()
	{
		MaterialInfo.SetActive(true);
		if (AppleFarm.gold > 0)
		{
			gold.text = " Gold: " + AppleFarm.gold.ToString();
			gold.gameObject.SetActive(true);
		}
		else
		{
			gold.gameObject.SetActive(false);
		}

		if (AppleFarm.wood > 0)
		{
			wood.text = " Wood: " + AppleFarm.wood.ToString();
			wood.gameObject.SetActive(true);
		}
		else
		{
			wood.gameObject.SetActive(false);
		}

		if (AppleFarm.food > 0)
		{

			food.text = " Food: " + AppleFarm.food.ToString();
			food.gameObject.SetActive(true);
		}
		else
		{
			food.gameObject.SetActive(false);
		}

		if (AppleFarm.stone > 0)
		{
			stone.text = " Stone: " + AppleFarm.stone.ToString();
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
