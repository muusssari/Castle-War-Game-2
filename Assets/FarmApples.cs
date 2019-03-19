using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmApples : MonoBehaviour {

	public GameObject[] Trees;
	public float count = 5;
	private float countReset;
	public int x = 0;
	public int y = 0;
	public bool farmerOnDuty;

	void Start ()
	{
		countReset = count;
	}
	
	void Update ()
	{
		if (farmerOnDuty && x <= 7)
		{
			if (count <= 0)
			{
				Trees[x].SetActive(true);
				x += 1;
				count = countReset;
			}
			count -= Time.deltaTime;
		}
		if (x > 7)
		{
			if (count <= 0)
			{
				Trees[y].GetComponent<GotApples>().Apples = true;
				y += 1;
				if (y >= 8)
				{
					y = 0;
				}
				count = countReset;
			}
			count -= Time.deltaTime;
		}
	}

	
}
