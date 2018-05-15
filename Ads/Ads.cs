using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour {

    public GameObject rew;

    private void handleResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                rew.GetComponent<RewardWindowScript>().startReward();
                AdsController.gameEnded = 0;
                
       
                Debug.Log("Finished");
                break;
            case ShowResult.Failed:
                Debug.Log("Failed");
                break;
            case ShowResult.Skipped:
                Debug.Log("Skipped");
                break;
        }
    }

    public void showAd()
    {
        if (Advertisement.IsReady())
        {
            Player.player.adWindow.GetComponent<AdButtonMove>().moveOut();
            Advertisement.Show("rewardedVideo", new ShowOptions()
            {
                resultCallback = handleResult
            });
        }
    }

    public void showSmallAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
    }

    
}
