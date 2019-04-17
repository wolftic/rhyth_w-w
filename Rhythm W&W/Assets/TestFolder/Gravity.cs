using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
    private float gravity = -0.2f;
    public float playerVelocity;
    public float jump;
    public void FixedUpdate()
    {
        float velocity = transform.position.y;
        if(playerVelocity > gravity)
        {
            playerVelocity -= 0.01f;
        }
        else
        {
            playerVelocity = gravity;
        }
        transform.position += new Vector3(0, playerVelocity);
        float dif = transform.position.y - velocity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity = 0.30f; 
        }
    }
}