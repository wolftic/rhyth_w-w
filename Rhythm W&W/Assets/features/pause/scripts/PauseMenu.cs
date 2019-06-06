using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseButton;
    [SerializeField]
    private GameObject _pauseScreen;

    public void Pause()
    {
        Time.timeScale = 0;
        _pauseButton.SetActive(false);
        _pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pauseScreen.SetActive(false);
        _pauseButton.SetActive(true);
    }

}
