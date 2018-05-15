using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLandMoving : MonoBehaviour {

    bool finished = false;
    float speed = 0.05f;
    float speed_acc = 0.01f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -5 && !finished)
        {
            transform.position += Vector3.up * speed;
            speed += speed_acc;
        }
        else
            finished = true;
	}
}
