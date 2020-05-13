using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Rigidbody2D rb2;
    bool hitFlg;
    bool jump;//ジャンプ中か否かの判定
    int lrSelector;//左右判定

    float stop;
    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        hitFlg = false;
        stop = 0;
        lrSelector = 0;
    }

    // Update is called once per frame
    void Update()
    {

        float xMov = Input.GetAxis("Horizontal");
        float yMov = Input.GetAxis("Vertical");

        if(yMov!=0&&!jump)//ジャンプ処理
        {   rb2.velocity = new Vector2(rb2.velocity.x, 5);
            jump = true;
        }


        if (Mathf.Abs(xMov) > 0)//入力中
        {
            if(xMov>0)
            { lrSelector = 1; }
            else if(xMov<0)
            { lrSelector = -1; }
            xMov *= 3;
            if(Mathf.Abs(rb2.velocity.x)>10)//速度10以上の時、10に固定する
            {
                if (rb2.velocity.x > 0)
                {
                    rb2.velocity = new Vector2(10, rb2.velocity.y);
                }
                else if(rb2.velocity.x<0)
                {
                    rb2.velocity = new Vector2(-10, rb2.velocity.y);
                }
            }
        }
        else//入力してないなら止まれ
        {
            //if (rb2.velocity.x < 0)//疑似的な逆方向入力で止める
            //    xMov = 4;
            //}
            //if (rb2.velocity.x > 0)
            //{
            //    xMov = -4;
            //}
            //xMov = 0;

            lrSelector = 0;
            if (jump==false||(hitFlg == true))
            {

            if (rb2.velocity.x < 0)//疑似的な逆方向入力で止める
                {
                    rb2.AddForce(new Vector2(2, 0)); ;
                }
            if (rb2.velocity.x > 0)
                {
                    rb2.AddForce(new Vector2(-20 , 0)); ;
                }

                if (Mathf.Abs(rb2.velocity.x)<1f)//速度が0.1以下なら停止
                {
                    rb2.velocity = new Vector2(0, rb2.velocity.y);
                }
            }
            

        }


        rb2.AddForce(new Vector2(5*lrSelector, 0));
    }

    public Vector2 GetPlayerVel()
    {
        return rb2.velocity;
    }
    public void SetVelY(float y)
    {
        rb2.velocity = new Vector2(rb2.velocity.x, y);
    }
    public float GetPlayerMove()
    {
        return Mathf.Abs(rb2.velocity.x);
    }
    public Vector2 GetPlayerPos()
    {
        return rb2.position;
    }
    public void SetHitFlg(bool flg)
    {
        hitFlg = flg;   
    }

    public void SetJumpFlg(bool flg)
    {
       jump = flg;
    }
}
