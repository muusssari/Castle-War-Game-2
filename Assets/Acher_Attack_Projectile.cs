using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acher_Attack_Projectile : MonoBehaviour {

	public float hitRate = 2f;
	public float hitCountdown = 0f;
	public float damage = 1f;
	private Transform target;
	private GameObject targetThing;
	private Items item;
	public Transform firePoint;
	public Rigidbody arrowPrefab;
	Unit_Moving move;
	private float attackRange;
	public bool moving;

	public float dis;
	public float time = 1f;


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
			dis = Vector3.Distance(firePoint.position, target.position);
			time = ((dis - 2f) / (attackRange - 2f)) * (2f - 0.5f) + 0.5f;

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
			Vector3 Vo = CalculateVelocity(target.position + Vector3.up/2, firePoint.position, time);
			firePoint.transform.rotation = Quaternion.LookRotation(Vo);

			if (dis <= attackRange)
			{

				if (hitCountdown <= 1f && targetThing != null)
				{
					item.ShootAnimation();
				}

				if (hitCountdown <= 0f && targetThing != null)
				{
					Shoot(Vo);
					hitCountdown = 8f / hitRate;
				}

				hitCountdown -= Time.deltaTime;
			}
			else
			{
				item.ShootAnimationStop();
			}

			FaceTarget();
		}
		if (target == null)
		{
			hitCountdown = 3f;
		}
	}

	void Shoot(Vector3 Vo)
	{
		Rigidbody obj = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
		obj.velocity = Vo;
	}

	Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float t)
	{
		Vector3 distance = target - origin;
		Vector3 distanceXZ = distance;
		distanceXZ.y = 0f;

		float Sy = distance.y;
		float Sxz = distanceXZ.magnitude;

		float Vxz = Sxz / t;
		float Vy = Sy / t + 0.5f * Mathf.Abs(Physics.gravity.y) * t;

		Vector3 result = distanceXZ.normalized;
		result *= Vxz;
		result.y = Vy;

		return result;
	}

	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
	}
}
