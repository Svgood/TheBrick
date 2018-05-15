using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEndGame : MonoBehaviour {

    Color32 tmp;
    byte fadeSpeed = 3;
    public Text txt;
    public static bool startFading = false;
    public static bool fade = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (startFading)
        {
            txt.enabled = true;

            tmp = txt.color;
            tmp.a += fadeSpeed;
            if (tmp.a + fadeSpeed >= 255)
            {
                tmp.a = 255;
                startFading = false;
                return;
            }
            txt.color = tmp;
        }
        if (fade)
        {
            startFading = false;
            tmp = txt.color;
            tmp.a -= fadeSpeed;
            if (tmp.a - fadeSpeed <= 0)
            {
                tmp.a = 0;
                fade = false;
                txt.enabled = false;
                return;
            }
            txt.color = tmp;
        }
	}


}
