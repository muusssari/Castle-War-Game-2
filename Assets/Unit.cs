using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public Vector2 ScreenPos;
	public bool OnScreen;
	public bool Selected = false;
	public GameObject mouseSelect;
	public GameObject ring;
	public GameObject canvas;
	public GameObject shop;

	private void Start()
	{
		mouseSelect = GameObject.Find("MouseSelecting");
		ring = transform.Find("SelectRing/Panel").gameObject;
		canvas = transform.Find("SelectRing").gameObject;
		shop = GameObject.Find("ShopUI");
		
	}

	private void Update()
	{
		if (Selected)
		{
			canvas.SetActive(true);
		}
		else
		{
			canvas.SetActive(false);
		}

		if (!Selected)
		{
			ScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);

			if (mouseSelect.GetComponent<Unit_Selected>().UnitWithinScreenSpace(ScreenPos))
			{
				if (!OnScreen)
				{
					mouseSelect.GetComponent<Unit_Selected>().unitsOnScreen.Add(this.gameObject);
					OnScreen = true;
				}
			}
			else
			{
				if (OnScreen)
				{
					mouseSelect.GetComponent<Unit_Selected>().RemoveFromOnScreenUnits(this.gameObject);
				}
			}
		}
	}
}
