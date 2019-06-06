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
        MusicController.Instance.Pause();
        Time.timeScale = 0;
        _pauseButton.SetActive(false);
        _pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        MusicController.Instance.UnPause();
        Time.timeScale = 1;
        _pauseScreen.SetActive(false);
        _pauseButton.SetActive(true);
    }

    public void QuitGame()
    {
        MusicController.Instance.Stop();
        Time.timeScale = 1;
        _pauseScreen.SetActive(false);
        _pauseButton.SetActive(true);
    }

    private void Update()
    {
        if (MusicController.Instance.isPlaying)
        {
            _pauseButton.SetActive(true);
        }
        else
        {
            _pauseButton.SetActive(false);
        }
    }

}
