using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : Singleton<TowerController>
{
    public System.Action<float, float> OnMoveTower;

    private float _plummetSpeed = -4f;
    private float _rotationSpeed = 0f;

    private AudioClip _music;

    public void Init(AudioClip music)
    {
        _music = music;
    }

    public void Begin()
    {
        MusicController.Instance.Play(_music);
    }

    private void Start()
    {
        MusicController.Instance.OnSoundBurst += OnPlummet;
        MusicController.Instance.OnRegularSound += OnSink;
    }

    private void OnPlummet()
    {
        if (OnMoveTower != null) OnMoveTower(_plummetSpeed, _rotationSpeed);
    }

    private void OnSink()
    {
        if (OnMoveTower != null) OnMoveTower(_plummetSpeed / 3, _rotationSpeed);
    }

    private void OnDestroy()
    {
        if (MusicController.HasInstance())
        {
            MusicController.Instance.OnSoundBurst -= OnPlummet;
            MusicController.Instance.OnRegularSound -= OnSink;
        }
    }

}