using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Singleton<InputController> {
    public Action<string> OnKeyPressed;
    public Action<string> OnKeyReleased;
	void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnKeyPressed("left");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnKeyPressed("right");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnKeyPressed("up");
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            OnKeyReleased("left");
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            OnKeyReleased("right");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnKeyReleased("up");
        }
    }
}
