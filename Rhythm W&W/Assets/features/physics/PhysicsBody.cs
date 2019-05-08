﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBody : MonoBehaviour {

    private const int _raycastWidth = 5;
    private const float _skinWidth = 0.015f;

    private BoxCollider _collider;

    [SerializeField]
    private LayerMask _layermask;

    public CollisionsInfo Collisions;

    protected void Awake() 
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void CalculatePhysics(ref Vector3 velocity) 
    {
        if (velocity.y != 0) VerticalCollision(ref velocity);
        if (velocity.x != 0) HorizontalCollision(ref velocity);
    }

    private void VerticalCollision(ref Vector3 velocity) 
    {
        RaycastHit hitInfo;
        Color color;
        Vector3 rayOrigin, rayLeft, rayForward;

        float rayLength = Mathf.Abs(velocity.y) + _skinWidth;
        float directionY = Mathf.Sign(velocity.y);       

        Vector3 origin = transform.position + _collider.center + (Vector3.up * directionY * _collider.size.y / 2) * transform.localScale.y;
        float halfWidth = _collider.size.x / 2f * transform.localScale.x;
        float halfDepth = _collider.size.z / 2f * transform.localScale.z;

        for (int x = 0; x < _raycastWidth; x++)
        {
            for (int y = 0; y < _raycastWidth; y++)
            {
                rayLeft = Vector3.left * Mathf.Lerp(-halfWidth, halfWidth, (float)x / (_raycastWidth - 1f));
                rayForward = Vector3.forward * Mathf.Lerp(-halfDepth, halfDepth, (float)y / (_raycastWidth - 1f));
                rayOrigin = origin + rayLeft + rayForward - (Vector3.up * _skinWidth * directionY);

                color = Color.Lerp(Color.Lerp(Color.red, Color.green, x / (_raycastWidth - 1f)), 
                                         Color.Lerp(Color.magenta, Color.yellow, y / (_raycastWidth - 1f)),
                                        (x + y) / (_raycastWidth * 2f -2));

                bool hit = Physics.Raycast(rayOrigin, Vector3.up * directionY, out hitInfo, rayLength, _layermask);

                if (hit) {
                    velocity.y = (hitInfo.distance - _skinWidth) * directionY;
                    rayLength = hitInfo.distance - _skinWidth;

                    Collisions.top = directionY == 1;
                    Collisions.bottom = directionY == -1;
                }

			    Debug.DrawRay(rayOrigin, Vector3.up * directionY * rayLength, color);
            }
        }

    }

    private void HorizontalCollision(ref Vector3 velocity) 
    {
        RaycastHit hitInfo;
        Color color;
        Vector3 rayOrigin, rayForward, rayUp;

        float rayLength = Mathf.Abs(velocity.x) + _skinWidth;
        float directionX = Mathf.Sign(velocity.x);       

        Vector3 origin = transform.position + _collider.center + (Vector3.right * directionX * _collider.size.x / 2) * transform.localScale.x;
        float halfWidth = _collider.size.z / 2f * transform.localScale.z;
        float halfDepth = _collider.size.y / 2f * transform.localScale.y;

        for (int x = 0; x < _raycastWidth; x++)
        {
            for (int y = 0; y < _raycastWidth; y++)
            {
                rayForward = Vector3.forward * Mathf.Lerp(-halfWidth, halfWidth, (float)x / (_raycastWidth - 1f));
                rayUp = Vector3.up * Mathf.Lerp(-halfDepth, halfDepth, (float)y / (_raycastWidth - 1f));
                rayOrigin = origin + rayForward + rayUp - (Vector3.right * _skinWidth * directionX);

                color = Color.Lerp(Color.Lerp(Color.red, Color.green, x / (_raycastWidth - 1f)), 
                                         Color.Lerp(Color.magenta, Color.yellow, y / (_raycastWidth - 1f)),
                                        (x + y) / (_raycastWidth * 2f -2));

                bool hit = Physics.Raycast(rayOrigin, Vector3.right * directionX, out hitInfo, rayLength, _layermask);

                if (hit) {
                    velocity.x = (hitInfo.distance - _skinWidth) * directionX;
                    rayLength = hitInfo.distance - _skinWidth;

                    Collisions.right = directionX == 1;
                    Collisions.left = directionX == -1;
                }

			    Debug.DrawRay(rayOrigin, Vector3.right * directionX * rayLength, color);
            }
        }

    }

    public void Move(Vector3 velocity) 
    {
        Collisions.Reset();

        CalculatePhysics(ref velocity);
        
        transform.Translate(velocity);
    }

    public struct CollisionsInfo 
    {
        public bool top, forward, backward, left, right, bottom;
        public void Reset() 
        {
            top = forward = backward = left = right = bottom = false;
        }
    }
}