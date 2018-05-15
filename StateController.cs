using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateController : MonoBehaviour {

    public GameObject menuAll;
    public GameObject gameObj, menuObj;
    public GameObject gameUI, menuUI;
    public GameObject shopObj, shopUI;
    public GameObject fakeUI;
    public GameObject skinUI;
    public Sprite pauseBtn1, pauseBtn2;
    public GameObject pause;
    public Text txt;
    public GameObject setButton;

    GameObject[] uis;
    int curUI;

    public GameObject btn1, btn2;
    float speed = 0.04f;
    float speed_acc = 0.003f;
    //1 - menu 2 - game
    //for pause
    public GameObject pauseBtn;

    public GameObject currShopObj;
    public static bool gameStarted = false;
    public static bool gamePause = false;
    public static bool showFadeScreen = true;
    Vector2 player_velocity;
    public AudioSource mainTheme;

    //For sound
    
	// Use this for initialization
	void Start () {
        if (Random.Range(0, 2) == 1)
            mainTheme = GameObject.Find("Ambient").GetComponent<AudioSource>();
        else
            mainTheme = GameObject.Find("ambien2").GetComponent<AudioSource>();
        uis = new GameObject[] {fakeUI, menuUI, shopUI, skinUI};
        curUI = 1;
        StartCoroutine(playMainTheme());
        Debug.Log(Application.systemLanguage);
    }
	


	// Update is called once per frame
	void Update () {
        moveAll();
        inputCheck();

    }

    public IEnumerator playMainTheme()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.2f);
            if (!mainTheme.isPlaying)
                mainTheme.Play();
        }

    }

    public void inputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameStarted)
        {
            if (curUI == 1)
                Application.Quit();
            if (curUI == 3)
            {
                curUI = 1;
                GetComponent<SkinsController>().closeSkins();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            pauseGame();
        }

 
    }

    public void startGame()
    {
        speed = 0;
        curUI = 0;
        showFadeScreen = false;
        gameStarted = true;

        gameUI.SetActive(true);
        gameObj.SetActive(true);

        menuObj.SetActive(false);
        uis[3].SetActive(false);
        //menuUI.SetActive(false);
    }

    public void openShop()
    {
        if (curUI == 1 || curUI == 3)
        {
            if (GetComponent<Settings>().settings.active)
                GetComponent<Settings>().settings.SetActive(false);

            currShopObj = Instantiate(shopObj, new Vector3(0, 0, 0), transform.rotation);
            speed = 0;
            shopUI.SetActive(true);
            curUI = 2;
        }
    }

    public void closeShop()
    {
        GameObject land = GameObject.Find("land");
        land.GetComponent<Rigidbody2D>().isKinematic = false;
        land.GetComponent<Rigidbody2D>().mass = 2;
        Destroy(currShopObj, 2);
        speed = 0;
        curUI = 1;
    }

    public void endGame()
    {
        SaveController.updateScore(Player.player.score);
        MovingBrick.minusScale = 0;
        gamePause = false;
        gameStarted = false;
        DestroyAfter.drop = false;
        Player.playerReady = false;
        BrickCreator.canCreate = true;
        BrickCreator.end = false;
        BrickCreator.br.unpause();
        SceneManager.LoadScene(0);
    }

    void moveAll()
    {
        if (Mathf.Abs(uis[curUI].transform.position.x - speed) > 0.3f)
        {
            if (uis[curUI].transform.position.x > 0)
            {
                foreach (GameObject obj in uis)
                {
                    obj.transform.position -= Vector3.right * speed;
                }
                speed += speed_acc;
            }
            if (uis[curUI].transform.position.x < 0)
            {
                foreach (GameObject obj in uis)
                {
                    obj.transform.position += Vector3.right * speed;
                }
                speed += speed_acc;
            }
        }
        else if (uis[curUI].transform.position.x != 0)
        {
            uis[curUI].transform.position = new Vector3(0, uis[curUI].transform.position.y, 0);
            if (curUI == 0)
            {
                uis[1].SetActive(false);
                uis[2].SetActive(false);
                uis[3].SetActive(false);
            }
            if (curUI == 1)
            {
                setButton.SetActive(true);
                uis[2].transform.position = uis[2].GetComponent<StartPos>().start_pos;
                uis[3].transform.position = uis[3].GetComponent<StartPos>().start_pos;
            }
            if (curUI == 2)
            {
                setButton.SetActive(false);
                uis[3].transform.position = uis[2].GetComponent<StartPos>().start_pos;
            }
            if (curUI == 3)
            {
                uis[2].SetActive(false);
            }
            else
                if (curUI != 0)
                    uis[2].SetActive(true);
        }
       

    }

    public void pauseGame()
    {
        if (!gamePause)
        {
            gamePause = true;
            player_velocity = Player.player.GetComponent<Rigidbody2D>().velocity;
            Player.player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Player.player.GetComponent<Rigidbody2D>().isKinematic = true;
            txt.enabled = true;
            pause.GetComponent<Image>().sprite = pauseBtn2;
            pauseBtn.SetActive(true);
        }
        else
        {
            gamePause = false;
            Player.player.GetComponent<Rigidbody2D>().isKinematic = false;
            Player.player.GetComponent<Rigidbody2D>().velocity = player_velocity;
            pause.GetComponent<Image>().sprite = pauseBtn1;
            txt.enabled = false;
            pauseBtn.SetActive(false);
        }
    }

    public void openSkins()
    {
        GetComponent<SkinsController>().openSkins();
        GameObject land = GameObject.Find("land");
        land.GetComponent<Rigidbody2D>().isKinematic = false;
        land.GetComponent<Rigidbody2D>().mass = 2;
        Destroy(currShopObj, 2);
        speed = 0;
        curUI = 3;
    }

    public void goToShop()
    {

    }

    public void btnClickedSound()
    {
        GetComponent<AudioSource>().Play();
    }

    public int getCurUI()
    {
        return curUI;
    }
    

}
