using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : Singleton<TowerController> {
    public System.Action<float, float> OnMoveTower;
    
    private float _plummetSpeed = -1f;
    private float _rotationSpeed = 0f;

    private void Start() 
    {
        MusicController.Instance.OnSoundBurst += OnPlummet;    
    }

    private void Update() 
    {
        OnPlummet();
    }

    private void OnPlummet() 
    {
        if (OnMoveTower != null) OnMoveTower(_plummetSpeed, _rotationSpeed);
    }

    private void OnDestroy() {
        if (MusicController.HasInstance())
        {
            MusicController.Instance.OnSoundBurst -= OnPlummet;
        }
    }
}
