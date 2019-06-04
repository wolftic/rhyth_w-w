using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : Singleton<GameController> {
    public event System.Action<int> OnPlayerDie;
    public event System.Action OnPlayerWin;
    public event System.Action<Vector3> OnResetPlayer;

    [SerializeField]
    private GamePopUpPrefab _gamePopup;
    [SerializeField]
    private LevelSelectTransitionHandler _levelSelectPopup;
    
    [SerializeField]
    private Text _swipeToStart;
    
    [SerializeField]
    private Level[] _levels;
    [SerializeField]
    private int _startLevel;
    private int _currentLevel;

    private void Awake() 
    {
        GestureController.Instance.OnSwipe += OnSwipe;
    }

    private void Start()
    {
        ResetAll();
        OpenLevelSelect();
    }
    
    private void OnSwipe(SwipeType type)
    {
        switch(type)
        {
            case SwipeType.UP:
                TowerController.Instance.Begin();

                _swipeToStart.gameObject.SetActive(false);
                
                GestureController.Instance.OnSwipe -= OnSwipe;
            break;
        }
    }

    public void KillPlayer(int uuid)
    {
        if (OnPlayerDie != null) OnPlayerDie(uuid);
        OpenEndResult(false);
    }

    public void TriggerWin()
    {
        if (OnPlayerWin != null) OnPlayerWin();
        OpenEndResult(true);
    }

    public void PlayLevel(int level)
    {
        if (_levels != null && _levels.Length <= level) return;

        _currentLevel = level;

        ResetAll(level);

        _levels[_currentLevel].gameObject.SetActive(true);
        _swipeToStart.gameObject.SetActive(true);
        
        GestureController.Instance.OnSwipe += OnSwipe;
    }

    public void Restart()
    {
        _gamePopup.gameObject.SetActive(false);
        PlayLevel(_currentLevel);
        Debug.Log("restart");
    }

    public void OpenEndResult(bool playerWon)
    {
        _levelSelectPopup.gameObject.SetActive(false);

        Level level = _levels[_currentLevel];
        int score = 0; // ScoreController.Instance.Score;
        _gamePopup.Set(level.Name, score, level.PointsToUnlock, playerWon);
        _gamePopup.gameObject.SetActive(true);
    }

    public void OpenLevelSelect()
    {
        _gamePopup.gameObject.SetActive(false);

        _levelSelectPopup.Init(0, _levels);
        _levelSelectPopup.LevelSelected += PlayLevel;
        _levelSelectPopup.gameObject.SetActive(true);
    }
    
    private void ResetAll(int level = 0)
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].gameObject.SetActive(false);
            _levels[i].GetComponent<Tower>().Reset();
        }

        _levelSelectPopup.LevelSelected -= PlayLevel;

        _swipeToStart.gameObject.SetActive(false);
        _levelSelectPopup.gameObject.SetActive(false);
        _gamePopup.gameObject.SetActive(false);

        CollectableController.Instance.ResetCollectables();
        // ScoreController.Instance.ResetScore();

        if (OnResetPlayer != null) OnResetPlayer(_levels[level].SpawnPoint);
    }

    private void OnDestroy() {
        if (GestureController.HasInstance())
        {
            GestureController.Instance.OnSwipe -= OnSwipe;
        }
    }

}
