using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

	public GameObject item;
	Animation anim;

	void Start ()
	{
		anim = item.GetComponent<Animation>();
	}

	public void MeleeAttackAnimation()
	{
		anim.Play("SwordSwing");
	}
	public void ShootAnimation()
	{
		anim.Play("BowShooting");
	}
	public void ShootAnimationStop()
	{
		anim.Stop("BowShooting");
	}
}
