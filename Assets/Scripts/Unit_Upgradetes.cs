using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Upgradetes : MonoBehaviour {

	public GameObject spear = null;
	private float updateShield = 0;
	private float updateWeapon = 0;

	void Start () {

		if (tag == "Enemy")
		{
		}
		else
		{
			spear = transform.Find("spear").gameObject;
		}

	}
	

	void Update () {
		updateWeapon = PlayerStats.UpgradeWeapon;
		updateShield = PlayerStats.UpgradeShield;


		if (updateWeapon == 2)
		{
			spear.SetActive(true);
		}
		if (updateShield == 2)
		{

		}
	}
}
