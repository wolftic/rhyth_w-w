using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles everthing from the ending screen
/// </summary>
public class GamePopUpPrefab : MonoBehaviour {
    [SerializeField]
    private GameObject _winText, _loseText;

    [SerializeField]
    private Text _levelName, _scoreAmount;
    
    /// <summary>
    /// Populate popup with information
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="playerScore"></param>
    /// <param name="maxScore"></param>
    /// <param name="won"></param>
    public void Set(string levelName, int playerScore, int maxScore, bool won)
    {
        _levelName.text = levelName;
        _scoreAmount.text = playerScore + "/" + maxScore;
        _winText.SetActive(won);
        _loseText.SetActive(!won);
    }

    /// <summary>
    /// Action triggered by the retry button
    /// </summary>
    public void Retry()
    {
        GameController.Instance.Restart();
    }

    /// <summary>
    /// Action triggered by the continue button
    /// </summary>
    public void Continue()
    {
        GameController.Instance.OpenLevelSelect();
        // GameController.Instance.
    }
}
