using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : Singleton<GameController> {
    public event System.Action<int> OnPlayerDie;
    public event System.Action OnPlayerWin;
    
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

    public void KillPlayer(int uuid)
    {
        if (OnPlayerDie != null) OnPlayerDie(uuid);
        Invoke("Restart", 15f);
    }

    public void TriggerWin()
    {
        if (OnPlayerWin != null) OnPlayerWin();
        Invoke("Restart", 15f);
    }

    public void Restart()
    {
        Debug.Log("restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy() {
        if (GestureController.HasInstance())
        {
            GestureController.Instance.OnSwipe -= OnSwipe;
        }
    }

}
