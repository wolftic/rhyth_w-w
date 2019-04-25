using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquare : MonoBehaviour {
    public AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;
    public GameObject level;
    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;
    void Awake()
    {

        if (!audioSource)
        {
            Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
        }
        clipSampleData = new float[sampleDataLength];
        Renderer renderer = level.GetComponent<Renderer>();
        float radius = renderer.bounds.extents.magnitude;
        print(radius);
    }

    void Update()
    {

        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; 
            if (clipLoudness >= 0.22f)
            {
                level.transform.localEulerAngles += new Vector3(0, clipLoudness * 3);
                level.transform.position -= new Vector3(0, clipLoudness / 10);
            }
            else
            {
                level.transform.localEulerAngles += new Vector3(0, 0.4f);
            }
        }

    }
}
