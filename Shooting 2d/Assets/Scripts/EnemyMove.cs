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

    float waitTime;
    float wait;
    int spriteLR;
    bool jumpFlg;
    Vector3 thisScaleR;
    Vector3 thisScaleL;

    // Start is called before the first frame update
    void Start()
    {
        dive = new Vector2(0, -1);
        counter = 0;
        hp = 10;
        rb2 = GetComponent<Rigidbody2D>();
        player = GameObject.Find("ShadowBody");
        ceil = true;

        waitTime = 2.5f;
        wait = 0;
        jumpFlg = false;
        spriteLR = 1;//1=右向き　-１左向き
        thisScaleL = this.transform.localScale;
        thisScaleR = thisScaleL;
        thisScaleR.x *= -1;
    }

    // Update is called once per frame
void Update()
{
        ShiftLR();
        if(spriteLR==1)
        {
            this.transform.localScale = thisScaleR;
        }
        else
        {
            this.transform.localScale = thisScaleL;
        }
        

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
        rb2.AddForce((dive + target.normalized) * 200);
        ceil = false;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
                if(collision.gameObject.tag=="Bullet"|| collision.gameObject.tag == "Missile"
            ||collision.gameObject.tag == "Boom")
        {
            hp--;
            if (collision.gameObject.tag != "Boom")//爆発以外(弾直撃)なら
            {
                collision.gameObject.GetComponent<BulletCnt>().AwakeDestroyFlg();//弾消し
            }
                if (hp<=0)
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
                rb2.AddForce((-dive + target.normalized) * 200);
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


    private void ShiftLR()
    {
        wait += Time.deltaTime;

        if (this.transform.position.x > player.transform.position.x)
        {
            if (waitTime <= wait)
            {
                waitTime = Random.Range(0.5f, 2.5f);
                wait = 0;
                jumpFlg = true;

                spriteLR = -1;
            }
        }
        if (this.transform.position.x < player.transform.position.x)
        {
            if (waitTime <= wait)
            {
                waitTime = Random.Range(0.5f, 2.5f);
                wait = 0;
                jumpFlg = true;
                spriteLR = 1;
            }
        }


    }
}
