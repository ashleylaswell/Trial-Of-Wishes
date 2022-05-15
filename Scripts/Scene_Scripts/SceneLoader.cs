using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void OnEnable()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (sceneName == "OpeningScene")
        {
            SceneManager.LoadScene("LevelOne");
        }
        else if (sceneName == "LevelTwoOpening")
        {
            SceneManager.LoadScene("LevelTwo");
        }
        else if (sceneName == "EndingScene")
        {
            SceneManager.LoadScene("HUD");
        }
    }
}
