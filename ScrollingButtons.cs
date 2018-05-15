using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingButtons : MonoBehaviour {

    public GameObject btns;
    float offset;
    float moveValue;
    Scrollbar scroll;

    private void Start()
    {
        scroll = GetComponent<Scrollbar>();
        offset = btns.transform.position.x - transform.position.x;
        moveValue = btns.transform.GetChild(0).transform.localScale.x*4.5f;
        Debug.Log(moveValue);
    }

    public void moveBtns()
    {
        btns.transform.position = new Vector3(transform.position.x + offset - moveValue * scroll.value, btns.transform.position.y, btns.transform.position.z);
    }
}
