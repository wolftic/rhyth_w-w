using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquare : Singleton<MoveSquare> {
    public AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;
    private GameObject level;
    private float currentUpdateTime = 0f;
    public Action OnPlummet;
    private float clipLoudness;
    private float[] clipSampleData;
    void Awake()
    {
        level = this.gameObject;
        if (!audioSource)
        {
            Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
        }
        clipSampleData = new float[sampleDataLength];
        Renderer renderer = level.GetComponent<Renderer>();
        float radius = renderer.bounds.extents.magnitude;
    }

    void Update()
    {

            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; 
            if (clipLoudness >= 0.22f)
            {
                level.transform.localEulerAngles += new Vector3(0, 0.75f);
                level.transform.position -= new Vector3(0, 0.1f);
                OnPlummet();
            }
            else
            {
            level.transform.position -= new Vector3(0, 0.005f);
            level.transform.localEulerAngles += new Vector3(0, 0.3f);
            }

    }
}
