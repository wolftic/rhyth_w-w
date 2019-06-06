using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In combination with the Rocket script this will make the rocket actually move in the sky.
/// </summary>
public class RocketFly : MonoBehaviour {
    private bool _shouldFly = false;

    private float _speed = 0f;
    private float _rotationSpeed = 0f;
	
    private void Update() 
    {
        if (!_shouldFly) return;

        _speed += Time.deltaTime;

        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        transform.Rotate(transform.forward * _rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Starts the flying sequence
    /// </summary>
    public void StartSequence() 
    {
        _rotationSpeed = Random.Range(-1, 1);
        _speed = 0f;

        _shouldFly = true;
    }
}
