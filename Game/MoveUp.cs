using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour {

    public float dist;
    public float speed = 0.1f;
    public float acceleration = 0.01f;
    float passedDist;
    bool moving = true;
	// Use this for initialization
	void Start () {
        //start_pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            transform.position += Vector3.up * speed;
            passedDist += speed;
            speed += acceleration;
            if (passedDist > dist)
            {
                moving = false;
            }
        }
	}
}
