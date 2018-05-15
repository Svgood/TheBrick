using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    bool soundsOn = true;
    public GameObject settings;
    public AudioSource[] sources;
    Image soundImg;
    public Sprite img1, img2;
    float[] sourcesVol;
	// Use this for initialization
	void Start () {
        soundImg = GameObject.Find("sounds").GetComponent<Image>();
        sourcesVol = new float[sources.Length];
        for (int i = 0; i < sources.Length; i++)
        {
            sourcesVol[i] = sources[i].volume;
        }

        if (PlayerPrefs.GetInt("soundOn", 1) == 1)
            soundsOn = false;
        else
            soundsOn = true;
        turnVolume();


        
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void turnSettings()
    {
        if (settings.active)
            settings.SetActive(false);
        else
            settings.SetActive(true);
    }

    public void turnVolume()
    {
        if (soundsOn)
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].volume = 0;
            }
            soundsOn = false;
            soundImg.sprite = img2;
            PlayerPrefs.SetInt("soundOn", 0);
        }
        else
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].volume = sourcesVol[i];
            }
            soundsOn = true;
            soundImg.sprite = img1;
            PlayerPrefs.SetInt("soundOn", 1);
        }
    }

    public void changeLang()
    {
        GameObject obj = GameObject.Find("Dropdown").transform.GetChild(0).gameObject;
        Localization local = GameObject.Find("Localization").GetComponent<Localization>();
        switch (obj.GetComponent<Text>().text)
        {
            case "English":
                local.setEnglish();
                break;
            case "Deutsch":
                local.setGerman();
                break;
            case "Русский":
                local.setRussian();
                break;
            case "Español":
                local.setSpanish();
                break;
            case "Français":
                local.setFrench();
                break;
            case "中文":
                local.setChinS();
                break;

        }
        PlayerPrefs.SetString("lang", obj.GetComponent<Text>().text);
    }

    public void changeLang(string langName)
    {
        Localization local = GameObject.Find("Localization").GetComponent<Localization>();
        switch (langName)
        {
            case "English":
                local.setEnglish();
                break;
            case "Deutsch":
                local.setGerman();
                break;
            case "Русский":
                local.setRussian();
                break;
            case"Français":
                local.setFrench();
                break;
            case "中文":
                local.setChinS();
                break;
            case "Español":
                local.setSpanish();
                break;
            default:
                local.setEnglish();
                break;

        }
    }
}
