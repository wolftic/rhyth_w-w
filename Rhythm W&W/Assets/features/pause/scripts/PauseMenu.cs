using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseButton;
    [SerializeField]
    private GameObject _playButton;
    [SerializeField]
    private GameObject _retryButton;
    [SerializeField]
    private GameObject _returnToMenuButton;


    public void Pause()
    {
        Time.timeScale = 0;
        _pauseButton.SetActive(false);

        _playButton.SetActive(true);
        _retryButton.SetActive(true);
        _returnToMenuButton.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;

        _playButton.SetActive(false);
        _retryButton.SetActive(false);
        _returnToMenuButton.SetActive(false);

        _pauseButton.SetActive(true);
    }

}
