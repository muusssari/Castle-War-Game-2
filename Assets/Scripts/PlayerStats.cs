using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public static int Gold;
	public static float UpgradeWeapon;
	public static float UpgradeShield;
	public int startGold = 400;
    public Text goldtext;

    private void Start()
    {
        Gold = startGold;
		UpgradeWeapon = 1;
    }
    private void Update()
    {
        goldtext.text = "gold: " + Gold.ToString();
		
    }

}
