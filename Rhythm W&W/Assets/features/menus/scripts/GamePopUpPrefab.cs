using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePopUpPrefab : MonoBehaviour {

    public Action<string> ButtonPressed;

    private GameObject _winText;
    private GameObject _loseText;

    private Text _levelName;
    private Text _scoreAmount;
    
	void Awake () {
        _levelName = transform.Find("bg/lvl").GetComponent<Text>();
        _scoreAmount = transform.Find("bg/scoreamount").GetComponent<Text>();
        _winText = transform.Find("gewonnen").gameObject;
        _loseText = transform.Find("verloren").gameObject;
	}
	
    public void Set(string levelName, int playerScore, int maxScore, bool won)
    {
        _levelName.text = levelName;
        _scoreAmount.text = playerScore + "/" + maxScore;
        if (won)
        {
            _winText.SetActive(true); _loseText.SetActive(false);
        }
        else
        {
            _winText.SetActive(false); _loseText.SetActive(true);
        } 
    }

    public void OnButtonPressed(string buttonName) // de twee button namen zijn "retry" "continue"
    {
        ButtonPressed(buttonName);
    }
}
