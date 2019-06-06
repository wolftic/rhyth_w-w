using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreController : Singleton<ScoreController>
{
    public int ScoreCount = 0;

    public event System.Action<int> OnScoreChanged;


    private void Start()
    {
        CollectableController.Instance.OnPickup += OnPickup;
    }

    private void OnPickup(int score)
    {
        ScoreCount += score;
        if (OnScoreChanged != null) OnScoreChanged(ScoreCount);
    }

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
