using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RocketFly))]
public class Rocket : MonoBehaviour {
    private SpriteRenderer _renderer;
    private RocketFly _fly;
    
    [SerializeField]
    private Sprite _defaultState, _winState;

    [SerializeField]
    private List<GameObject> _activeOnWin;

    private void Awake() 
    {
        _fly = GetComponent<RocketFly>();
        _renderer = GetComponent<SpriteRenderer>();

        GameController.Instance.OnPlayerWin += OnPlayerWin;
    }

    private void Start() 
    {
        SetWin(false);
    }

    [ContextMenu("win")]
    private void OnPlayerWin() 
    {
        SetWin(true);
        _fly.StartSequence();
    }

    private void SetWin(bool win) 
    {
        _renderer.sprite = win ? _winState : _defaultState;

        for (int i = 0; i < _activeOnWin.Count; i++)
        {
            _activeOnWin[i].SetActive(win);
        }
    }

    private void OnDestroy()
    {
        if (GameController.HasInstance()) 
        {
            GameController.Instance.OnPlayerWin -= OnPlayerWin;   
        }
    }
}
