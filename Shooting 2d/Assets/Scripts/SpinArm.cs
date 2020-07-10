using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinArm : MonoBehaviour
{

    Vector2 bulletVec;
    GameObject cannon;
    GameObject bulletGen;//弾の出現場所
    GameObject bulletPrefab;//弾prefab
    GameObject missilePrefab;//ミサイルプレファブ
    GameObject instance;
    Quaternion rota;//回転量
    private int bulletType= 1;//現在利用可能な弾。
    public float wheelSpinTest;
    int rateCont;//射撃レート管理
    bool lrCheck;//専用のヤツから入ったのを制御する
    bool forLR;

    bool newLR;//今どっちに進んでたか
    bool oldLR;
    int moveCheck;//今動いてるか


    Vector3 pos;
    Quaternion rotation;
    Vector3 genPos;
    float radian;//パッド操作で腕が向く方向

    public float rotaCheck;


    void Start()
    {
        cannon = GameObject.Find("ArmSpinCore");
        bulletGen = GameObject.Find("BulletGen");
        bulletPrefab = (GameObject)Resources.Load ("Prefab/Bullet");
        missilePrefab = (GameObject)Resources.Load("Prefab/Missile");
        // ;
        wheelSpinTest = 0;
        rateCont = 0;
        rotaCheck = 0;
        radian = 90;

        forLR=true;
        oldLR = true;
        newLR =true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale=this.transform.GetChild(1).localScale;//ボディ向き変更
        Vector3 thisScale = this.transform.GetChild(0).GetChild(0).localScale;//アームキャノン向き変更

        if (this.transform.root.GetComponent<MovePlayer>().SetMouseMode())
        {
            pos = Camera.main.WorldToScreenPoint(transform.localPosition);//ローカル座標を画面座標へ変換
            rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos);//回転量求める
            genPos = Camera.main.WorldToScreenPoint(bulletGen.transform.localPosition);//弾の発射座標もうごかす
        }
        if(!this.transform.root.GetComponent<MovePlayer>().SetMouseMode())
        {
            
            pos = Camera.main.WorldToScreenPoint(transform.localPosition);//ローカル座標を画面座標へ変換
            rotation = Quaternion.Euler(0, 0,radian );
            genPos = Camera.main.WorldToScreenPoint(bulletGen.transform.localPosition);//弾の発射座標もうごかす
        }

        //var bulletTbuf = Input.GetAxis("Mouse ScrollWheel") * 10;
        var bulletTbuf = 0;
        //弾の変更
        if(Input.GetButtonDown("BulletChangePlus"))
        {
            bulletTbuf++;
        }
        if (Input.GetButtonDown("BulletChangeMinus"))
        {
            bulletTbuf--;
        }

        rateCont--;//射撃間隔

        bulletType +=(int) bulletTbuf;//弾変更　ある数値以上になったら戻すようにしてやればよさそう
        if (bulletType < 1)
        { bulletType = 5; }
        if (bulletType > 5) bulletType = 1;




        //if (CalcLRMouse())//前
        //{
        //    scale.x = 1;
        //    thisScale.y =3;
        //    forLR = true;
        //}
        //else if(!CalcLRMouse())//後ろ
        //{
        //    scale.x = -1;
        //    thisScale.y = -3;
        //    forLR = false;
        //}

        //見た目の回転
        if (CalcLRPad())//前
        {
            scale.x = 1;
            thisScale.y = 3;
            forLR = true;
        }
        else if (!CalcLRPad())//後ろ
        {
            scale.x = -1;
            thisScale.y = -3;
            forLR = false;
        }


        this.transform.GetChild(1).localScale = scale;//変更
        this.transform.GetChild(0).GetChild(0).localScale = thisScale;//反映;
        rotaCheck = rotation.z;
        
        cannon.transform.localRotation = rotation;//回転ッ！
        bulletVec = genPos -pos;//弾の発射方向
        if(FireFlg())//ほっしゃん！
        {
            rota=rotation;

            switch (bulletType)///撃つ弾の変更
            {
                case 0: //けつばん
                    break;
                    
                case 1:
                    if (rateCont <= 0)//ノーマルビーム
                    {
                        instance = (GameObject)Instantiate(bulletPrefab, bulletGen.transform.position, rota);//弾丸

                        rateCont = GetComponent<Rate_of_Fire>().SetWeaponRate(bulletType);
                        
                    }
                    else { break; }
                    break;
                case 2://味噌
                    if (rateCont <= 0)
                    {
                        if (GetComponent<MovePlayer>().SetMissileAmmo() > 0)
                        {
                            instance = (GameObject)Instantiate(missilePrefab, bulletGen.transform.position, rota);//味噌
                            instance.GetComponent<BulletCnt>().GetBulletType(bulletType);
                            rateCont = GetComponent<Rate_of_Fire>().SetWeaponRate(bulletType);
                            GetComponent<MovePlayer>().MissileAmmoMinus(1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else { break; }

                    break;
        }
            
        }




        wheelSpinTest = bulletType;
    }

   public Vector2 GetGenPos()
    {

        return bulletGen.transform.position;
    }
    public Vector2 GetBulletVector()
    {

        return bulletVec;
    }
    public Quaternion GetQuaternion()
    {
        return rota;
    }


    private bool CalcLRMouse()//左右反転するやつ
    {
        bool flg=true;
        var check = this.transform.Find("ArmSpinCore").gameObject.transform.Find("BulletGen").gameObject;
        if (this.transform.position.x < check.transform.position.x)
        { flg= true; }
        else if (this.transform.position.x > check.transform.position.x)
        {
            flg= false;
        }

        return flg;
    }

    //public void CalcRotation(float inportX, float inportY)//スパメト的AIMをできるようにするもの……だったんだけど正直スティックでやる動きじゃなかった。ぐりぐり動かせるようにする予定
    //{
    //    int cal = radian;

    //    //前
    //    if (moveCheck == 0 || moveCheck > 0&&newLR)
    //    {
    //        if (inportX > 0 && inportY > 0)
    //        {
    //            cal = 225;
    //        }
    //        if (inportX > 0 && inportY == 0)
    //        {
    //            cal = 270;
    //        }
    //        if (inportX > 0 && inportY < 0)
    //        {
    //            cal = 315;
    //        }
    //    }

    //    //後ろ
    //    if (moveCheck == 0 || moveCheck < 0&&!newLR)
    //    {
    //        if (inportX < 0 && inportY < 0)
    //        {
    //            cal = 45;
    //        }
    //        if (inportX < 0 && inportY == 0)
    //        {
    //            cal = 90;
    //        }
    //        if (inportX < 0 && inportY > 0)
    //        {
    //            cal = 135;
    //        }
    //    }
    //    if (inportX == 0 && inportY> 0)
    //    {
    //        cal =180;
    //    }
    //    if (inportX == 0 && inportY < 0)
    //    {
    //        cal = 0;
    //    }

    //    if (inportX == 0 && inportY == 0)
    //    {
    //        if (newLR)
    //        {
    //            cal = 270;
    //        }
    //        else
    //        {
    //            cal = 90;
    //        }
    //    }

    //    radian = cal;
    //}
    public void CalcRotation(float inportX, float inportY)//滑らかエイム。テメェ簡単な割に時間かけすぎィ！
    {
        float cal = radian;

        Vector3 cont = new Vector3(-inportX, -inportY,0);

        

        Debug.Log(cont.x);
        // Debug.Log(inportY);

        if (cont.x > 0)
        {
            cal = Vector3.Angle(transform.up, cont);//これで半分
        }else if(cont.x<0)
        { cal = Vector3.Angle(transform.up, -cont)+180; }
        
        if (cal != 0)
        {
            radian = cal;
        }
    }

        private bool CalcLRPad()//左右反転するやつ
    {
        bool flg = true;//どっち向きにするか　True＝前

        
        var check = this.transform.Find("ArmSpinCore").gameObject.transform.Find("BulletGen").gameObject;

        if (moveCheck < 0)
        {
            newLR = false;
            flg = false;
        }
        else if (moveCheck == 0)
        {
            newLR = oldLR;
        }
        else if (moveCheck > 0)
        {
            newLR = true;
            flg = true;
        }


        if (this.transform.position.x < check.transform.position.x)
        {
            if (moveCheck == 0)
                flg = true;
        }

        if (this.transform.position.x > check.transform.position.x)
        {
            if ( moveCheck == 0)
                flg = false;
        }

        oldLR = newLR;
        return flg;
    }


   public void GetInport(int lr)
    {

        moveCheck = lr;

    }
    
    public bool SetForwardLR()
    {
        return forLR;
    }

private bool FireFlg()
    {
        bool flg = false;
        if(this.transform.root.GetComponent<MovePlayer>().SetMouseMode())
        {
            flg = Input.GetMouseButton(1);
        }
        else
        {
            flg = Input.GetButtonDown("Fire1");
        }
        return flg;
    }

}
