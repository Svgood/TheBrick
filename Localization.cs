using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour {

    public GameObject resText, setTxt;
    public string player, bricks, sky, chosen, set, tapToContinue;
	// Use this for initialization
	void Start () {

        if (PlayerPrefs.GetString("lang", "none") == "none")
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                    setRussian();
                    break;
                case SystemLanguage.English:
                    setEnglish();
                    break;
                case SystemLanguage.German:
                    setGerman();
                    break;
                case SystemLanguage.Spanish:
                    setSpanish();
                    break;
                case SystemLanguage.ChineseSimplified:
                    setChinS();
                    break;
                case SystemLanguage.ChineseTraditional:
                    setChinT();
                    break;
                case SystemLanguage.French:
                    setFrench();
                    break;

                default:
                    setEnglish();
                    break;
            }
        else
            GameObject.Find("Controller").GetComponent<Settings>().changeLang(PlayerPrefs.GetString("lang"));


	}

	
    public void setText()
    {

        resText.GetComponent<Text>().text = tapToContinue;
        setTxt.GetComponent<Text>().text = set;
    }

	public void setEnglish()
    {
        player = "Player";
        bricks = "Bricks";
        sky = "Sky";
        chosen = "Chosen";
        set = "Set";
        tapToContinue = "Tap to continue";
        setText();
    }

    public void setGerman()
    {
        player = "Spieler";
        bricks = "Ziegel";
        sky = "Himmel";
        chosen = "Gewählt";
        set = "Wählen";
        tapToContinue = "Tippen Sie, um fortzufahren";
        setText();
    }

    public void setRussian()
    {
        player = "Игрок";
        bricks = "Блоки";
        sky = "Небо";
        chosen = "Выбран";
        set = "Выбрать";
        tapToContinue = "Тапните, чтобы продолжить";
        setText();
    }

    public void setSpanish()
    {
        player = "Jugador";
        bricks = "Bloques";
        sky = "Cielo";
        chosen = "Elegido";
        set = "Conjunto";
        tapToContinue = "Pulse para continuar";
        setText();
    }

    public void setFrench()
    {
        player = "Joueur";
        bricks = "Blocs";
        sky = "Ciel";
        chosen = "Choisi";
        set = "Ensemble";
        tapToContinue = "Appuyez pour continuer";
        setText();
    }

    public void setChinS()
    {
        player = "播放机";
        bricks = "块";
        sky = "天空";
        chosen = "选择";
        set = "组";
        tapToContinue = "点击继续";
        setText();
    }

    public void setChinT()
    {
        player = "播放機";
        bricks = "塊";
        sky = "天空";
        chosen = "選擇";
        set = "組";
        tapToContinue = "點擊繼續";
        setText();
    }
}
