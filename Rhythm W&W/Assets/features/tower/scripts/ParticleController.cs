using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all the particles
/// </summary>
public class ParticleController : MonoBehaviour {
    [SerializeField]
    private int _particleAmount = 5;
    
    private ParticleSystem _particleSystem;
	
    private void Awake() 
    {
        _particleSystem = GetComponent<ParticleSystem>();
        MusicController.Instance.OnSoundBurst += BurstParticles;
	}

    /// <summary>
    /// Activate the burst particles
    /// </summary>
    private void BurstParticles()
    {
        _particleSystem.Emit(_particleAmount);
    }

    #if UNITY_ENGINE
	private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BurstParticles();
        }
	}
    #endif

    private void OnDestroy() 
    {
        if (MusicController.HasInstance())
        {
            MusicController.Instance.OnSoundBurst -= BurstParticles;
        }
    }
}