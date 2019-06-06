using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This handles every collectable in game, including resetting.
/// </summary>
/// <typeparam name="CollectableController">Singleton</typeparam>
public class CollectableController : Singleton<CollectableController> 
{
    public event System.Action<int> OnPickup;

    private List<Collectable> _collectables = new List<Collectable>();

    /// <summary>
    /// Add the collectable to the list
    /// </summary>
    /// <param name="collectable">Collectable in question</param>
    public void AddCollectable(Collectable collectable) 
    {
        if (_collectables.Contains(collectable)) return;
        _collectables.Add(collectable);
    }

    /// <summary>
    /// Notify that a collectable is picked up
    /// </summary>
    /// <param name="score">The amount that the collectible was worth</param>
    public void RegisterPickedUp(int score)
    {
        if (OnPickup != null) OnPickup(score);
    }

    /// <summary>
    /// Bring back all the collectables to every map
    /// </summary>
    public void ResetCollectables()
    {
        for (int i = 0; i < _collectables.Count; i++)
        {
            _collectables[i].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Remove the collectable from the list
    /// </summary>
    /// <param name="collectable">Collectable in question</param>
    internal void RemoveCollectable(Collectable collectable)
    {
        _collectables.Remove(collectable);
    }
}
