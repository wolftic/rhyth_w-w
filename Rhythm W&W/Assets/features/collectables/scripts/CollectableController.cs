using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : Singleton<CollectableController> 
{
    public event System.Action<int> OnPickup;

    private List<Collectable> _collectables = new List<Collectable>();

    public void AddCollectable(Collectable _collectable) 
    {
        if (_collectables.Contains(_collectable)) return;
        _collectables.Add(_collectable);
    }

    public void RegisterPickedUp(int score)
    {
        if (OnPickup != null) OnPickup(score);
    }

    public void ResetCollectables()
    {
        for (int i = 0; i < _collectables.Count; i++)
        {
            _collectables[i].gameObject.SetActive(true);
        }
    }

    internal void RemoveCollectable(Collectable collectable)
    {
        _collectables.Remove(collectable);
    }
}
