using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquare : Singleton<MoveSquare> {
    [SerializeField]
    private int _sampleDataLength = 1024;

    public Action OnPlummet;
    
    private AudioSource _audioSource;
    private GameObject _level;
    private float _clipLoudness;
    private float[] _clipSampleData;

    private void Awake()
    {
        _level = this.gameObject;
        _audioSource = GetComponent<AudioSource>();
        _clipSampleData = new float[_sampleDataLength];
    }

    private void Update()
    {
        _audioSource.clip.GetData(_clipSampleData, _audioSource.timeSamples);
        _clipLoudness = 0f;

        foreach (float sample in _clipSampleData)
        {
            _clipLoudness += Mathf.Abs(sample);
        }
        
        _clipLoudness /= _sampleDataLength; 
        
        if (_clipLoudness >= 0.22f)
        {
            _level.transform.localEulerAngles += new Vector3(0, 0.75f);
            _level.transform.position -= new Vector3(0, 0.1f);
            
            if (OnPlummet != null) OnPlummet();
        }
        else
        {
            _level.transform.position -= new Vector3(0, 0.005f);
            _level.transform.localEulerAngles += new Vector3(0, 0.3f);
        }
    }
}
