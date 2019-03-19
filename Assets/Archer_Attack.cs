using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Attack : MonoBehaviour {

	public float hitRate = 2f;
	public float hitCountdown = 0f;
	public float damage = 1f;
	private Transform target;
	private GameObject targetThing;
	private Items item;
	public Transform firePoint;
	public GameObject arrowPrefab;
	public float dis;
	Unit_Moving move;
	private float attackRange;
	public bool moving;


	void Start()
	{
		firePoint = gameObject.transform.Find("Model/ShortBow/firepoint").GetComponent<Transform>();
		move = GetComponent<Unit_Moving>();
		item = GetComponent<Items>();
		attackRange = GetComponent<SeachForEnemy>().attackRange;
	}

	void Update()
	{
		target = GetComponent<SeachForEnemy>().target;
		targetThing = GetComponent<SeachForEnemy>().targetThing;
		if (target != null && targetThing != null)
		{
			if (moving)
			{
				if (dis > attackRange)
				{
					move.MoveToPoint(target.position);
				}
				else
				{
					move.MoveToPoint(transform.position);
				}
			}
			dis = Vector3.Distance(transform.position, target.position);
			

			if (dis <= attackRange)
			{
				
				if (hitCountdown <= 1f && targetThing != null)
				{
					item.ShootAnimation();
				}
				
				if (hitCountdown <= 0f && targetThing != null)
				{
					Shoot();
					hitCountdown = 2f / hitRate;
				}

				hitCountdown -= Time.deltaTime;
			}
			
			FaceTarget();
		}
		if (target == null)
		{
			hitCountdown = 3f;
		}
	}

	void Shoot()
	{
		GameObject arrowFly = (GameObject)Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
		Arrow arrow = arrowFly.GetComponent<Arrow>();

		if (arrow != null)
		{
			arrow.Seek(target, targetThing);
		}
	}
	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
	}
}
