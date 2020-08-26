using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Rigidbody2D rb2;
    bool hitFlg;//ダメージ
    bool jump;//ジャンプ中か否かの判定
    int lrSelector;//左右判定
    static float playerSpeed=5;

    Vector3 enemyLocal;
    Vector2 knockBackVec;
    float invincible;//無敵時間
    int inviTime;//無敵時間のデフォ
    public float inviView;//デバッグ用カウンタ
    GameObject bodyImage;//shadowBodyの仕舞いどころさん
    GameObject armImg;//アームキャノンの仕舞いどころさん

    
    bool mouseMode;//コントロール方法の変更　false:パッド　true:マウスモード


    float life;//現在体力
    float maxLife;//最大体力


    int missile;
    int missileMax;


    float xMov;
    float yMov;
    public float rsX;
    public float rsY;
    float stop;
    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        hitFlg = false;
        stop = 0;
        lrSelector = 0;
        invincible = 0;
        inviTime = 1;
        inviView = 0;
        maxLife = 1990;
        life = 390;
        mouseMode = false;

        xMov=0;
        yMov=0;
        rsX = 0;
        rsY = 0;

        bodyImage = transform.Find("BodyImage").gameObject;
        armImg = transform.Find("ArmSpinCore").gameObject.transform.Find("ShadowArm").gameObject;

        missileMax = 30;
        missile = missileMax;

    }

    // Update is called once per frame
    void Update()
    {


        invincible-=Time.deltaTime;//無敵時間減算
        inviView = invincible;

        if(invincible>0)//被弾時点滅
        {
            var level = Mathf.Abs(Mathf.Sin(Time.time * 60));//乗算値は時間内の点滅数
            bodyImage.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,level);
            armImg.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);

        }
        else
        {

            bodyImage.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            armImg.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }


        if (!mouseMode)
        {
            xMov = Input.GetAxis("Horizontal");
            yMov = Input.GetAxis("Vertical");
            rsX = Input.GetAxis("Horizontal2");
            rsY = Input.GetAxis("Vertical2");
        }

        Debug.Log(xMov);

        //if(hitFlg)
        //{
        //    rb2.velocity = new Vector2(0, 0);
        //    rb2.velocity = knockBackVec;
        //    hitFlg = false;
        //}

        KnockBack();//ノックバック




        MovingUnit();//移動系

        //移動。
        rb2.AddForce(new Vector2(playerSpeed*lrSelector, 0));

        this.GetComponent<SpinArm>().GetInport(lrSelector);
        
        if (mouseMode)//マウスの時だけこれを呼ぶ
        {
            SetAnimMouse(lrSelector, GetComponent<SpinArm>().SetForwardLR());//モーション設定(マウス)
        }

        this.GetComponent<SpinArm>().CalcRotation(rsX,rsY);
        SetAnimPad();
        //   this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -50.22003f);
        xMov = yMov = rsX = rsY =0;

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            hitFlg = true;
            enemyLocal = transform.InverseTransformPoint(collision.transform.position);
            knockBackVec = new Vector2(this.transform.position.x - enemyLocal.x, this.transform.position.y - enemyLocal.y- 0.3f);//ノックバック方向だと思う
        }

        if (collision.gameObject.tag == "Ground")
        {
                SetJumpFlg(false);
        }



    }


    private void SetAnimMouse(int inport,bool inportFlg)
    {
        if (inportFlg)//モーション設定のところ。なんでかぬるぽなう
        {
            if (inport > 0)
            {
                bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(1);
            }
            else
            if (inport < 0)
            {
                bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(2);
            }
            else { bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(0); }
        }
        if (!inportFlg)
        {
            if (inport > 0)
            {
                bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(2);
            }
            else
            if (inport < 0)
            {
                bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(1);
            }
            else { bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(0); }
        }

    }

    private void MovingUnit()
    {
        if (yMov != 0 && !jump)//ジャンプ処理　ボタン式にするべきだろう。今のままだと誤爆する
        { rb2.velocity = new Vector2(rb2.velocity.x, 10);
            jump = true;
        }


        if (Mathf.Abs(xMov) > 0)//入力中
        {
            if (xMov > 0.8)
            {
                lrSelector = 1;
            }
            else if (xMov < -0.8)
            {
                lrSelector = -1;
            }
            xMov *= 3;
            if (Mathf.Abs(rb2.velocity.x) > 10)//速度10以上の時、10に固定する
            {
                if (rb2.velocity.x > 0)
                {
                    rb2.velocity = new Vector2(10, rb2.velocity.y);
                }
                else if (rb2.velocity.x < 0)
                {
                    rb2.velocity = new Vector2(-10, rb2.velocity.y);
                }
            }
        }
        else//入力してないなら止まれ
        {

            lrSelector = 0;
            if (jump == false || (hitFlg == true))
            {
//疑似逆入力のつもりだったが、PhysisMaterialの摩擦で止めることに。
                if (Mathf.Abs(rb2.velocity.x) < 0.5f)//速度が0.1以下なら停止
                {
                    rb2.velocity = new Vector2(0, rb2.velocity.y);
                }
            }

        }
    }

        private void SetAnimPad()
    {

        if (GetComponent<SpinArm>().SetForwardLR())//右向きなら
        {
            if (xMov == 0)//静止
            {
                bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(0);
            }
            if (xMov < 0)//左移動
            {
                bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(2);//後ろ走り
            }
            if (xMov > 0)//右移動
            {
                bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(1);//走り
            }
        }
        else if(!GetComponent<SpinArm>().SetForwardLR())//左向きなら
        {
            if (xMov == 0)
            {
                bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(0);
            }
            if (xMov < 0)//左移動
            {
                bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(1);
            }
            if (xMov > 0)//右移動
            {
                bodyImage.GetComponent<PlayerAnimCont>().SetLRFlug(2);
            }
        }

    }

    private void KnockBack()
    {

        if (hitFlg)//やっぱVelocityでやります
        {
            if (invincible <= 0)//カウントゼロ後にぶっ飛ぶ処理とライフ減少
            {
                life -= 15;//仮の減少値　そのうち敵ごとに固有の設定をしたい
                rb2.velocity = new Vector2(0, 0);

                Vector3 pos;
                pos = new Vector3(0, 0, 0);
                if (knockBackVec.x < 0)//←//Y軸を固定方向への吹き飛びに再定義
                {
                    pos.x = rb2.transform.position.x - 0.8f;
                    pos.y = - 0.4f;

                }
                else if (knockBackVec.x > 0)//→
                {
                    pos.x = rb2.transform.position.x + 0.8f;
                    pos.y =-0.4f;
                }

                var knockBackVector= (pos - rb2.transform.position);
                for (int i = 0; i < 2; i++)
                {
                    rb2.velocity = rb2.velocity + (knockBackVec.normalized * 3);
                }
                

                invincible = inviTime;
            }
            hitFlg = false;

        }
    }

        public bool SetMouseMode()
    {
        return mouseMode;
    }

    public int SetPlayerLife()
    {
        return (int)life;
    }
    public int SetPlayerMaxLife()
    {
        return (int)maxLife;
    }

    public int SetMissileAmmo()
    {
        return missile;
    }

    public void MissileAmmoMinus(int minus)
    {
        missile-=minus;
    }
    public void MissileAmmoPlus(int plus)
    {
        missile += plus;

    }
}
