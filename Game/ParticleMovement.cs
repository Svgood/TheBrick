using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovement : MonoBehaviour {


    public static bool moveBack = false;
    Vector2 dir;
    float speed = 0.1f;
    float speed_dec = -0.001f;
    float rotation = 0;
    Vector2 start_pos;
    //move back
    float speed_recalc = 0;
    float dist;
	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().color = Colors.playerColor;
        start_pos = new Vector2(transform.position.x, transform.position.y);
        dir = new Vector2(Random.Range(-1f, 1f), 1);
        speed -= Random.Range(0, 0.04f);
        rotation = Random.Range(-1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(dir.x, dir.y, 0)*speed;
        if (speed > 0)
            speed += speed_dec;
        else
            speed = 0;
        transform.Rotate(new Vector3(0, 0, rotation));

        if (moveBack)
        {
            if (speed_recalc == 0)
            {
                dir = new Vector2(transform.position.x - Player.player.transform.position.x, transform.position.y - Player.player.transform.position.y);

                Destroy(this.gameObject, 4);
                dist = Mathf.Sqrt(Mathf.Pow((start_pos.x - transform.position.x), 2) + Mathf.Pow((start_pos.y - transform.position.y), 2));
                speed_recalc = dist / 60;
                if (speed_recalc < 0.5f)
                    speed_recalc = 0.1f;

                speed_recalc = 0.05f;
            }
            else
            {
                speed_recalc = 0.05f;
                transform.position -= new Vector3(dir.x, dir.y, 0) * speed_recalc;
            }
        }
	}

    
}
