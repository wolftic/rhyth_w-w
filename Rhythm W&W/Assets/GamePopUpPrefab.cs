using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePopUpPrefab : MonoBehaviour {

    public Action<string> ButtonPressed;

    private Text _levelName;
    private Text _scoreAmount;
    
	void Awake () {
        _levelName = transform.Find("bg/lvl").GetComponent<Text>();
        _scoreAmount = transform.Find("bg/scoreamount").GetComponent<Text>();
	}
	
    public void Set(string levelName, int playerScore, int maxScore)
    {
        _levelName.text = levelName;
        _scoreAmount.text = playerScore + "/" + maxScore;
    }

    public void OnButtonPressed(string buttonName) // de twee button namen zijn "retry" "continue"
    {
        ButtonPressed(buttonName);
    }
}
