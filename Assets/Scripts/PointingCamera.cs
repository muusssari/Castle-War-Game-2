using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingCamera : MonoBehaviour {

	public GameObject Camera;
	public Transform target;
	void Start () {
		Camera = GameObject.Find("Main Camera");
		target = Camera.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
	}
}
