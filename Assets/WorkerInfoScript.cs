using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerInfoScript : MonoBehaviour {

	public Text Gold;
	public Text Wood;
	public Text Food;
	public Text Stone;

	public void Infos(int gold, int wood, int food, int stone)
	{
		Gold.text = "Gold: " + gold.ToString();
		Wood.text = "Wood: " + wood.ToString();
		Food.text = "Food: " + food.ToString();
		Stone.text = "Stone: " + stone.ToString();
	}
}
