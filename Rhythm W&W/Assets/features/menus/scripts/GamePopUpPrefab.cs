using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePopUpPrefab : MonoBehaviour {
    [SerializeField]
    private GameObject _winText, _loseText;

    [SerializeField]
    private Text _levelName, _scoreAmount;
    
    public void Set(string levelName, int playerScore, int maxScore, bool won)
    {
        _levelName.text = levelName;
        _scoreAmount.text = playerScore + "/" + maxScore;
        _winText.SetActive(won);
        _loseText.SetActive(!won);
    }

    public void Retry()
    {
        GameController.Instance.Restart();
    }

    public void Continue()
    {
        GameController.Instance.OpenLevelSelect();
        // GameController.Instance.
    }
}
