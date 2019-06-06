using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles everything player score related
/// </summary>
/// <typeparam name="ScoreController">Singleton</typeparam>
public class ScoreController : Singleton<ScoreController>
{
    public int ScoreCount = 0;

    public event System.Action<int> OnScoreChanged;

    private void Start()
    {
        CollectableController.Instance.OnPickup += OnPickup;
    }

    /// <summary>
    /// Triggered when a collectable is picked up
    /// </summary>
    /// <param name="score">Points gathered from the collectable</param>
    private void OnPickup(int score)
    {
        ScoreCount += score;
        if (OnScoreChanged != null) OnScoreChanged(ScoreCount);
    }

    /// <summary>
    /// Reset score to 0
    /// </summary>
    public void ResetScore()
    {
        ScoreCount = 0;
    }

    private void OnDestroy()
    {
        if (CollectableController.HasInstance())
        {
            CollectableController.Instance.OnPickup -= OnPickup;
        }
    }
}
