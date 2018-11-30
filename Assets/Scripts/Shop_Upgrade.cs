using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Upgrade : MonoBehaviour {


	public void SelectUpdateWeapon()
	{
		if (PlayerStats.Gold < 500)
		{
			Debug.Log("not enougt gold");
			return;
		}
		PlayerStats.Gold -= 500;
		PlayerStats.UpgradeWeapon += 1;
	}
	public void SelectUpdateShield()
	{
		if (PlayerStats.Gold < 500)
		{
			Debug.Log("not enougt gold");
			return;
		}
		PlayerStats.Gold -= 500;
		PlayerStats.UpgradeShield += 1;
	}

}
