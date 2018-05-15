using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsController : MonoBehaviour {


    public static int gameEnded = 0;
    public static int timeBeforeBonus = 2;

	// Update is called once per frame
	void Update () {
		if (gameEnded > 6)
        {
            gameEnded = 0;
            GetComponent<Ads>().showSmallAd();
        }
	}
}
