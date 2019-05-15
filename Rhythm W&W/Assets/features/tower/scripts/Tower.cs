using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField]
    private float _plummetSpeed = 0.1f;
    [SerializeField]
    private float _rotationSpeed = 10f;

    [SerializeField]
    private AudioClip _music;

    private void Awake() 
    {
        MusicController.Instance.OnSoundBurst += OnPlummet;    
    }

    private void Start()
    {
        MusicController.Instance.Play(_music);
    }

    private void OnPlummet() 
    {
        Debug.Log("plummet");
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        transform.Translate(0, _plummetSpeed * Time.deltaTime, 0);
    }

    private void OnDestroy() {
        if (MusicController.HasInstance())
        {
            MusicController.Instance.OnSoundBurst -= OnPlummet;
        }
    }
}
