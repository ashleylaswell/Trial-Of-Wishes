using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// Handle hitpoints and damages
public class PlayerHealthScript : MonoBehaviour
{
    public GameObject[] hearts;

    public int hitpoints = 3;
    public bool isEnemy = true;

    private bool canDamage;

    void Awake()
    {
        canDamage = true;
    }

    void Update()
    {
        string isEnemyStr = isEnemy.ToString();

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (hitpoints < 1)
        {
            Destroy(hearts[0].gameObject);
            Destroy(gameObject);
            if ((isEnemyStr == "False") && (sceneName == "LevelOne")) 
            {
                SceneManager.LoadScene("LevelOne");
            }
            else if ((isEnemyStr == "False") && (sceneName == "LevelTwo")) 
            {
                SceneManager.LoadScene("LevelTwo");
            }
        }
        else if (hitpoints < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (hitpoints < 3)
        {
            Destroy(hearts[2].gameObject); 
        }
    }

    /// Inflicts damage and check if the object should be destroyed
    public void Damage()
    {
        if (canDamage)
        {
            hitpoints--;
            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }
}