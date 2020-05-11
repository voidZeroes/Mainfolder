using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int type;
     Vector2 dive;
    int counter;
    int hp;
    Rigidbody2D rb2;
    Vector2 target;
    GameObject player;

    bool ceil;


    // Start is called before the first frame update
    void Start()
    {
        dive = new Vector2(0, -1);
        counter = 0;
        hp = 10;
        rb2 = GetComponent<Rigidbody2D>();
        player = GameObject.Find("ShadowBody");
        ceil = true;
    }

    // Update is called once per frame
void Update()
{
        if (counter <= 0)
        {
            switch (type)
            {
                case 0:
                    SearchMove();
                    break;
                case 1:
                    if (ceil == true)
                    {
                        BoundMove();
                    }
                     break;
            }
        }
    

        counter--;
  }


    private void SearchMove()
    {
        target = player.GetComponent<MovePlayer>().GetPlayerPos() - rb2.position;
        rb2.velocity = new Vector2(0, 0);
        rb2.AddForce(target.normalized * 80);
        counter = 50;
    }

    private void BoundMove()
    {
        target = player.GetComponent<MovePlayer>().GetPlayerPos() - rb2.position;
        rb2.velocity = new Vector2(0, 0);
        rb2.AddForce((dive + target.normalized) * 80);
        ceil = false;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
                if(collision.gameObject.tag=="Bullet")
        {
            hp--;
            collision.gameObject.GetComponent<BulletCnt>().AwakeDestroyFlg();
            if(hp<=0)
            {
                Destroy(this.gameObject, 0.5f);
            }
        }

        if (collision.gameObject.tag == "Ground")
        {
            if (type == 1)
            {

                target = player.GetComponent<MovePlayer>().GetPlayerPos() - rb2.position;
                rb2.velocity = new Vector2(0, 0);
                rb2.AddForce((-dive + target.normalized) * 80);
            }
        }

        if (collision.gameObject.tag == "Ceiling")
        {
            if (type == 1)
            {

                ceil = true;
                rb2.velocity = new Vector2(0, 0);
                counter = 100;
            }
        }
    }

}
