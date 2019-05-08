using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [ContextMenu("Get new radius")]
    private void GetRadius() 
    {
        GameObject tower = GameObject.FindGameObjectWithTag("Tower");
        if (!tower) {
            _radius = 5;
        } else {
            _radius = tower.transform.localScale.x / 2;
        }
    }

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
