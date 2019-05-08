using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPlayer : PhysicsBody {
    [SerializeField]
    private Vector3 _velocity;
    [SerializeField]
    private float _jumpHeight;

    [SerializeField]
    private float _timeForRound = 5f;
    private float _moveSpeed;

    private float _gravity = -50f;
    
    private Animator _animation;
    private SpriteRenderer _renderer;
    
    private int _direction = 1;
    private float _jumpAcceleration;


    private void Awake() {
        base.Awake();
        _animation = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _moveSpeed = (2 * Mathf.PI) / _timeForRound * 5;
        GestureController.Instance.OnSwipe += OnSwipe;
    }

    private void OnSwipe(SwipeType type)
    {
        switch(type)
        {
            case SwipeType.UP:
                float final = 0f;
                float squaredAcceleration = final - 2 * _gravity * _jumpHeight;
                _jumpAcceleration = Mathf.Sqrt(squaredAcceleration);
                _animation.SetTrigger("jump");
            break;
            case SwipeType.LEFT:
                _direction = -1;
            break;
            case SwipeType.RIGHT:
                _direction = 1;
            break;
        }
    }

    private void Update() 
    {
        if (/*Collisions.top ||  */Collisions.bottom) {
			_velocity.y = 0;
		} else {
            _jumpAcceleration = 0;
        }

        _velocity.x = _direction * _moveSpeed;

        if (_jumpAcceleration > 0) {
            _velocity.y = _jumpAcceleration;
            _jumpAcceleration = 0;
        }
        
        _velocity.y += _gravity * Time.deltaTime;

        HandleAnimations();
        this.Move(_velocity * Time.deltaTime);
    }

    public override void Move(Vector3 velocity)
    {
        base.Move(velocity);

        Vector3 position = transform.position;
        Vector3 xz = position;
                xz.y = 0;
        
        transform.rotation = Quaternion.LookRotation(-xz, Vector3.up);
        transform.position = xz.normalized * 2.55f + Vector3.up * position.y;
    }

    private void HandleAnimations() {        
        _animation.SetBool("running", Mathf.Abs(_velocity.x) > 0);
        _renderer.flipX = _direction < 0;
    }

    private void OnDestroy() {
        if (GestureController.HasInstance()) 
        {
            GestureController.Instance.OnSwipe -= OnSwipe;
        }
    }
}
