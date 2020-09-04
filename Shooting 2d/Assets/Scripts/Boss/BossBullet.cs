using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb2;
    Vector2 move;
    GameObject Boss;
    public GameObject Boom;
    float life;

    void Start()
    {
        rb2 = this.GetComponent<Rigidbody2D>();
        Boss = GameObject.Find("CannonBase/SpinCannon/Cannon");
        move=this.transform.position - Boss.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2.velocity.magnitude < 10)
        {
            rb2.AddForce(move);
        }
    }
    private void SetForce(Vector2 force)
    {
        rb2.AddForce(force.normalized*100);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BoomGen();
        }
    }

    void BoomGen()
    {
        GameObject instance = (GameObject)Instantiate(Boom, this.transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
