using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all the physics from a character
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public abstract class PhysicsBody : MonoBehaviour {
    private const int _raycastWidth = 5;
    private const float _skinWidth = 0.015f;

    private BoxCollider _collider;

    [SerializeField]
    private LayerMask _layermask;

    public CollisionsInfo Collisions;

    internal bool ignorePhysics = false;

    protected void Awake() 
    {
        _collider = GetComponent<BoxCollider>();
    }

    /// <summary>
    /// Calculate physics from an velocity
    /// </summary>
    /// <param name="velocity">Input velocity</param>
    private void CalculatePhysics(ref Vector3 velocity) 
    {
        if (velocity.y != 0) VerticalCollision(ref velocity);
        if (velocity.x != 0) HorizontalCollision(ref velocity);
    }

    /// <summary>
    /// Calculate vertical collisions
    /// </summary>
    /// <param name="velocity">Input velocity</param>
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
                rayLeft = -transform.right * Mathf.Lerp(-halfWidth, halfWidth, (float)x / (_raycastWidth - 1f));
                rayForward = transform.forward * Mathf.Lerp(-halfDepth, halfDepth, (float)y / (_raycastWidth - 1f));
                rayOrigin = origin + rayLeft + rayForward - (Vector3.up * _skinWidth * directionY);

                color = Color.Lerp(Color.Lerp(Color.red, Color.green, x / (_raycastWidth - 1f)), 
                                         Color.Lerp(Color.magenta, Color.yellow, y / (_raycastWidth - 1f)),
                                        (x + y) / (_raycastWidth * 2f -2));

                bool hit = Physics.Raycast(rayOrigin, Vector3.up * Mathf.Min(directionY, 0), out hitInfo, rayLength, _layermask);

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

    /// <summary>
    /// Calculate horizontal collisions
    /// </summary>
    /// <param name="velocity">Input velocity</param>
    private void HorizontalCollision(ref Vector3 velocity) 
    {
        RaycastHit hitInfo;
        Color color;
        Vector3 rayOrigin, rayForward, rayUp;

        float rayLength = Mathf.Abs(velocity.x) + _skinWidth;
        float directionX = Mathf.Sign(velocity.x);       

        Vector3 origin = transform.position + _collider.center + (transform.right * directionX * _collider.size.x / 2) * transform.localScale.x;
        float halfWidth = _collider.size.z / 2f * transform.localScale.z;
        float halfDepth = _collider.size.y / 2f * transform.localScale.y;

        for (int x = 0; x < _raycastWidth; x++)
        {
            for (int y = 0; y < _raycastWidth; y++)
            {
                rayForward = transform.forward * Mathf.Lerp(-halfWidth, halfWidth, (float)x / (_raycastWidth - 1f));
                rayUp = Vector3.up * Mathf.Lerp(-halfDepth, halfDepth, (float)y / (_raycastWidth - 1f));
                rayOrigin = origin + rayForward + rayUp - (transform.right * _skinWidth * directionX);

                color = Color.Lerp(Color.Lerp(Color.red, Color.green, x / (_raycastWidth - 1f)), 
                                         Color.Lerp(Color.magenta, Color.yellow, y / (_raycastWidth - 1f)),
                                        (x + y) / (_raycastWidth * 2f -2));

                bool hit = Physics.Raycast(rayOrigin, transform.right * directionX, out hitInfo, rayLength, _layermask);

                if (hit) {
                    velocity.x = (hitInfo.distance - _skinWidth) * directionX;
                    rayLength = hitInfo.distance - _skinWidth;

                    Collisions.right = directionX == 1;
                    Collisions.left = directionX == -1;
                }

			    Debug.DrawRay(rayOrigin, transform.right * directionX * rayLength, color);
            }
        }

    }

    /// <summary>
    /// Move the gameobject in the direction of velocity
    /// </summary>
    /// <param name="velocity">Input velocity</param>
    public virtual void Move(Vector3 velocity) 
    {
        Collisions.Reset();

        if (!ignorePhysics) 
        {
            CalculatePhysics(ref velocity);
        }

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
