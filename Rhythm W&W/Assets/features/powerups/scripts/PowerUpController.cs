using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpTypes
{
    SLOW_MOTION,
    DOUBLE_JUMP,
    INVINCIBLE,
}

public class PowerUpController : Singleton<PowerUpController>
{
    [SerializeField]
    private float _slowmotionDurration = 4f;
    private float _slowmotionEndTime;

    [SerializeField]
    private float _invinibleDurration = 7f;
    private float _invinibleEndTime;
    public bool IsPlayerInvincible = false;

    [SerializeField]
    private float _doubleJumpDurration = 7f;
    private float _doubleJumpEndTime;
    public bool HasDoubleJumpPower = false;

    private List<PickablePowerUp> _powerUps = new List<PickablePowerUp>();

    public void AddPowerup(PickablePowerUp powerup)
    {
        if (_powerUps.Contains(powerup)) return;
        _powerUps.Add(powerup);
    }

    public void RegisterPickedUp(PowerUpTypes poweruptype)
    {
        switch (poweruptype)
        {
            case PowerUpTypes.SLOW_MOTION:
                SlowMotionPower();
                break;
            case PowerUpTypes.DOUBLE_JUMP:
                DoubbleJumpPower();
                break;
            case PowerUpTypes.INVINCIBLE:
                InvinciblePower();
                break;
        }
    }
    void SlowMotionPower()
    {
        _slowmotionEndTime = Time.time + _slowmotionDurration;
    }

    public void DoubbleJumpPower()
    {
        _doubleJumpEndTime = Time.time + _doubleJumpDurration;
    }

    public void InvinciblePower()
    {
        _invinibleEndTime = Time.time + _invinibleDurration;
    }

    public void HandleSlowmotion(bool isActive)
    {
        if (isActive)
        {
            Debug.Log("SlowMotionPower");
            Time.timeScale = .5f;
            MusicController.Instance._audioSource.pitch = 0.5f;
        }
        else
        {
            Time.timeScale = 1f;
            MusicController.Instance._audioSource.pitch = 1f;
        }
    }

    public void HandleInvincible(bool isActive)
    {
        if (isActive)
        {
            IsPlayerInvincible = true;
            Debug.Log("Player is Invincible");
        }
        else
        {
            IsPlayerInvincible = false;
        }
    }

    public void HandleDoubleJump(bool isActive)
    {
        if (isActive)
        {
            HasDoubleJumpPower = true;
            Debug.Log("Double Jump active");
        }
        else
        {
            HasDoubleJumpPower = false;
            Debug.Log("Double Jump not active");
        }
    }

    public void ResetPowerUps()
    {
        for (int i = 0; i < _powerUps.Count; i++)
        {
            _powerUps[i].gameObject.SetActive(true);
        }
    }

    internal void RemovePowerUp(PickablePowerUp powerup)
    {
        _powerUps.Remove(powerup);
    }

    private void Update()
    {
        HandleSlowmotion(Time.time < _slowmotionEndTime);
        HandleInvincible(Time.time < _invinibleEndTime);
        HandleDoubleJump(Time.time < _doubleJumpEndTime);
    }
}
