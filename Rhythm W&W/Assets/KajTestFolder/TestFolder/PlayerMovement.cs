using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localEulerAngles -= new Vector3(0, 1f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localEulerAngles += new Vector3(0, 1f);
        }
    }
}