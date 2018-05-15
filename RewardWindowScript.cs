using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardWindowScript : MonoBehaviour {
    float txt_b_start;
    public Text txt_bonus, txt_total;
    public Image img;
    Color tmp;
    int cur, end;
    bool start = false;
	// Use this for initialization
	void Start () {
        txt_b_start = txt_bonus.transform.position.y - Player.player.transform.position.y  ;
        cur = PlayerPrefs.GetInt("total");
        txt_total.text = "" + cur;
        end = cur + 100;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            txt_bonus.transform.position += Vector3.down * 0.03f;
            tmp = txt_bonus.color;
            if (tmp.a > 0)
                tmp.a -= 0.01f;
            txt_bonus.color = tmp;

            if (cur < end)
            {
                cur += 1;
                txt_total.text = "" + cur;
            }
            else
            {
                tmp = img.color;
                if (tmp.a > 0)
                    tmp.a -= 0.01f;
                img.color = tmp;
                tmp = txt_total.color;
                if (tmp.a > 0)
                    tmp.a -= 0.01f;
                else
                {
                    tmp.a = 1;
                    txt_total.color = tmp;
                    txt_bonus.color = tmp;
                    tmp.a = 0.3f;
                    img.color = tmp;
                    start = false;
                    txt_bonus.transform.position = new Vector3(txt_bonus.transform.position.x, Player.player.transform.position.y +  txt_b_start, txt_bonus.transform.position.z);
                    tmp.a = 1;
                    SaveController.minusPoints(-100);
                    this.gameObject.SetActive(false);
                }
                
                txt_total.color = tmp;

               
            }
        }
	}

    public void startReward()
    {
        this.gameObject.SetActive(true);
        start = true;
        cur = PlayerPrefs.GetInt("total");
        txt_total.text = "" + cur;
        end = cur + 100;
    }
}
