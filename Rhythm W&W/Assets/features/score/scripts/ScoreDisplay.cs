using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;

    private void Start()
    {
        ScoreController.Instance.OnScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int scoreCount)
    {
        scoreText.text = "Score: " + scoreCount.ToString();
    }
    
    private void OnDestroy() 
    {
        if (ScoreController.HasInstance())
        {
            ScoreController.Instance.OnScoreChanged -= OnScoreChanged;
        }
    }
}
