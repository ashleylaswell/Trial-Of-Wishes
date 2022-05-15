using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    void OnCollisionEnter2D (Collision2D other) {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if((other.gameObject.tag == MyTags.PLAYER_TAG) && (sceneName == "LevelOne")){
            SceneManager.LoadScene("LevelTwoOpening");
        }
        else if ((other.gameObject.tag == MyTags.PLAYER_TAG) && (sceneName == "LevelTwo")){
            SceneManager.LoadScene("EndingScene");
        }
    }
}
