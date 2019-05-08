using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : Singleton<CollectableController> 
{
    public event System.Action<int> OnPickup;

    public void RegisterPickedUp(int score)
    {
        if (OnPickup != null) OnPickup(score);
    }
}
