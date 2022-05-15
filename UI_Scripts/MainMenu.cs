using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void BeginPlayBtn()
    {
        SceneManager.LoadScene("OpeningScene");
    }

    public void LoadLevels()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void LoadLevelOne()
    {   
        SceneManager.LoadScene("LevelOne");
        
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("LevelTwo");
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("HUD");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
