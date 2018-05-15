using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChecker : MonoBehaviour {

    public static bool mouseChek = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        mouseChek = true;
    }

    private void OnMouseExit()
    {
        mouseChek = false;
    }
}
