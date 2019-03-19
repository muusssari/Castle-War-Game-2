using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialsNeeded : MonoBehaviour {

	public Text gold;
	public Text wood;
	public Text food;
	public Text stone;
	public int StoneToWall;
	public bool buildingWall = false;

	void Update ()
	{
		if (buildingWall)
		{
			stone.text = " Stone: " + StoneToWall.ToString();
			stone.gameObject.SetActive(true);
			food.gameObject.SetActive(false);
			wood.gameObject.SetActive(false);
			gold.gameObject.SetActive(false);
		}
	}

	public void SetMaterialInfoActive(bool a)
	{
		buildingWall = a;
		gameObject.SetActive(a);
		stone.gameObject.SetActive(a);
	}
}
