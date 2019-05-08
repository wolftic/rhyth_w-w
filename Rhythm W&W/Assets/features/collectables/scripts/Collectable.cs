using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Collectable : MonoBehaviour 
{
    [SerializeField]
    private int _score = 1;

    private int _playerLayer;
    
    private void Awake()
    {
        _playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnPickup() 
    {
        gameObject.SetActive(false);
        CollectableController.Instance.RegisterPickedUp(_score);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != _playerLayer) return;
        OnPickup();
    }
}
