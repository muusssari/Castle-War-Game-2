using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultShoot : MonoBehaviour {

	public float hitRate = 2f;
	public float hitCountdown = 0f;
	public float damage = 1f;
	public Transform target;
	public GameObject targetThing;
	public Transform firePoint;
	public Rigidbody StonePrefab;
	Unit_Moving move;
	public float attackRange;
	Animation pole;
	public float dis;
	public float time = 1f;

	void Start ()
	{
		move = GetComponent<Unit_Moving>();
		pole = transform.Find("Model/Pole").GetComponent<Animation>();
	}
	
	void Update ()
	{
		target = GetComponent<SeachForEnemy>().target;
		targetThing = GetComponent<SeachForEnemy>().targetThing;

		if (target != null && targetThing != null)
		{

			dis = Vector3.Distance(firePoint.position, target.position);
			time = ((dis - 5f) / (40f - 5f)) * (3f - 1.6f) + 1.6f;

			if (dis > attackRange)
			{
				move.MoveToPoint(target.position);
			}
			else
			{
				move.MoveToPoint(transform.position);
			}

			if (dis <= attackRange)
			{
				
				Vector3 Vo = CalculateVelocity(target.position, firePoint.position, time);
				firePoint.transform.rotation = Quaternion.LookRotation(Vo);
				if (hitCountdown <= 0.1f && targetThing != null)
				{
					pole.Play("CatapultShoot");
				}

				if (hitCountdown <= 0f && targetThing != null)
				{
					pole.Play("CatapultShoot");
					Shoot(Vo);
					hitCountdown = 7f / hitRate;
				}

				hitCountdown -= Time.deltaTime;
			}

			FaceTarget();
		}
		if (target == null)
		{
			hitCountdown = 1f;
		}

	}
	void Shoot(Vector3 Vo)
	{
		Rigidbody obj = Instantiate(StonePrefab, firePoint.position, Quaternion.identity);
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
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
