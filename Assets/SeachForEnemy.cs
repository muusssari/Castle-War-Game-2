using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeachForEnemy : MonoBehaviour {

	public Transform target = null;
	public GameObject targetThing = null;
	private string enemytag;
	public float attackRange;
	public Vector3 offset;


	void Start()
	{
		if (tag == "Player")
		{
			enemytag = "Enemy";
		}
		else
		{
			enemytag = "Player";
		}
		InvokeRepeating("UpdateTarget", 0f, 1f);
	}

	void UpdateTarget()
	{
		Vector3 ownPos = transform.position + offset;
		if (targetThing == null && target == null)
		{
			GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);

			float shortestDistance = Mathf.Infinity;
			GameObject nearestEnemy = null;
			targetThing = null;


			foreach (GameObject enemy in enemies)
			{
				if (ownPos.y > enemy.transform.position.y)
				{
					float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
					if (distanceToEnemy < shortestDistance)
					{
						shortestDistance = distanceToEnemy;
						nearestEnemy = enemy;
					}
				}
			}
			if (nearestEnemy != null && shortestDistance <= attackRange)
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
		else
		{
			return;
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 1.5f);
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
