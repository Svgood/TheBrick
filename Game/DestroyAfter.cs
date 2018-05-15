using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {

    public float delay = 16;
    float delay_count = 0;
    public static bool drop = false;
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Colors.bricksColor;
    }
    // Update is called once per frame
    void Update () {
        if (!BrickCreator.br.getPause())
            delay_count += Time.deltaTime;
        if (delay_count > delay)
        {
            Destroy(this.gameObject);
        }
        if (drop)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
        if (Vector3.Distance(Player.player.transform.position, transform.position) > 10 && drop)
            Destroy(this.gameObject);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Land")
            Destroy(this.gameObject);
    }
}
