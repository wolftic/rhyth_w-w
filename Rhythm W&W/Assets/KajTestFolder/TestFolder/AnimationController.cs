using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    private Animator animator;
	void Start () {
        animator = GetComponent<Animator>();
        InputController.Instance.OnKeyPressed += OnKeyPressed;
        InputController.Instance.OnKeyReleased += OnKeyReleased;
	}
	
	void Update () {
		
	}
    void OnKeyPressed(string key)
    {
        switch (key)
        {
            default:
                return;
            case "left":
                
                break;
            case "right":
                break;
            case "up":
                print("doet het");
                animator.SetTrigger("Jump");
                break;

        }
    }
    void OnKeyReleased(string key)
    {
        switch (key)
        {
            default:
                return;
            case "left":
                
                break;
            case "right":
                break;
            case "up":
                
                break;

        }
    }
}
