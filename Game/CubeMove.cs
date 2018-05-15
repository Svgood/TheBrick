using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour {

    float rotation, speed;
	// Use this for initialization
	void Start () {
        rotation = Random.Range(-1.5f, 1.5f);
        speed = Random.Range(0.02f, 0.06f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.down * speed ;
        transform.Rotate(new Vector3(0, 0, rotation));
        if (transform.position.y < -7)
            Destroy(this.gameObject);
	}
}
