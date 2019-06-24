using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablePowerUp : MonoBehaviour
{
    [SerializeField]
    private PowerUpTypes type;

    private int _playerLayer;

    private void Awake()
    {
        _playerLayer = LayerMask.NameToLayer("Player");
    }

    private void Start()
    {
        PowerUpController.Instance.AddPowerup(this);
    }

    private void OnPickup()
    {
        gameObject.SetActive(false);
        PowerUpController.Instance.RegisterPickedUp(type);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != _playerLayer) return;
        OnPickup();
    }

    private void OnDestroy()
    {
        PowerUpController.Instance.RemovePowerUp(this);
    }
}
