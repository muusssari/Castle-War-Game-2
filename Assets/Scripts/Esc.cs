using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Esc : MonoBehaviour
{

	public string levelToLoad = "";
	public GameObject menu;
	public static bool GameIsPaused = false;
	public GameObject shops;
	public GameObject minimap;
	private bool map = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			if (!map)
			{
				minimap.GetComponent<Animation>().Play("minimapSize");
				map = true;
			}
			else
			{
				minimap.GetComponent<Animation>().Play("minimapSmall");
				map = false;
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
				shops.transform.GetChild(0).gameObject.SetActive(false);
				shops.transform.GetChild(1).gameObject.SetActive(false);
				shops.transform.GetChild(2).gameObject.SetActive(false);
			}
		}
	}

	void Pause()
	{
		menu.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void Resume()
	{
		menu.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
	public void ExitGame()
	{
		SceneManager.LoadScene(levelToLoad);
		Time.timeScale = 1f;
	}

}
