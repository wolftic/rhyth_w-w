using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all the music in the game
/// </summary>
/// <typeparam name="MusicController"></typeparam>
[RequireComponent(typeof(AudioSource))]
public class MusicController : Singleton<MusicController> {
    public System.Action OnSoundBurst;
    public System.Action OnRegularSound;

    private AudioSource _audioSource;
    private bool _isPlaying = false;
    private float _clipLoudnessTreshold = .20f;

    private float _clipLoudness;
    private float[] _clipSampleData;
    private int _sampleDataLength = 1024;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;

        GameController.Instance.OnPlayerWin += OnPlayerWin;
        GameController.Instance.OnPlayerDie += OnPlayerDie;
    }

    /// <summary>
    /// Play an audioclip
    /// </summary>
    /// <param name="clip"></param>
    public void Play(AudioClip clip) 
    {
        _clipSampleData = new float[1024];
        
        _audioSource.clip = clip;
        _audioSource.Play();

        _isPlaying = true;
    }

    /// <summary>
    /// Stops the music
    /// </summary>
    public void Stop() 
    {
        _audioSource.Stop();
        _isPlaying = false;
    }

    /// <summary>
    /// Triggered when the player wins
    /// </summary>
    private void OnPlayerWin() { Stop(); }
    /// <summary>
    /// Triggered when the player dies
    /// </summary>
    /// <param name="uuid"></param>
    private void OnPlayerDie(int uuid) { Stop(); }

    private void Update() 
    {
        if (!_isPlaying) return;
        
        _audioSource.clip.GetData(_clipSampleData, _audioSource.timeSamples);
        _clipLoudness = 0f;

        foreach (float sample in _clipSampleData)
        {
            _clipLoudness += Mathf.Abs(sample);
        }
        
        _clipLoudness /= _sampleDataLength; 

        if (_clipLoudness >= _clipLoudnessTreshold)
        {
            if (OnSoundBurst != null) OnSoundBurst();
        } else
        {
            if (OnRegularSound != null) OnRegularSound();
        }
    }

    private void OnDestroy()
    {
        if (GameController.HasInstance())
        {
            GameController.Instance.OnPlayerWin += OnPlayerWin;
            GameController.Instance.OnPlayerDie += OnPlayerDie;
        }
    }
}
