using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdButtonMove : MonoBehaviour {

    float offset;
    float speed = 0.1f;
    int move = 0;
	// Use this for initialization
	void Start () {
        
        offset = 0 - transform.position.x;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (move == 1)
        {
            
            transform.position += Vector3.right * speed;
            if (transform.position.x > Player.player.transform.position.x)
            {
                move = 0;
            }
        }
        if (move == -1)
        {
            transform.position -= Vector3.right * speed;
            if (transform.position.x < Player.player.transform.position.x - offset)
            {
                move = 0;
            }
        }
    }

    public void moveIn()
    {
        if (AdsController.timeBeforeBonus <= 0)
        {

            if (Advertisement.IsReady())
            {
                AdsController.timeBeforeBonus = 2;
                move = 1;
            }

        }
        else
        {
            AdsController.timeBeforeBonus -= 1;
        }
    }

    public void moveOut()
    {
        move = -1;
    }
}
