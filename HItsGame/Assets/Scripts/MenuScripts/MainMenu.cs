using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameLevel;
    private string levelToLoad;

    public void NewGame()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
