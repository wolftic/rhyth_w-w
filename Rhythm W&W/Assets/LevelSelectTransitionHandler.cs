using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelSelectTransitionHandler : MonoBehaviour {
    private float _standardSize;
    private float _startPosition;

    private GameObject _lockIcon;
    private GameObject _levelWindow;

    public int CollectibleAmount;

    public Action<int> LevelSelected;


    private Text _scoreText;
    private Text _levelName;
    private Image _maskedImage;
    private Image _sprite;
    private float _spriteWidth;

    private int _levelIndex;

    public Sprite[] images;
    public int[] UnlockThreshHold;

    public bool IsAnimating;

    [SerializeField] private float _swivelSpeed;
    [SerializeField] private float _smallestSize;



	void Start () {
        _levelWindow = transform.Find("levelprefab").gameObject;
        _scoreText = transform.Find("collectibles/scoretext").GetComponent<Text>();
        _sprite = _levelWindow.GetComponent<Image>();
        _levelName = _levelWindow.transform.Find("levelname").GetComponent<Text>();
        _maskedImage = _levelWindow.transform.Find("mask/levelimage").GetComponent<Image>();
        _lockIcon = _levelWindow.transform.Find("mask/locked").gameObject;
        _spriteWidth = _sprite.preferredWidth;
        _startPosition = _levelWindow.transform.position.x;
        _standardSize = _levelWindow.transform.localScale.x;

        SetScoreText(CollectibleAmount, UnlockThreshHold[UnlockThreshHold.Length - 1]);

        UpdateLevelData();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && IsAnimating == false)
        {
            OnBackPress();
        }
	}

    public void SetScoreText(int amount, int max)
    {
        _scoreText.text = amount + "/" + max;
    }

    private void SeeIfUnlockable()
    {
        if(CollectibleAmount >= UnlockThreshHold[_levelIndex])
        {
            _lockIcon.SetActive(false);
        } else
        {
            _lockIcon.SetActive(true);
        }
    }

    public void OnLevelSelected()
    {
        LevelSelected(_levelIndex);
    }

    public void OnBackPress()
    {
        if (IsAnimating) return;
        if (_levelIndex == 0) _levelIndex = images.Length - 1; else _levelIndex--;
        StartCoroutine(TransitionLeft(true));
    }

    public void UpdateLevelData()
    {
        _maskedImage.sprite = images[_levelIndex];
        _levelName.text = "" + images[_levelIndex].name;
        SeeIfUnlockable();
    }

    public void OnForwardPress()
    {
        if (IsAnimating) return;
        if (_levelIndex >= images.Length - 1) _levelIndex = 0; else _levelIndex++;
        StartCoroutine(TransitionRight(true));
    }

    private IEnumerator TransitionLeft(bool start)
    {

        if (start) IsAnimating = true; else UpdateLevelData();
        float timer = 0;
        while (timer <= 1)

        {
            timer += Time.deltaTime * _swivelSpeed;
            float newSize;
            float newPosition;
            if (start)
            {
                newSize = Mathf.Lerp(_standardSize, _smallestSize, timer);
                newPosition = Mathf.Lerp(_startPosition, Screen.width + _spriteWidth / 4, timer);
            }
            else
            {
                newSize = Mathf.Lerp(_smallestSize, _standardSize, timer);
                newPosition = Mathf.Lerp(Screen.width + _spriteWidth / 4, _startPosition, timer);
            }
            _levelWindow.transform.position = new Vector3(newPosition, _levelWindow.transform.position.y, transform.position.z);
            _levelWindow.transform.localScale = new Vector3(newSize, newSize, newSize);
            yield return null;
        }


        if (start) StartCoroutine(TransitionRight(false)); else IsAnimating = false;
    } 

    private IEnumerator TransitionRight(bool start)
    {
        if (start) IsAnimating = true; else UpdateLevelData();

        float timer = 0;

        while (timer <= 1)
        {

            timer += Time.deltaTime * _swivelSpeed;
            float newSize;
            float newPosition;
            if (start)
            {
                newSize = Mathf.Lerp(_standardSize, _smallestSize, timer);
                newPosition = Mathf.Lerp(_startPosition, 0 - _spriteWidth / 4, timer);
            }
            else
            {
                newSize = Mathf.Lerp(_smallestSize, _standardSize, timer);
                newPosition = Mathf.Lerp(0 - _spriteWidth / 4, _startPosition, timer);
            }
            _levelWindow.transform.position = new Vector3(newPosition, _levelWindow.transform.position.y, transform.position.z);
            _levelWindow.transform.localScale = new Vector3(newSize, newSize, newSize);
            yield return null;

        }
        if (start) StartCoroutine(TransitionLeft(false)); else IsAnimating = false;
    }
}
