using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPlayer : PhysicsBody
{
    [SerializeField]
    private Vector3 _velocity;
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _deadJumpHeight;
    private bool canDoubleJump;

    [SerializeField]
    private float _timeForRound = 5f;
    private float _moveSpeed;

    private float _gravity = -50f;

    private Animator _animation;
    private SpriteRenderer _renderer;

    private int _direction = 1;
    private float _jumpAcceleration;

    private bool _isDead = false;

    private void Awake()
    {
        base.Awake();
        _animation = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _moveSpeed = (2 * Mathf.PI) / _timeForRound * 5;

        _isDead = true;

        GestureController.Instance.OnSwipe += OnSwipe;
        TowerController.Instance.OnMoveTower += OnMove;
        GameController.Instance.OnPlayerDie += OnPlayerDie;
        GameController.Instance.OnResetPlayer += OnResetPlayer;
    }

    private void OnMove(float plummetSpeed, float rotationSpeed)
    {
        transform.Translate(Vector3.up * plummetSpeed * Time.deltaTime);
    }

    private void OnPlayerDie(int uuid)
    {
        if (uuid != gameObject.GetInstanceID()) return;

        OnDie();
    }

    private void OnResetPlayer(Vector3 spawn)
    {
        _isDead = false;
        _animation.SetBool("dead", false);
        transform.position = spawn;

        _velocity.x = 0;
        _velocity.y = 0;
        this.ignorePhysics = false;
    }

    private void OnDie()
    {
        if (_isDead) return;
        _isDead = true;

        float final = 0f;
        float squaredAcceleration = final - 2 * _gravity * _deadJumpHeight;
        _jumpAcceleration = Mathf.Sqrt(squaredAcceleration);

        _velocity.x = 0;
        _velocity.y = _jumpAcceleration;
        _jumpAcceleration = 0;

        _animation.SetBool("dead", true);
        this.ignorePhysics = true;
    }

    private void OnSwipe(SwipeType type)
    {
        switch (type)
        {
            case SwipeType.UP:
                if (Collisions.bottom || canDoubleJump)
                {
                    float final = 0f;
                    float squaredAcceleration = final - 2 * _gravity * _jumpHeight;
                    _jumpAcceleration = Mathf.Sqrt(squaredAcceleration);

                    if (Collisions.bottom && PowerUpController.Instance.HasDoubleJumpPower) canDoubleJump = true;
                    else canDoubleJump = false;
                }
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
        if (!_isDead) WhileLiving();
        else WhileDead();

        _velocity.y += _gravity * Time.deltaTime;

        HandleAnimations();
        this.Move(_velocity * Time.deltaTime);
    }

    private void WhileDead() { }

    private void WhileLiving()
    {
        if (PowerUpController.Instance.IsPlayerInvincible)
        {
            _renderer.color = new Color(1f, 1f, 1f, .5f);
        }
        else
        {
            _renderer.color = new Color(1f, 1f, 1f, 1f);
        }
        if (transform.position.y <= .88f)
        {
            GameController.Instance.KillPlayer(gameObject.GetInstanceID());
            return;
        }

        if (Collisions.bottom)
        {
            _velocity.y = 0;
        }

        _velocity.x = _direction * _moveSpeed;

        if (_jumpAcceleration > 0)
        {
            _velocity.y = _jumpAcceleration;
            _animation.SetTrigger("jump");
            _jumpAcceleration = 0;
        }
    }

    public override void Move(Vector3 velocity)
    {
        base.Move(velocity);

        Vector3 position = transform.position;
        Vector3 xz = position;
        xz.y = 0;

        transform.rotation = Quaternion.LookRotation(-xz, Vector3.up);
        transform.position = xz.normalized * 2.5f + Vector3.up * position.y;
    }

    private void HandleAnimations()
    {
        _animation.SetBool("running", Mathf.Abs(_velocity.x) > 0);
        _renderer.flipX = _direction < 0;
    }

    private void OnDestroy()
    {
        if (GestureController.HasInstance())
        {
            GestureController.Instance.OnSwipe -= OnSwipe;
        }

        if (TowerController.HasInstance())
        {
            TowerController.Instance.OnMoveTower -= OnMove;
        }

        if (GameController.HasInstance())
        {
            GameController.Instance.OnPlayerDie -= OnPlayerDie;
            GameController.Instance.OnResetPlayer -= OnResetPlayer;
        }
    }
}
