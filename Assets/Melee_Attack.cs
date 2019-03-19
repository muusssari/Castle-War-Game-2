using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Attack : MonoBehaviour {

	Unit_Moving move;
	public float hitRate = 1f;
	public float hitCountdown = 0f;
	public float damage = 1f;
	private Transform target;
	private GameObject targetThing;
	private Items item;

	void Start ()
	{
		move = GetComponent<Unit_Moving>();
		item = GetComponent<Items>();
	}
	
	void Update ()
	{
		target = GetComponent<SeachForEnemy>().target;
		targetThing = GetComponent<SeachForEnemy>().targetThing;
		if (target != null)
		{
			if (Vector3.Distance(transform.position, target.position) >= 1.5f)
			{
				move.MoveToPoint(target.position);
			}
			else
			{
				move.MoveToPoint(transform.position);
			}
			
			FaceTarget();
			if (hitCountdown <= 0f && targetThing != null && Vector3.Distance(transform.position, target.position) <= 2.5f)
			{
				Hit();
				item.MeleeAttackAnimation();
				hitCountdown = 1f / hitRate;
			}

			hitCountdown -= Time.deltaTime;
		}
		if (target == null)
		{
			hitCountdown = 2f;
		}
	}

	void Hit()
	{
		target.GetComponent<Unit_Healt>().GetHit(damage);
	}
	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
	}
}

