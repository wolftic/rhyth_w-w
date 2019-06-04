using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void StartSequence() 
    {
        _rotationSpeed = Random.Range(-1, 1);
        _speed = 0f;

        _shouldFly = true;
    }
}
