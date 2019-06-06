using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Obstacle : MonoBehaviour {
    private BoxCollider _collider;
    private LayerMask _mask;
    
    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void Start() {
        _mask = LayerMask.GetMask("Player");
    }

    private void Kill(PhysicsPlayer player) {
        GameController.Instance.KillPlayer(player.gameObject.GetInstanceID());
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + _collider.center, _collider.size / 2, transform.rotation, _mask);

        if (colliders.Length == 0) return;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].CompareTag("Player")) continue;

            Kill(colliders[i].GetComponent<PhysicsPlayer>());
        }
    }
}
