using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    void Awake()
    {
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HUD");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
