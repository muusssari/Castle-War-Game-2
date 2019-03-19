using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour {

	Rigidbody rb;
	Collider col;
	private bool collid = false;
	private bool hit = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		col = GetComponent<BoxCollider>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		
		if (collision.collider.transform.root.gameObject.tag == "World")
		{
			collid = true;
			rb.isKinematic = true;
			rb.useGravity = false;
			rb.freezeRotation = true;
			col.enabled = false;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			rb.Sleep();
			
			hit = true;
			Destroy(gameObject, 4f);
		}
		if (hit == false)
		{
			if (collision.collider.transform.root.gameObject.tag == "Enemy")
			{
				collid = true;
				col.enabled = false;
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				rb.Sleep();
				
				rb.isKinematic = true;
				hit = true;
				Destroy(gameObject);
			}
			if (collision.collider.transform.root.gameObject.name == "MainmenuObj")
			{
				collid = true;
				col.enabled = false;
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				rb.Sleep();
				
				rb.isKinematic = true;
				hit = true;
				Destroy(gameObject);
			}
		}
	}

	private void LateUpdate()
	{
		if (!collid)
		{
			transform.rotation = Quaternion.LookRotation(rb.velocity);
		}
	}
}
