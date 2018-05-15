using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsController : MonoBehaviour {

    public Text txt, setBtnText;
    public Sprite [] skins;
    public static Sprite playerSkin;
    public GameObject spawn;
    GameObject player, obj, nextBtn;
    Localization loc;

    string save;
    int[] buy;
    int[] cost;

    int skinsCost = 500;
    int curSkin = 0;
    int moveOut = 0;
	// Use this for initialization
	void Start () {
        //reset();
        loc = GameObject.Find("Localization").GetComponent<Localization>();
        buy = new int[skins.Length];
        cost = new int[skins.Length];

        nextBtn = GameObject.Find("NextSkins");
        save = PlayerPrefs.GetString("skins", "20");
        for (int i = 0; i < save.Length; i++)
        {
            if (save[i] != '0')
            {
                buy[i] = 1;
            }
            if (save[i] == '2')
            {
                buy[i] = 2;
                playerSkin = skins[i];
            }
           
        }
        for (int i = 0; i < cost.Length; i++)
            cost[i] = skinsCost;
    }
	
	// Update is called once per frame
	void Update () {
        if (obj != null)
        {

            obj.transform.Rotate(new Vector3(0, 0, 2));

            if (obj.transform.position.y > -0.5f)
                obj.GetComponent<Rigidbody2D>().velocity += Vector2.up * 0.04f;
            else
            {

                obj.GetComponent<Rigidbody2D>().isKinematic = true;
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }

            obj.transform.position += Vector3.right * 0.1f * moveOut;
        }
        else
        {
            moveOut = 0;
        }

    }

    public void next()
    {
        if (curSkin + 1 < skins.Length)
        {
            curSkin += 1;
            changeSkin();
            if (curSkin == skins.Length - 1)
                nextBtn.SetActive(false);
        }
    }

    public void prev()
    {
        if (curSkin == 0)
        {
            closeSkins();
            GetComponent<StateController>().openShop();

        }
        else
        {
            nextBtn.SetActive(true);
            curSkin -= 1;
            changeSkin();
        }
    }

    public void closeSkins()
    {
        Destroy(obj, 2);
        moveOut = 1;
    }

    public void saveAll()
    {
        string tmp = "";
        for (int i = 0; i < buy.Length; i++)
        {
            tmp += "" + buy[i];
        }
        PlayerPrefs.SetString("skins", tmp);
    }

    public void set()
    {
        playerSkin = skins[curSkin];
        for (int i = 0; i < buy.Length; i++)
        {
            if (buy[i] == 2)
            {
                buy[i] = 1;
                break;
            }
        }
        buy[curSkin] = 2;
        setBtnText.text = loc.chosen;
        saveAll();
    }

    public void buySkin()
    {
        if (buy[curSkin] == 0)
        {
            if (SaveController.totalPoints >= skinsCost)
            {
                SaveController.minusPoints(cost[curSkin]);
                setBtnText.text = loc.chosen;
                set();
            }
        }
        else
            set();
    }

    public void changeSkin()
    {

        if (buy[curSkin] == 0)
            setBtnText.text = "" + cost[curSkin];
        else if (buy[curSkin] == 1)
            setBtnText.text = loc.set;
        else
            setBtnText.text = loc.chosen;

        txt.text = skins[curSkin].name;
        obj.GetComponent<SpriteRenderer>().sprite = skins[curSkin];
    }

    public void openSkins()
    {
        moveOut = 0;
        player = Instantiate(spawn, new Vector3(0, 0, 0), transform.rotation);
        obj = player.transform.GetChild(0).gameObject;
        obj.GetComponent<SpriteRenderer>().color = Colors.playerColor;
    }

    private void reset()
    {
        PlayerPrefs.SetString("skins", "20");
    }
}
