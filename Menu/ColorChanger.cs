using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour {


    public GameObject player;
    public GameObject[] bricks;
    public GameObject sky;

    private void Start()
    {
        sky = GameObject.Find("sky");
        player.GetComponent<SpriteRenderer>().color = Colors.playerColor;
        player.GetComponent<SpriteRenderer>().sprite = SkinsController.playerSkin;
        foreach (GameObject obj in bricks)
        {
            obj.GetComponent<SpriteRenderer>().color = Colors.bricksColor;
        }
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Colors.bricksColor;
    }

    public void setPlayer(Color color)
    {
        player.GetComponent<SpriteRenderer>().color = color;
        Colors.playerColor = color;
        PlayerPrefs.SetString("playerColor", "" + color.r + ";" + color.g + ";" + color.b + ";" + color.a);
    }

    public void setBricks(Color color)
    {
        foreach (GameObject obj in bricks)
        {
            obj.GetComponent<SpriteRenderer>().color = color;
        }
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
        Colors.bricksColor = color;
        PlayerPrefs.SetString("bricksColor", "" + color.r + ";" + color.g + ";" + color.b + ";" + color.a);
    }
    public void setSky(Color color)
    {
        sky.GetComponent<SpriteRenderer>().color = color;
        PlayerPrefs.SetString("skyColor", "" + color.r + ";" + color.g + ";" + color.b + ";" + color.a);
    }

   
}
