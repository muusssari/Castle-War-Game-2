﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider slider;
	public int scene;

	private void Start()
	{
		LoadLevel(scene);
	}
	public void LoadLevel(int sceneIndex)
	{
		StartCoroutine(LoadAsync(sceneIndex));
	}

	IEnumerator LoadAsync(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		//loadingScreen.SetActive(true);
		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			slider.value = progress;
			yield return null;
		}
		
	}
}
