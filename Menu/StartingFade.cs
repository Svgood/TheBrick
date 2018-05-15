using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartingFade : MonoBehaviour {

    Color tmp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!StateController.showFadeScreen)
            Destroy(this.gameObject);
        if (GetComponent<Image>().color.a > 0)
        {
            tmp = GetComponent<Image>().color;
            tmp.a -= 0.008f;
            GetComponent<Image>().color = tmp;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
