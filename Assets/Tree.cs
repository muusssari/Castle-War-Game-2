using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

	public int wood = 20;
	public bool cutted = false;
	public bool cuttingtree = false;
	public bool cuttedTree = false;

	public float count = 4;


	void Update ()
	{
		if (cuttedTree || cutted)
		{
			GetComponent<Animation>().Stop("TreeAnimDefault");
		}
		else
		{
			GetComponent<Animation>().Play("TreeAnimDefault");
		}
		
		if (wood <= 0 && !cuttedTree)
		{
			transform.GetChild(0).gameObject.SetActive(false);
			cuttedTree = true;
			count = 50f;
		}
		if (cuttingtree && !cutted)
		{
			
			if (count <= 0)
			{
				transform.GetChild(2).gameObject.SetActive(false);
				transform.GetChild(3).gameObject.SetActive(false);
				transform.GetChild(0).gameObject.GetComponent<Animation>().Play("TreeFall");
				cutted = true;
			}
			count -= Time.deltaTime;
		}

		if (cuttedTree)
		{
			
			if (count <= 0)
			{
				wood = 100;
				transform.GetChild(2).gameObject.SetActive(true);
				transform.GetChild(3).gameObject.SetActive(true);
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(0).gameObject.transform.Rotate(90,0,0);

				cuttedTree = false;
				cuttingtree = false;
				cutted = false;
				count = 4;
			}
			count -= Time.deltaTime;
		}


	}

}
