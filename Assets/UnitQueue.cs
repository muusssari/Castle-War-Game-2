using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitQueue : MonoBehaviour {


	public UnitBlueprints[] Units = new UnitBlueprints[12];
	public Text[] ques;
	public Text procent;
	public GameObject SelectedBuilding;

	private void Start()
	{
		for (int i = 0; i < 12; i++)
		{
			ques[i].text = " ";
		}
	}
	public void UnitProcent(float b)
	{
		procent.text = b.ToString() + "%";
	}
	public void AddQues(List<UnitBlueprints> unitQueue, GameObject a)
	{
		SelectedBuilding = a;
		Units = unitQueue.ToArray();
		if (Units.Length <= 0)
		{
			procent.gameObject.SetActive(false);
		}
		else
		{
			procent.gameObject.SetActive(true);
		}

		for (int i = 0; i < 12; i++)
		{
			ques[i].text = " ";
		}
		for (int i = 0; i < Units.Length; i++)
		{
			string a1 = Units[i].prefab.name.Replace("(Clone)", "");
			string a2 = a1.Replace("Player_", "");
			string b = a2.Replace("Unit_", "");
			ques[i].text = b;
		}
	}


	public void UnitRemove(int a)
	{
		if (Units.Length <= 0)
		{
			return;
		}
		else if (Units.Length > a)
		{
			PlayerStats.Gold += Units[a].gold;
			PlayerStats.Wood += Units[a].wood;
			PlayerStats.Food += Units[a].food;
			PlayerStats.Stone += Units[a].stone;
			SelectedBuilding.GetComponent<buildingSelected>().UnitQueue.Remove(Units[a]);
			return;
		}
		else
		{
			return;
		}
	}
	
}
