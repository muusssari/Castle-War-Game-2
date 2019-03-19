using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPosition : MonoBehaviour {

	public Transform floor;
	public Vector3 offset;

	void Update ()
	{
		if (floor != null)
		{
			transform.position = floor.position + offset;
		}
	}
}
