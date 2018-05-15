using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseBtnScript : MonoBehaviour {

    public GameObject child;
    public int cost = 50;

    private void Start()
    {
        if (cost != 0)
        {
            child = transform.GetChild(0).gameObject;
        }

        if (child != null)
        {
            cost = int.Parse(child.transform.GetChild(0).GetComponent<Text>().text);
            if (cost == 0)
                child.SetActive(false);
        }
    }
}
