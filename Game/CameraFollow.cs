using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    // Use this for initialization
    private Vector3 offset;

    bool offsetRecalced = false;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (offsetRecalced)
            transform.position = player.transform.position + offset;
        if (StateController.gameStarted && !offsetRecalced && Player.playerReady)
        {
            recalcOffset();
        }
    }

    void recalcOffset()
    {
        offsetRecalced = true;
        offset = transform.position - player.transform.position;
    }
}
