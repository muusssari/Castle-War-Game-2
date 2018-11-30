using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Units : MonoBehaviour {

    public UnitBlueprints soldier;
    public UnitBlueprints archer;
    public UnitBlueprints Miner;
	public UnitBlueprints Catapult;

	public Text costS;
	public Text costA;
	public Text costM;
	public Text costC;

	BuildingManager buildingManager;
    

    void Start()
    {
        buildingManager = BuildingManager.instance;
    }

	private void Update()
	{
		costS.text = soldier.cost.ToString();
		costA.text = archer.cost.ToString();
		costM.text = Miner.cost.ToString();
		costC.text = Catapult.cost.ToString();
	}

	public void SelectSoldier()
    { 
        Debug.Log("Soldier selectet");
        buildingManager.SelectUnitToBuild(soldier);
    }
    public void SelectMiner()
    {
        Debug.Log("Miner selectet");
        buildingManager.SelectUnitToBuild(Miner);
    }
    public void SelectArcher()
    {
        Debug.Log("Archer selectet");
        buildingManager.SelectUnitToBuild(archer);
    }
	public void SelectCatapult()
	{
		Debug.Log("Archer selectet");
		buildingManager.SelectUnitToBuild(Catapult);
	}
}
