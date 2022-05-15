using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private Text score;
    private int scoreCount = 0;

    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Text>();
    }

    void Update()
    {
        score.text = "Score: " + scoreCount;
    }

    public void AddToScore(int amount)
    {
        scoreCount += amount;
    }
}
