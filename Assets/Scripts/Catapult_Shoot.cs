using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult_Shoot : MonoBehaviour {

	public Transform target;
	public GameObject arrowPrefab;
	public Transform firePoint;
	public GameObject targetThing = null;
	public float range = 40f;
	private float turnSpeed = 6f;
	public float damage = 1f;
	private string enemytag;
	public Transform rotate;
	public float hitRate = 1;
	private float hitCountdown = 0f;
	public float update;
	public Transform tower;


	void Start()
	{
		if (tag == "Enemy")
		{
			enemytag = "Tower";
		}
		else
		{
			enemytag = "TowerE";
		}

		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	void Update()
	{


		if (target != null)
		{
			GetComponent<Unit_Movement>().enabled = false;

			if (target != null)
			{
				Vector3 dir = target.position - transform.position;
				Quaternion lookingRotation = Quaternion.LookRotation(dir);
				Vector3 rotation = Quaternion.Lerp(rotate.rotation, lookingRotation, Time.deltaTime * turnSpeed).eulerAngles;
				rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
			}
			if (hitCountdown <= 0f && targetThing != null)
			{
				Shoot();
				hitCountdown = 1f / hitRate;
			}

			hitCountdown -= Time.deltaTime;
		}
		
		else
		{
			GetComponent<Unit_Movement>().enabled = true;
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

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		targetThing = null;

		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetThing = nearestEnemy;
		}
		else
		{
			target = null;
			targetThing = null;
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
