using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Easy to use script to rotate and place the sprite on the tower based on location
/// </summary>
[ExecuteInEditMode]
public class TowerProjector : MonoBehaviour
{
    [SerializeField]
    private bool _isStatic;

    private float _radius;
    
    private void Awake() 
    {
        GetRadius();
    }

    private void Start() 
    {
        ApplyTranslation();
    }

    private void Update()
    {
        if (_isStatic && Application.isPlaying) return;
        ApplyTranslation();
    }

    /// <summary>
    /// Get tower radius
    /// </summary>
    private void GetRadius() 
    {
        GameObject tower = GameObject.FindGameObjectWithTag("Tower");
        if (!tower) {
            _radius = 5;
        } else {
            _radius = tower.transform.localScale.x / 2;
        }
    }

    /// <summary>
    /// Apply rotation and position to the sprite
    /// </summary>
    private void ApplyTranslation()
    {
        Vector3 position = transform.position;
        float yPosition = position.y;
        position.y = 0;
        
        transform.rotation = Quaternion.LookRotation(position, Vector3.up);
        transform.position = position.normalized * _radius;

        transform.position += Vector3.up * yPosition;
    }
}
