using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Esc : MonoBehaviour
{

	public string levelToLoad = "";
	public GameObject menu;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (menu.active == true)
			{
				menu.SetActive(false);
			}
			else
			{
				menu.SetActive(true);
			}
		}
	}

	public void MenuReturn()
	{
		menu.SetActive(false);
	}
	public void ExitGame()
	{
		SceneManager.LoadScene(levelToLoad);
	}

}
