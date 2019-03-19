using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMove : MonoBehaviour {


	Unit_Moving move;
	public Vector3 offset;

	void Start ()
	{
		move = GetComponent<Unit_Moving>();
		move.MoveToPoint(offset);
	}
	
}
