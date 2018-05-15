using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colors : MonoBehaviour {

    public Color32 red, blue, orange, green, yellow, cyan, purple, black, white;
    public static Colors colors;
    public Text[] texts;
    public Button pauseBtn, settingsBtn, soundBtn;

    public static Color bricksColor, playerColor, bgColor;

    List<Color32> palete = new List<Color32>();
	// Use this for initialization
	void Awake () {
        colors = this;
        palete.Add(red);
        palete.Add(blue);
        palete.Add(orange);
        palete.Add(yellow);
        palete.Add(green);
        palete.Add(cyan);
        palete.Add(purple);

        string[] t = PlayerPrefs.GetString("playerColor", "0;0;0;1").Split(';');
        playerColor = new Color(float.Parse(t[0]), float.Parse(t[1]), float.Parse(t[2]), float.Parse(t[3]));
        setTexts(playerColor);


        t = PlayerPrefs.GetString("bricksColor", "0;0;0;1").Split(';');
        bricksColor = new Color(float.Parse(t[0]), float.Parse(t[1]), float.Parse(t[2]), float.Parse(t[3]));
        t = PlayerPrefs.GetString("skyColor", "255;255;255;1").Split(';');
        bgColor = new Color(float.Parse(t[0]), float.Parse(t[1]), float.Parse(t[2]), float.Parse(t[3]));
        GameObject.Find("sky").GetComponent<SpriteRenderer>().color = bgColor;
    }
	
	// Update is called once per frame
	void Update () {
        setTexts(playerColor);
	}

    public Color32 getRandomColor()
    {
        int rand = Random.Range(0, palete.Count);
        return palete[rand];
    }
    
    public Color32 getBlack()
    {
        return black;
    }

    public Color32 getWhite()
    {
        return white;
    }

    public void setTexts(Color color)
    {
        foreach (Text txt in texts)
        {
            txt.color = color;
        }
        ColorBlock cb = pauseBtn.colors;
        cb.normalColor = color;
        pauseBtn.colors = cb;
        settingsBtn.colors = cb;
        soundBtn.colors = cb;
    }

}
