using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    bool initFlg;


    public int type;
    Vector2 dive;
    int counter;
    int hp;
    Rigidbody2D rb2;
    Vector2 target;
    GameObject player;


    static int flogHP = 20;
    float flogJumpWaitTime;
    float flogJumpWaitCnt;
    public Sprite flogJumpSprite;
    public Sprite flogLandingSprite;

    bool ceil;


    float spriteWaitTime;
    float spriteWaitCnt;
    int spriteLR;
    bool jumpFlg;
    Vector3 thisScaleR;
    Vector3 thisScaleL;

    // Start is called before the first frame update
    void Start()
    {
        initFlg = false;

        dive = new Vector2(0, -1);
        counter = 0;
        hp = 10;
        rb2 = GetComponent<Rigidbody2D>();
        player = GameObject.Find("ShadowBody");
        ceil = true;

        jumpFlg = false;
        flogJumpWaitTime = 3.8f;
        flogJumpWaitCnt = 0;


        spriteWaitTime = 2.5f;
        spriteWaitCnt = 0;
        jumpFlg = false;
        spriteLR = 1;//1=右向き　-１左向き
        thisScaleL = this.transform.localScale;
        thisScaleR = thisScaleL;
        thisScaleR.x *= -1;

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        ShiftLR();
        if (spriteLR == 1)
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
                case 2:
                    FlogMove();

                    break;
            }
        }


        counter--;
        SpriteShifter();
    }


    private void SearchMove()//単純な追尾
    {
        target = player.GetComponent<MovePlayer>().GetPlayerPos() - rb2.position;
        rb2.velocity = new Vector2(0, 0);
        rb2.AddForce(target.normalized * 80);
        counter = 50;
    }

    private void BoundMove()//上空から襲い掛かる動き
    {
        target = player.GetComponent<MovePlayer>().GetPlayerPos() - rb2.position;
        rb2.velocity = new Vector2(0, 0);
        rb2.AddForce((target.normalized) * 200);
        ceil = false;

    }

    private void FlogMove()//カエルが跳ねる動き
    {
        //target = player.GetComponent<MovePlayer>().GetPlayerPos() - rb2.position;
        Vector3 scale = new Vector3(1, 1, 1);
        if (player.transform.position.x < this.transform.position.x)
        {
            if (!jumpFlg && flogJumpWaitCnt > flogJumpWaitTime)
            {
                scale.x = -1;
                rb2.AddForce(new Vector2(120 * scale.x, 300));
                jumpFlg = true;
            }
            flogJumpWaitCnt += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Missile"
    || collision.gameObject.tag == "Boom")
        {
            hp--;
            if (collision.gameObject.tag != "Boom")//爆発以外(弾直撃)なら
            {
                collision.gameObject.GetComponent<BulletCnt>().AwakeDestroyFlg();//弾消し
            }
            if (hp <= 0)
            {
                Destroy(this.gameObject, 0.5f);
            }
        }

        if (collision.gameObject.tag == "Ground")
        {
            if (type == 1)//地面接触時に跳ね返る動き
            {
                //target = player.GetComponent<MovePlayer>().GetPlayerPos() - rb2.position;
                rb2.velocity = new Vector2(0, 0);//静止
                target.y *= -1;//TargetのYを反転

                if (30 > Vector2.Angle(this.transform.up, target) &&
                    -30 < Vector2.Angle(this.transform.up, target))//角度が30度以内ならそのまま上へ
                {
                    rb2.AddForce((target.normalized) * 200);
                }
                else//そうでないときは真上へ
                {
                    rb2.AddForce((Vector3.up) * 200);
                }
            }
            if (type == 2)//カエル用の静止コマンド
            {
                rb2.velocity.Scale(new Vector2(0, 0));
                jumpFlg = false;
                flogJumpWaitCnt = 0;
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
        spriteWaitCnt += Time.deltaTime;

        if (this.transform.position.x > player.transform.position.x)
        {
            if (spriteWaitTime <= spriteWaitCnt)
            {
                spriteWaitTime = Random.Range(0.5f, 2.5f);
                spriteWaitCnt = 0;

                spriteLR = -1;
            }
        }
        if (this.transform.position.x < player.transform.position.x)
        {
            if (spriteWaitTime <= spriteWaitCnt)
            {
                spriteWaitTime = Random.Range(0.5f, 2.5f);
                spriteWaitCnt = 0;
                spriteLR = 1;
            }
        }
    }

    void SpriteShifter()
    {
        if (type == 2)
        {
            if (jumpFlg)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = flogJumpSprite;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = flogLandingSprite;
            }
        }
    }


    void Init()
    {
     if(type==2)
        {
            hp = flogHP;
        }
    }
    
}
