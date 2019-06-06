using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script you can add to a sprite to make it a collectable item.
/// </summary>
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

    private void Start()
    {
        CollectableController.Instance.AddCollectable(this);
    }

    /// <summary>
    /// An event that gets called whenever the Collectable gets picked up
    /// </summary>
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

    private void OnDestroy()
    {
        CollectableController.Instance.RemoveCollectable(this);
    }
}
