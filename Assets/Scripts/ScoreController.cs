using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score = 0;
    public Text t_score;
    public void ChangeScore(int points)
    {
        score += points;
        t_score.text = "SCORE:" + score.ToString();
    }

    void Start()
    {
        ChangeScore(0);
    }
}
