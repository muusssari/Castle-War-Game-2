using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotApples : MonoBehaviour {

	public bool Apples = false;

	private void Update()
	{
		if (Apples)
		{
			transform.Find("Apples").gameObject.SetActive(true);
		}
		else
		{
			transform.Find("Apples").gameObject.SetActive(false);
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (Apples)
		{
			if (other.transform.root.gameObject.name == "Player_Worker(Clone)")
			{
				Apples = false;
				transform.root.gameObject.GetComponent<CheckApples>().apples = false;
				other.transform.root.gameObject.GetComponent<Miner>().food += 5;
			}
		}
	}
}
