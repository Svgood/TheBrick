using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveController : MonoBehaviour {

    public Text txt_max, txt_total;
    public static int totalPoints;
    public static int maxScore;
	// Use this for initialization
	void Start () {
        maxScore = PlayerPrefs.GetInt("max", 0);
        totalPoints = PlayerPrefs.GetInt("total", 0);


    }
	
	// Update is called once per frame
	void Update () {
		txt_max.text = "" + maxScore;
        txt_total.text = "" + totalPoints;
    }

    public static void updateScore(int score)
    {
        int cur = PlayerPrefs.GetInt("total", 0);
        cur += score;
        totalPoints = cur;
        PlayerPrefs.SetInt("total", cur);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt("max", score);
        }
    }

    public static void minusPoints(int count)
    {
        totalPoints -= count;
        PlayerPrefs.SetInt("total", totalPoints);
    }
}
