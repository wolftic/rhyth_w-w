using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Shake());
        }
	}
    private IEnumerator Shake()
    {
        Vector3 normalPosition = transform.position;
        for (int i = 0; i < 40; i++)
        {

            float randomX = Random.Range(-1, 2);
            float randomY = Random.Range(-1, 2);
            float randomZ = Random.Range(-1, 2);
            float timer = 0.01f;
            while (timer >= 0)
            {
                timer -= Time.deltaTime;
                transform.position += new Vector3(randomX / 20, randomY / 20, randomZ / 20);
                yield return null;
            }
        }
        transform.position = normalPosition;
    }
}
