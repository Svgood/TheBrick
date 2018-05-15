using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    SpriteRenderer sprite;

    int curSound = 0;
    public AudioSource [] jumpSound;
    public AudioSource split;
    public Text txt;
    public GameObject playerParticle;
    public GameObject adWindow;
    GameObject pause;
    Rigidbody2D rb;


    GameObject lastLanded;

    bool onGround = true;
    bool pressed = false;
    public int score = 0;

    public static bool gameover = false;
    public static Player player;
    public static bool playerReady = false;


    bool canContinue = false;
    float d = 0.5f, dcount = 0;
    float lastY = 666;
	// Use this for initialization
	void Start () {
        pause = GameObject.Find("Pause");
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Colors.playerColor;
        player = this;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        sprite.sprite = SkinsController.playerSkin;
     
      
        txt.text = "" + score;
        if (lastY != 666)
            if (transform.position.y < lastY)
            {
                lastY = 666;
                end();
                return;
            }

        if (BrickCreator.br.getPause())
        {
            score = 0;
        }

        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		if (Input.GetMouseButtonDown(0) && onGround && !BrickCreator.br.getPause() && !StateController.gamePause && Camera.main.ScreenToWorldPoint(Input.mousePosition).y < transform.position.y + 4.5f && !BrickCreator.end)
        {
            

            onGround = false;
            rb.AddForce(Vector2.up * 450f);
            pressed = true;
        }
        

        if (Input.GetMouseButtonUp(0))
        {
            pressed = false;
            if (rb.velocity.y > 0)
                rb.velocity = new Vector2(0, 0);
        }

        if (rb.velocity.y < 0.1f && !pressed && !BrickCreator.end && !StateController.gamePause)
        {
            rb.velocity += Vector2.down * 0.5f;
        }


        if (Input.GetMouseButtonDown(0) && BrickCreator.br.getPause() && dcount > d && Input.mousePosition.y < Screen.height / 2)
        {
            pause.SetActive(true);
            adWindow.GetComponent<AdButtonMove>().moveOut();
            dcount = 0;
            canContinue = false;
            BrickCreator.br.unpause();
            ParticleMovement.moveBack = true;
            BrickCreator.br.moveToPlayer();
            GameObject.FindGameObjectWithTag("NoName").tag = "Land";
            transform.SetPositionAndRotation(transform.position, new Quaternion(0, 0, 0, 0));
        }

        if (canContinue)
        {
            dcount += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //createParticles();
        if (collision.collider.tag == "Wall")
        {
            

            if (BrickCreator.br.bricks.Count > 0)
            {
                if (!DestroyAfter.drop)
                {
                    DestroyAfter.drop = true;
                }
            }

            if (collision.collider.GetComponent<MovingBrick>().getMove() && !BrickCreator.end)
            {
                lastY = collision.collider.transform.position.y - 0.5f;
                BrickCreator.canCreate = true;
                lastLanded = collision.gameObject;
                playSound();
                score += 1;
                collision.collider.GetComponent<MovingBrick>().unMove();
                if (GameObject.FindGameObjectWithTag("Land") != null)
                    GameObject.FindGameObjectWithTag("Land").tag = "NoName";
            }
            else
            {
                if (collision.gameObject != lastLanded && !BrickCreator.end)
                {
                    end();
                }
            }
            onGround = true;
        }
        if (collision.collider.tag == "AWall")
        {
            onGround = true;
        }
        if (collision.collider.tag == "Pusher")
        {
            rb.AddForce(Vector2.right * 350f * (transform.position.x - collision.transform.position.x));
            if (!BrickCreator.end)
            {
                end();
            }
        }

        //someShit
        if (collision.collider.tag == "Land" && !playerReady)
        {
            playerReady = true;
            playSound();
            MovingBrick.lastBrickX = Player.player.transform.position.x;
            
            //BrickCreator.br.moveToPlayer();
        }

        if (collision.collider.tag == "Land" && BrickCreator.br.getPause())
        {
            pause.SetActive(false);
            AdsController.gameEnded += 1;
            canContinue = true;
            createParticles();
            rb.rotation = 0;
            rb.velocity = new Vector2(0, 0);

            transform.SetPositionAndRotation(transform.position, new Quaternion(0, 0, 0, 0));


            transform.position = new Vector3(collision.collider.transform.position.x, collision.collider.transform.position.y + collision.collider.transform.localScale.y / 2 + transform.localScale.y / 2, 0);
            transform.position = new Vector3(collision.collider.transform.position.x, transform.position.y, transform.position.z);
            ShowEndGame.startFading = true;
            GameObject.FindGameObjectWithTag("Land").tag = "NoName";
        }

        if (collision.collider.tag == "Land")
        {
            onGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!BrickCreator.br.getPause())
        {
            Destroy(collision.gameObject);
            Color tmp = sprite.color;
            tmp.a += 0.08f;
            sprite.color = tmp;
        }
    }

    public bool getOnGround()
    {
        return onGround;
    }

    public void createParticles()
    {
        split.Play();
        ParticleMovement.moveBack = false;
        for(int i = 0; i < 4; i++)
            for(int k = 0; k < 4; k++)
            {
                Instantiate(playerParticle, new Vector3(transform.position.x - transform.localScale.x / 2 + 0.15f * k, transform.position.y - transform.localScale.y / 2 + 0.15f * i, transform.position.z), transform.rotation).GetComponent<SpriteRenderer>().sprite = sprite.sprite;
            }
        Color tmp = sprite.color;
        tmp.a = 0;
        sprite.color = tmp;
    }

    public void playSound()
    {
        curSound = Random.Range(0, jumpSound.Length);
        jumpSound[curSound].Play();
    }

    public void end()
    {
        lastY = 666;
        adWindow.GetComponent<AdButtonMove>().moveIn();
        BrickCreator.end = true;
        SaveController.updateScore(score);
    }

    
}
