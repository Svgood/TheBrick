using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesSpawn : MonoBehaviour {

    public GameObject cube;
    float delay, delay_count;
    float range_min, range_max;
	// Use this for initialization
	void Start () {
        delay = 0.6f;
        range_min = -2.5f;
        range_max = 2.5f;
	}
	
	// Update is called once per frame
	void Update () {
        delay_count += Time.deltaTime;
        if (delay_count > delay)
        {
            delay_count = 0;
            GameObject tmp = Instantiate(cube, transform.position + Vector3.right * Random.Range(range_min, range_max), transform.rotation);
            tmp.GetComponent<SpriteRenderer>().color = Colors.playerColor;
            tmp.GetComponent<SpriteRenderer>().sprite = SkinsController.playerSkin;
        }
	}
}
