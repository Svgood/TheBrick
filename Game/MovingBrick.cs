using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBrick : MonoBehaviour {

    SpriteRenderer sprite;

    public static float lastBrickX = 0;
    public static float minusScale = 0;
    public GameObject p1, p2;
    public GameObject scaleBrick;
    public GameObject staticBrick;
    int dir;
    public float speed = 0.05f;
    bool move = true;
	// Use this for initialization
	void Start () {

        transform.localScale -= Vector3.right * minusScale;
        sprite = GetComponent<SpriteRenderer>();

        sprite.color = Colors.bricksColor;
        if (transform.position.x < Player.player.transform.position.x)
        {
            dir = 1;
        }
        else
            dir = -1;
	}
	
	// Update is called once per frame
	void Update () {
        if (move && !StateController.gamePause)
        {
            transform.position += Vector3.right * dir * speed;
            if (transform.position.x > Player.player.transform.position.x + 3)
            {
                dir = -1;
            }
            if (transform.position.x < Player.player.transform.position.x - 3)
            {
                dir = 1;
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && collision.transform.position.y > transform.position.y + transform.localScale.y / 2)
        {
            
            p1.SetActive(false);
            p2.SetActive(false);

            if (BrickCreator.br.bricks.Count > 1 && move && !((BrickCreator.br.bricks.Count - 1) % 10 == 0))
            {
                float offset = Mathf.Abs(transform.position.x - lastBrickX);
                //Debug.Log(offset);
                //Debug.Log(transform.lossyScale.x);
                GameObject temp = null;

                if (transform.position.x > lastBrickX)
                {
                    temp = Instantiate(scaleBrick, new Vector3(lastBrickX, transform.position.y, transform.position.z) + (Vector3.right * (transform.localScale.x / 2)), transform.rotation);
                    temp.transform.localScale = new Vector3(offset, transform.localScale.y, 1);
                    temp.transform.position += Vector3.right * temp.transform.localScale.x / 2;

                    
                    float oldx = transform.localScale.x;
                    transform.localScale += Vector3.left * offset;
                    transform.position = new Vector3(lastBrickX + oldx/2 - transform.localScale.x/2, transform.position.y, transform.position.z);
                    //transform.position += Vector3.left * (offset/3);

                }
                if (transform.position.x < lastBrickX)
                {
                    temp = Instantiate(scaleBrick, new Vector3(lastBrickX, transform.position.y, transform.position.z) - (Vector3.right * (transform.localScale.x / 2)), transform.rotation);
                    temp.transform.localScale = new Vector3(offset, transform.localScale.y, 1);
                    temp.transform.position -= Vector3.right * temp.transform.localScale.x / 2;

                    float oldx = transform.localScale.x;
                    transform.localScale += Vector3.left * offset;
                    transform.position = new Vector3(lastBrickX - oldx / 2 + transform.localScale.x / 2, transform.position.y, transform.position.z);
                    //transform.position -= Vector3.left * (offset / 3);

                }
                temp.GetComponent<SpriteRenderer>().color = sprite.color;
                minusScale += offset;
            }
           
            lastBrickX = transform.position.x;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dir *= -1;
        transform.position += Vector3.right * dir * speed * 2;
    }
    public bool getMove()
    {
        return move;
    }

    public void unMove()
    {
        move = false;
    }
}
