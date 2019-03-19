using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour {

	public string animName;

	public void PlayAnimation()
	{
		gameObject.GetComponent<Animation>().Play(animName);
	}
}
