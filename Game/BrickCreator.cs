using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrickCreator : MonoBehaviour {

    public static BrickCreator br;
    public Text scoreTxt;
    public GameObject spawner1, spawner2;
    public GameObject brick;
    public GameObject land;
    public List<GameObject> bricks = new List<GameObject>();
    public float delay = 0.5f;
    float delay_count;
    int spawn_pos;
    int rand;
    float starting_speed = 0.04f;
    float speed = 0.04f;
    public float speed_increment = 0.002f;
    float starting_speed_increment = 0.002f;

    //Delay before restart
    float res_delay = 3;
    float res_delay_count = 0;

    public static bool end = false;
    public static bool canCreate = true;
    bool pause = false;

    //Brick enlargment
    bool enlarged = true;
    int num = 0;

	void Start () {
        br = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (bricks.Count == 0)
        {
            delay_count = delay;
        }

        if (Player.playerReady)
        {
            if (!end && !pause)
            {
                delay_count += Time.deltaTime;
                if (delay_count > delay && canCreate)
                {
                    spawn();
                }
            }
            //Game end
            else if (!pause)
            {
                res_delay_count += Time.deltaTime;
                //Drop the beat
                foreach (GameObject obj in bricks)
                {
                    obj.GetComponent<Rigidbody2D>().isKinematic = false;
                    obj.GetComponent<Rigidbody2D>().gravityScale = 1.7f;
                }


                if (res_delay_count > res_delay && !pause)
                {
                    res_delay_count = 0;

                    clearBricks();
                    Rigidbody2D rb = Player.player.GetComponent<Rigidbody2D>();
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    DestroyAfter.drop = false;
                    Instantiate(land, Player.player.transform.position + Vector3.down * 10, new Quaternion(0, 0, 0, 0));


                    pauseOn();
                    end = false;
                }
            }
        }
        brickEnlarge();
	}

    void spawn()
    {
        canCreate = false;
        delay_count = 0;
        speed += speed_increment;
        speed_increment *= 0.97f;
        Debug.Log(speed);
        Debug.Log(speed_increment);
        rand = Random.Range(0, 2);
        GameObject brick_temp;
        if (rand == 1)
        {
            brick_temp = Instantiate(brick, spawner1.transform.position, spawner1.transform.rotation);
        }
        else
        {
            brick_temp = Instantiate(brick, spawner2.transform.position, spawner2.transform.rotation);
        }

        brick_temp.GetComponent<MovingBrick>().speed = speed;
        bricks.Add(brick_temp);

        //Up
        transform.position += Vector3.up * 0.6f;


        //EnLarge brick
        if (bricks.Count > 1 && (bricks.Count-1) % 10 == 0)
        {
            enlarged = false;
            num = bricks.Count - 2;
            //bricks[bricks.Count - 2].transform.localScale = new Vector3(6f, bricks[bricks.Count - 2].transform.localScale.y, bricks[bricks.Count - 2].transform.localScale.z);
            bricks[bricks.Count - 2].transform.position = new Vector3(Player.player.transform.position.x, bricks[bricks.Count - 2].transform.position.y, bricks[bricks.Count - 2].transform.position.z);
            MovingBrick.lastBrickX = Player.player.transform.position.x;
            MovingBrick.minusScale = 0;
        }
    }

    void brickEnlarge()
    {
        if (!enlarged)
        {
            bricks[num].transform.localScale += Vector3.right * 0.08f;
            if (bricks[num].transform.localScale.x > 7)
            {
                enlarged = true;
            }
        }
    }

    void clearBricks()
    {
        foreach (GameObject obj in bricks)
        {
            Destroy(obj.gameObject);
        }
        bricks = new List<GameObject>();
    }

    public void restart()
    {
        MovingBrick.minusScale = 0;
        MovingBrick.lastBrickX = Player.player.transform.position.x;
        DestroyAfter.drop = false;
        moveToPlayer();
        end = false;
        pause = false;
        SceneManager.LoadScene(0);
        
    }

    public void moveToPlayer()
    {
        transform.position = Player.player.transform.position;
        transform.position += Vector3.down * 0.05f;
        speed_increment = starting_speed_increment;
        MovingBrick.minusScale = 0;
        MovingBrick.lastBrickX = Player.player.transform.position.x;
        canCreate = true;
        delay_count = delay;
    }

    public void unpause()
    {
        speed = starting_speed;
        speed_increment = starting_speed_increment;
        //moveToPlayer();
        ShowEndGame.fade = true;
        pause = false;
    }

    public void pauseOn()
    {
        pause = true;
    }

    public bool getPause()
    {
        return pause;
    }
}
