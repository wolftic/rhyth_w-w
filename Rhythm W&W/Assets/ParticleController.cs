using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {
    private ParticleSystem _particleSystem;
	void Start () {
        _particleSystem = GetComponent<ParticleSystem>();
        MoveSquare.Instance.OnPlummet += BurstParticles;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BurstParticles();
        }
	}
    public void BurstParticles()
    {
        _particleSystem.Emit( 10 );
    }
}