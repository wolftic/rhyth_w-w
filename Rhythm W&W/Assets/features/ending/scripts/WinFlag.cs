using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WinFlag : MonoBehaviour {
    private BoxCollider _collider;
    private LayerMask _mask;
    
    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void Start() {
        _mask = LayerMask.GetMask("Player");
    }

    private void TriggerWin(PhysicsPlayer player) {
        player.gameObject.SetActive(false);
        GameController.Instance.TriggerWin();
    }

	private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + _collider.center, Vector3.Scale(_collider.size / 2, transform.localScale), transform.rotation, _mask);

        if (colliders.Length == 0) return;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].CompareTag("Player")) continue;

            TriggerWin(colliders[i].GetComponent<PhysicsPlayer>());
        }
    }
}
