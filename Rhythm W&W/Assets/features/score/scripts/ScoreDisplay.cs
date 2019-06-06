using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the score on the UI
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;

    private void Start()
    {
        ScoreController.Instance.OnScoreChanged += OnScoreChanged;
        GameController.Instance.OnGameStateChange += OnGameStateChange;
    }

    /// <summary>
    /// Triggered when the game state changes
    /// </summary>
    /// <param name="obj"></param>
    private void OnGameStateChange(GameState obj)
    {
        switch(obj) 
        {
            case GameState.IN_GAME:
            gameObject.SetActive(true);
            break;
            default:
            gameObject.SetActive(false);
            break;
        }
    }

    /// <summary>
    /// Triggered when the score changes
    /// </summary>
    /// <param name="scoreCount"></param>
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

        if (GameController.HasInstance())
        {
            GameController.Instance.OnGameStateChange -= OnGameStateChange;
        }
    }
}
