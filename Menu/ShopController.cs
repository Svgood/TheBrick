using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    public int cur_selected = 1;
    public Button[] btns;
    public int[] buy;
    public Text txt, txt_totalShop;
    Localization loc;
	// Use this for initialization
	void Start () {
        loc = GameObject.Find("Localization").GetComponent<Localization>();
        txt_totalShop = GameObject.Find("TotalShop").GetComponent<Text>();
        txt_totalShop.text = "" + SaveController.totalPoints;


        buy = new int[btns.Length];
        string[] temp = PlayerPrefs.GetString("shop", "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0").Split(';');
        Debug.Log(temp);
        for (int i = 0; i < btns.Length; i++)
        {
            if (temp[i] == "1")
            {
                buy[i] = 1;
                Destroy(btns[i].transform.GetChild(0).gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (cur_selected == 1)
            txt.text = loc.player;
        if (cur_selected == 2)
            txt.text = loc.bricks;
        if (cur_selected == 3)
            txt.text = loc.sky;
        minusPointsShop();
    }

    public void setColor(int id)
    {
        if (buy[id] == 0)
        {
            if (SaveController.totalPoints - btns[id].GetComponent<PurchaseBtnScript>().cost >= 0)
            {
                SaveController.minusPoints(btns[id].GetComponent<PurchaseBtnScript>().cost);
                if (btns[id].transform.GetChild(0).gameObject != null)
                    Destroy(btns[id].transform.GetChild(0).gameObject);
                buy[id] = 1;
                save();

                if (cur_selected == 1)
                    GetComponent<StateController>().currShopObj.GetComponent<ColorChanger>().setPlayer(btns[id].colors.normalColor);
                if (cur_selected == 2)
                    GetComponent<StateController>().currShopObj.GetComponent<ColorChanger>().setBricks(btns[id].colors.normalColor);
                if (cur_selected == 3)
                    GetComponent<StateController>().currShopObj.GetComponent<ColorChanger>().setSky(btns[id].colors.normalColor);
            }
        }
        else
        {
            if (cur_selected == 1)
                GetComponent<StateController>().currShopObj.GetComponent<ColorChanger>().setPlayer(btns[id].colors.normalColor);
            if (cur_selected == 2)
                GetComponent<StateController>().currShopObj.GetComponent<ColorChanger>().setBricks(btns[id].colors.normalColor);
            if (cur_selected == 3)
                GetComponent<StateController>().currShopObj.GetComponent<ColorChanger>().setSky(btns[id].colors.normalColor);
        }

    }

    public void minusPointsShop()
    {
        if(int.Parse(txt_totalShop.text) > SaveController.totalPoints)
        {
            txt_totalShop.text = "" + (int.Parse(txt_totalShop.text) - 2);
        }
        if (int.Parse(txt_totalShop.text) < SaveController.totalPoints)
        {
            txt_totalShop.text = "" + SaveController.totalPoints;
        }
    }

    public void next()
    {
        if (GetComponent<StateController>().getCurUI() == 2)
        {
            if (cur_selected < 3)
            {
                cur_selected += 1;


            }
            else
                GetComponent<StateController>().openSkins();
        }
    }

    public void previous()
    {
        if (GetComponent<StateController>().getCurUI() == 2)
        {
            if (cur_selected > 1)
            {
                cur_selected -= 1;

            }
            else
            {
                GetComponent<StateController>().closeShop();
                GetComponent<StateController>().setButton.SetActive(true);
            }
        }
    }

    public void save()
    {
        string t = "";
        for (int i = 0; i < buy.Length; i++)
        {
            t += "" + buy[i] + ";";
        }
        t.Substring(0, t.Length - 1);
        PlayerPrefs.SetString("shop", t);
    }

    public void reset()
    {
        PlayerPrefs.SetString("shop", "0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0");
        SaveController.minusPoints(-500);
    }

    public void addMoney()
    {
        SaveController.minusPoints(-500);
    }
}
