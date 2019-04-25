using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPlayer : PhysicsBody {
    [SerializeField]
    private Vector3 _velocity;
    [SerializeField]
    private float _jumpHeight;
    
    private float _gravity = -9.8f;

    private void Update() {
        if (Collisions.top || Collisions.bottom) {
			_velocity.y = 0;
         
            if (Input.GetKeyDown(KeyCode.Space)) {
                float final = 0f;
                float squaredAcceleration = final - 2 * _gravity * _jumpHeight;
                float acceleration = Mathf.Sqrt(squaredAcceleration);
                _velocity.y = acceleration;
            }
		}

        float x = Input.GetAxis("Horizontal") * 5f;
        _velocity.x = x;

        _velocity.y += _gravity * Time.deltaTime;

        this.Move(_velocity * Time.deltaTime);
    }
}
