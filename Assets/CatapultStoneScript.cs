using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultStoneScript : MonoBehaviour {

	public int damage = 10;

	private void Update()
	{

	}

	void Damage(Transform enemy)
	{
		enemy.GetComponent<Unit_Healt>().GetShot(damage);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "World")
		{
			Destroy(gameObject, 5f);
		}
		else if (collision.collider.tag == "Enemy")
		{
			Damage(collision.collider.transform.root);
			Destroy(gameObject);
		}
	}
}
