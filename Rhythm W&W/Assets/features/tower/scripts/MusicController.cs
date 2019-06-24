using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : Singleton<MusicController>
{
    public System.Action OnSoundBurst;

    public AudioSource _audioSource;
    private float _clipLoudnessTreshold = .26f;
    private bool _isPlaying = false;
    public bool isPlaying
    {
        get
        {
            return _isPlaying;
        }
    }

    private float _clipLoudness;
    private float[] _clipSampleData;
    private int _sampleDataLength = 1024;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;

        GameController.Instance.OnPlayerWin += OnPlayerWin;
        GameController.Instance.OnPlayerDie += OnPlayerDie;
    }

    public void Play(AudioClip clip)
    {
        _clipSampleData = new float[1024];

        _audioSource.clip = clip;
        _audioSource.Play();

        _isPlaying = true;
    }

    public void Stop()
    {
        _audioSource.Stop();
        _isPlaying = false;
    }

    public void Pause()
    {
        _audioSource.Pause();
        _isPlaying = false;
    }

    public void UnPause()
    {
        _audioSource.UnPause();
        _isPlaying = true;
    }

    private void OnPlayerWin() { Stop(); }
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
