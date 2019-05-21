using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreController : Singleton<ScoreController>
{
    public int scoreCount = 0;

    public event System.Action<int> OnScoreChanged;


    private void Start()
    {
        CollectableController.Instance.OnPickup += OnPickup;
    }

    private void OnPickup(int score)
    {
        scoreCount += score;
        if (OnScoreChanged != null) OnScoreChanged(scoreCount);
    }

    private void Update()
    {

    }

}
