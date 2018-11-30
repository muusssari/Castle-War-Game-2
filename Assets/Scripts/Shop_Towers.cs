using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Towers : MonoBehaviour {

    public TowerBlueprints barricade;
    public TowerBlueprints archertower;
	public Text costB;
	public Text costA;

	BuildingManager buildingManager;

	void Start () {
        buildingManager = BuildingManager.instance;
	}
	private void Update()
	{
		costB.text = barricade.cost.ToString();
		costA.text = archertower.cost.ToString();
	}

	public void SelectBarricade()
    {
        Debug.Log("Barrigade selectet");
        buildingManager.SelectTowerToBuild(barricade);
    }
    public void SelectArcherTower()
    {
        Debug.Log("Archer Tower selectet");
        buildingManager.SelectTowerToBuild(archertower);
    }
}
