using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckApples : MonoBehaviour {

	public GameObject[] AppleTrees;
	public bool apples = false;

	void LateUpdate ()
	{
		for (int i = 0; i < AppleTrees.Length; i++)
		{
			if (AppleTrees[i].GetComponent<GotApples>().Apples == true)
			{
				apples = true;
				return;
			}
		}
	}
}
