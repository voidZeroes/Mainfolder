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

    Vector3 pos;
    Quaternion rotation;
    Vector3 genPos;

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
        
        forLR=true;
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
            rotation = Quaternion.Euler(0, 0,45 );
            genPos = Camera.main.WorldToScreenPoint(bulletGen.transform.localPosition);//弾の発射座標もうごかす
        }

        var bulletTbuf = Input.GetAxis("Mouse ScrollWheel") * 10;

        rateCont--;//射撃間隔

        bulletType +=(int) bulletTbuf;//弾変更　ある数値以上になったら戻すようにしてやればよさそう
        if (bulletType < 1)
        { bulletType = 5; }
        if (bulletType > 5) bulletType = 1;




        if (CalcLR())//前
        {
            scale.x = 1;
            thisScale.y =3;
            forLR = true;
        }
        else if(!CalcLR())//後ろ
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
        if(Input.GetMouseButtonDown(1))
        {
            rota=rotation;

            switch (bulletType)///撃つ弾の変更
            {
                case 0: //けつばん
                    break;

                case 1:
                    if (rateCont <= 0)
                    {
                        instance = (GameObject)Instantiate(bulletPrefab, bulletGen.transform.position, rota);//弾丸

                        rateCont = GetComponent<Rate_of_Fire>().SetWeaponRate(bulletType);
                        
                    }
                    else { break; }
                    break;
                case 2:
                    if (rateCont <= 0)
                    {
                        instance = (GameObject)Instantiate(missilePrefab, bulletGen.transform.position, rota);//味噌
                        instance.GetComponent<BulletCnt>().GetBulletType(bulletType);
                        rateCont = GetComponent<Rate_of_Fire>().SetWeaponRate(bulletType);
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


    private bool CalcLR()
    {
        var check = this.transform.Find("ArmSpinCore").gameObject.transform.Find("BulletGen").gameObject;
        if (this.transform.position.x < check.transform.position.x)
        { return true; }
        else if (this.transform.position.x > check.transform.position.x)
        {
            return false;
        }
        else return true;
    }

    private int CalcRotation(int inportX, int inportY, bool howLR)
    {
        int cal = 0;

        if (inportX > 0 && inportY > 0)
        {
            cal = 45;
        }
        //if (inportX == 0 && inportY == 0)
        //{
        //    cal = 90;
        //}
        if (inportX > 0 && inportY < 0)
        {
            cal = 135;
        }
        if (inportX > 0 && inportY > 0)
        {
            cal = 45;
        }
        return cal;
    }
    public bool SetForwardLR()
    {
        return forLR;
    }
}
