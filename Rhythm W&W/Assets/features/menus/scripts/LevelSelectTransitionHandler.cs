using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelSelectTransitionHandler : MonoBehaviour {
    private float _standardSize, _startPosition, _spriteWidth;
    [SerializeField]
    private GameObject _lockIcon, _levelWindow;
    [SerializeField]
    private Text _scoreText, _levelName;
    [SerializeField]
    private Image _maskedImage, _sprite;

    public Action<int> LevelSelected;
    public Action ExitPressed;

    private int _levelIndex = 0;
    private int _collectibleAmount = 20;
    private Level[] _levels;

    [SerializeField]
    private bool _isAnimating;
    [SerializeField]
    private float _swivelSpeed;
    [SerializeField]
    private float _smallestSize;

	public void Init (int collected, Level[] levels) 
    {   
        Reset();

        _spriteWidth = _sprite.preferredWidth;
        _startPosition = _levelWindow.transform.position.x;
        _standardSize = _levelWindow.transform.localScale.x;

        _collectibleAmount = collected;
        _levels = levels;

        UpdateLevelData();
    }

    public void SetScoreText(int amount, int max)
    {
        _scoreText.text = String.Concat(amount, "/", max);
    }

    private void SeeIfUnlockable()
    {
        if (_collectibleAmount >= _levels[_levelIndex].PointsToUnlock)
        {
            _lockIcon.SetActive(false);
        } else
        {
            _lockIcon.SetActive(true);
        }
        SetScoreText(_collectibleAmount, _levels[_levelIndex].PointsToUnlock);
    }

    private void UpdateLevelData()
    {
        _maskedImage.sprite = _levels[_levelIndex].Image;
        _levelName.text = _levels[_levelIndex].Name;
        SeeIfUnlockable();
    }

    public void OnLevelSelected()
    {
        if (_collectibleAmount < _levels[_levelIndex].PointsToUnlock) return;
        if (LevelSelected != null) LevelSelected(_levelIndex);
    }
    
    public void OnExitMenu()
    {
        // ExitPressed();
    }

    public void OnBackPress()
    {
        if (_isAnimating) return;
        if (_levelIndex == 0) _levelIndex = _levels.Length - 1; else _levelIndex--;
        StartCoroutine(TransitionLeft(true));
    }

    public void OnForwardPress()
    {
        if (_isAnimating) return;
        if (_levelIndex >= _levels.Length - 1) _levelIndex = 0; else _levelIndex++;
        StartCoroutine(TransitionRight(true));
    }

    private IEnumerator TransitionLeft(bool start)
    {
        if (start) _isAnimating = true; 
        else UpdateLevelData();

        float newSize, newPosition;
        float timer = 0;
        
        while (timer <= 1)
        {
            timer += Time.deltaTime * _swivelSpeed;

            if (start)
            {
                newSize = Mathf.Lerp(_standardSize, _smallestSize, Ease(timer));
                newPosition = Mathf.Lerp(_startPosition, Screen.width + _spriteWidth / 4, Ease(timer));
            }
            else
            {
                newSize = Mathf.Lerp(_smallestSize, _standardSize, Ease(timer));
                newPosition = Mathf.Lerp(Screen.width + _spriteWidth / 4, _startPosition, Ease(timer));
            }
            
            _levelWindow.transform.position = new Vector3(newPosition, _levelWindow.transform.position.y, transform.position.z);
            _levelWindow.transform.localScale = new Vector3(newSize, newSize, newSize);
            
            yield return null;
        }

        if (start) StartCoroutine(TransitionRight(false));
        else _isAnimating = false;
    } 

    private IEnumerator TransitionRight(bool start)
    {
        if (start) _isAnimating = true; 
        else UpdateLevelData();

        float newSize, newPosition;
        float timer = 0;
            
        while (timer <= 1)
        {
            timer += Time.deltaTime * _swivelSpeed;
            
            if (start)
            {
                newSize = Mathf.Lerp(_standardSize, _smallestSize, Ease(timer));
                newPosition = Mathf.Lerp(_startPosition, 0 - _spriteWidth / 4, Ease(timer));
            }
            else
            {
                newSize = Mathf.Lerp(_smallestSize, _standardSize, Ease(timer));
                newPosition = Mathf.Lerp(0 - _spriteWidth / 4, _startPosition, Ease(timer));
            }
            
            _levelWindow.transform.position = new Vector3(newPosition, _levelWindow.transform.position.y, transform.position.z);
            _levelWindow.transform.localScale = new Vector3(newSize, newSize, newSize);
            
            yield return null;
        }

        if (start) StartCoroutine(TransitionLeft(false));
        else _isAnimating = false;
    }

    private float Ease(float value, float start = 0f, float end = 1f) 
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value + start;
        value -= 2;
        return end * 0.5f * (value * value * value + 2) + start;
    }

    private void Reset()
    {
        _collectibleAmount = 0;
        _levels = null;
        _maskedImage.sprite = null;
        _levelName.text = "";
        _scoreText.text = "";
        _lockIcon.SetActive(true);
    }
}