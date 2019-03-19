using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {


	void Start ()
	{
		StartCoroutine("SplashLogo");
	}
	

	public IEnumerator SplashLogo()
	{
		yield return new WaitForSeconds(5);
		SceneManager.LoadSceneAsync(1);
	}
}
