using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        ScoreController.Instance.OnScoreChanged += OnScoreChanged;
    }

    void OnScoreChanged(int scoreCount)
    {
        scoreText.text = "Score: " + scoreCount.ToString();
    }
}
