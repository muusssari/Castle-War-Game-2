using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaColliderScript : MonoBehaviour {

	private bool coll = false;
	private void Start()
	{
		InvokeRepeating("CheckColl", 0f, 2f);
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "World" || other.tag == "Tree" || other.gameObject.layer == 11 || other.name == "BaseArea" || other.name == "ColliderFarm" || other.tag == "Mine")
		{
			coll = true;
		}
	}

	private void Update()
	{
		if (coll)
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
		}
	}

	private void CheckColl()
	{
		coll = false;
	}


}
