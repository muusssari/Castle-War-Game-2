using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplaying : MonoBehaviour {

	public Text allUnits;
	public Text soldiers;
	public Text archers;
	public Text workers;
	public Text catapults;

	public Text gold;
	public Text wood;
	public Text stone;
	public Text food;

	public Text UnitsKilled;
	public Text UnitsDied;

	public Text win;

	void Start ()
	{
		allUnits.text = GameStats.UnitsAmount.ToString();
		soldiers.text = GameStats.SoldiersAmount.ToString();
		archers.text = GameStats.ArchersAmount.ToString();
		workers.text = GameStats.WorkerAmount.ToString();
		catapults.text = GameStats.CatapultAmount.ToString();

		gold.text = GameStats.GetGold.ToString();
		wood.text = GameStats.GetWood.ToString();
		stone.text = GameStats.GetStone.ToString();
		food.text = GameStats.GetFood.ToString();

		UnitsKilled.text = GameStats.UnitKilled.ToString();
		UnitsDied.text = GameStats.UnitsDied.ToString();

		win.text = GameStats.Win.ToString();
	}

}
