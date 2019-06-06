using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles everything happening to the tower
/// </summary>
/// <typeparam name="TowerController"></typeparam>
public class TowerController : Singleton<TowerController>
{
    public System.Action<float, float> OnMoveTower;

    private float _plummetSpeed = -4f;
    private float _rotationSpeed = 0f;

    private AudioClip _music;

    /// <summary>
    /// Initialize the tower with music
    /// </summary>
    /// <param name="music"></param>
    public void Init(AudioClip music)
    {
        _music = music;
    }

    /// <summary>
    /// Start tower falling sequence
    /// </summary>
    public void Begin()
    {
        MusicController.Instance.Play(_music);
    }

    private void Start()
    {
        MusicController.Instance.OnSoundBurst += OnPlummet;
        MusicController.Instance.OnRegularSound += OnSink;
    }

    /// <summary>
    /// Triggered when the tower should plummet
    /// </summary>
    private void OnPlummet()
    {
        if (OnMoveTower != null) OnMoveTower(_plummetSpeed, _rotationSpeed);
    }

    /// <summary>
    /// Triggered when the tower should rotate
    /// </summary>
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