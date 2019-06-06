using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps all the information for a level
/// </summary>
public class Level : MonoBehaviour {
    public string Name;
    public int PointsToUnlock;
    public Sprite Image;

    public Vector3 SpawnPoint {
        get {
            return _spawnPoint.position;
        }
    }

    [SerializeField]
    private Transform _spawnPoint;
}
