using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player gestures
/// </summary>
/// <typeparam name="GestureController">Singleton</typeparam>
public class GestureController : Singleton<GestureController>
{
    private Vector2 _touchStartPos;
    private Vector2 _touchEndPos;
    private bool _swiping = false;

    [SerializeField]
    private float _minSwipeDistanceX, _minSwipeDistanceY;

    public System.Action<SwipeType> OnSwipe;

    void Awake()
    {
        _minSwipeDistanceX = Screen.width * 0.1f;
        _minSwipeDistanceY = Screen.height * 0.1f;
    }

    private void Update()
    {
        if (TouchBegin())
        {
            _swiping = true;
            _touchStartPos = TouchPos();
        }

        if (!_swiping) return;

        if (TouchMove() || TouchEnd())
        {
            _touchEndPos = TouchPos();
            Vector3 delta = _touchEndPos - _touchStartPos;

            if (Mathf.Abs(delta.y) > _minSwipeDistanceY)
            {
                if (delta.y > 0)
                {
                    DoSwipe(SwipeType.UP);
                }
                else
                {
                    DoSwipe(SwipeType.DOWN);
                }
            }
            else if (Mathf.Abs(delta.x) > _minSwipeDistanceX)
            {
                if (delta.x > 0)
                {
                    DoSwipe(SwipeType.RIGHT);
                }
                else
                {
                    DoSwipe(SwipeType.LEFT);
                }
            }
        }
    }

    /// <summary>
    /// Triggered when player swipes
    /// </summary>
    /// <param name="type"></param>
    private void DoSwipe(SwipeType type)
    {
        _swiping = false;
        if (OnSwipe != null)
        {
            OnSwipe(type);
        }
    }

    /// <summary>
    /// When the player touch begins
    /// </summary>
    /// <returns></returns>
    private bool TouchBegin()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonDown(0);
#else
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#endif
    }

    /// <summary>
    /// When the players touch ends
    /// </summary>
    /// <returns></returns>
    private bool TouchEnd()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonUp(0);
#else
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
#endif
    }

    /// <summary>
    /// When the players touch moves
    /// </summary>
    /// <returns></returns>
    private bool TouchMove()
    {
#if UNITY_EDITOR
        return Input.GetMouseButton(0);
#else
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
#endif
    }

    /// <summary>
    /// Players touch position
    /// </summary>
    /// <returns></returns>
    private Vector3 TouchPos()
    {
#if UNITY_EDITOR
        return Input.mousePosition;
#else
        return Input.GetTouch(0).position;
#endif
    }
}

public enum SwipeType
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}