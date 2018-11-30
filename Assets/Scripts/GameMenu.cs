using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public string levelToLoad = "";

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void Quit()
    {
        Debug.Log("Quit Game...");
        Application.Quit();
    }

}
