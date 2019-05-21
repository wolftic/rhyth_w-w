﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : Singleton<MusicController> {
    public System.Action OnSoundBurst;

    private AudioSource _audioSource;
    private bool _isPlaying = false;
    private float _clipLoudnessTreshold = .18f;

    private float _clipLoudness;
    private float[] _clipSampleData;
    private int _sampleDataLength = 1024;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
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
        _isPlaying = false;
    }

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
}