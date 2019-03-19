using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Unit_Orders : MonoBehaviour {

	public Transform target = null;
	public GameObject playerbase;

	private void Start()
	{
		playerbase = GameObject.Find("PlayerBase");
	}
	void Update ()
	{
		target = GetComponent<SeachForEnemy>().target;
		if (target == null)
		{
			if (playerbase != null)
			{
				GetComponent<Unit_Moving>().MoveToPoint(playerbase.transform.position);
			}
			
		}
		else if (target != null)
		{
			return;
		}
	}
}
