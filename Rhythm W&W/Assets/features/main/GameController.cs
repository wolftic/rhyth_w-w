using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController> {
    [SerializeField]
    private Text _swipeToStart;
    
    private void Awake() 
    {
        GestureController.Instance.OnSwipe += OnSwipe;
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

    private void OnDestroy() {
        if (GestureController.HasInstance())
        {
            GestureController.Instance.OnSwipe -= OnSwipe;
        }
    }

}
