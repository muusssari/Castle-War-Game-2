using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public Transform target;
	public float speed = 5f;
	public float damage = 0.5f;
	public GameObject targetThing;


	public void Seek(Transform getTarget, GameObject getTargetThing)
	{
		targetThing = getTargetThing;
		target = getTarget;
	}

	private void Update()
	{
		if (targetThing == null || target == null)
		{
			Destroy(gameObject);
			return;
		}
	

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target.position);
		
	}

	void HitTarget()
	{
		Damage(target);
		Destroy(gameObject);
	}

	void Damage (Transform enemy)
	{
		enemy.GetComponent<Unit_Healt>().GetShot(damage);
	}

}
