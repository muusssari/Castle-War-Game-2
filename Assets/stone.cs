using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone : MonoBehaviour {

	public int Stone = 100;



	void Update()
	{

		if (Stone <= 0)
		{
			Destroy(gameObject);
		}



	}
}
