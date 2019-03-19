using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit_Moving : MonoBehaviour {

	NavMeshAgent agent;
	public Vector3 destion;
	private GameObject target;
	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
		if (target != null && tag == "Player")
		{
			MoveToPoint(target.transform.position);
		}
	}

	public void MoveToPoint (Vector3 point)
	{
		destion = point;
		agent.SetDestination(point);
	}
	public void TargetDestination(GameObject a)
	{
		target = a;
	}
}
