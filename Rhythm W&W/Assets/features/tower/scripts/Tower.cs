using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField]
    private AudioClip _music;

    private void OnEnable()
    {
        TowerController.Instance.Init(_music);
    }

    private void Start() {
        TowerController.Instance.OnMoveTower += OnMove;
    }

    private void OnMove(float plummetSpeed, float rotationSpeed) {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        transform.Translate(0, plummetSpeed * Time.deltaTime, 0);
    }

    public void Reset()
    {
        transform.position = Vector3.zero;
    }

    private void OnDestroy() {
        if (TowerController.HasInstance()) 
        {
            TowerController.Instance.OnMoveTower -= OnMove;
        }    
    }
}
