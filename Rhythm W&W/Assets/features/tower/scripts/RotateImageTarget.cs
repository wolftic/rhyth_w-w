using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotate image based on players position
/// </summary>
public class RotateImageTarget : MonoBehaviour {
    [SerializeField]
    private PhysicsPlayer _player;

    private Quaternion _oldRotation;

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _player.transform.rotation, 10f * Time.deltaTime);
        // _oldRotation = _player.transform.rotation;
    }
}
