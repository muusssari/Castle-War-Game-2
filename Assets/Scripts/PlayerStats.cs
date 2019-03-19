using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public static int Gold;
	public static int Wood;
	public static int Stone;
	public static int Food;
	public static float UpgradeWeapon;
	public static float UpgradeShield;
	public int startGold = 400;
	public int startWood = 100;
	public int startStone = 100;
	public int startFood = 100;
	public static int Maxpop = 10;
	public static int Popnow = 0;
	public Text goldtext;
	public Text woodtext;
	public Text stonetext;
	public Text foodtext;
	public Text populationtext;

	private void Start()
    {
		Gold = startGold;
		Wood = startWood;
		Stone = startStone;
		Food = startFood;

		GameStats.GetGold = 0;
		GameStats.UnitsAmount = 0;
		GameStats.WorkerAmount = 0;
		GameStats.SoldiersAmount = 0;
		GameStats.ArchersAmount = 0;
		GameStats.CatapultAmount = 0;

		GameStats.UnitKilled = 0;
		GameStats.UnitsDied = 0;

		GameStats.GetGold = 0;
		GameStats.GetWood = 0;
		GameStats.GetStone = 0;
		GameStats.GetFood = 0;
	}
    private void Update()
    {
        goldtext.text = "Gold: " + Gold.ToString();
		woodtext.text = "Wood: " + Wood.ToString();
		stonetext.text = "Stone: " + Stone.ToString();
		foodtext.text = "Food: " + Food.ToString();
		populationtext.text = "Pop: " + Popnow.ToString() + "/" + Maxpop.ToString();
	}

}
