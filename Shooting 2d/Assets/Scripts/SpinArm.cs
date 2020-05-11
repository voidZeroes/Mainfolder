using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinArm : MonoBehaviour
{

    Vector2 bulletVec;
    GameObject cannon;
    GameObject bulletGen;//弾の出現場所
    GameObject bulletPrefab;//弾prefab
    GameObject instance;
    Quaternion rota;//回転量
    private int bulletType= 1;//現在利用可能な弾。
    public float wheelSpinTest;


    void Start()
    {
        cannon = GameObject.Find("ArmSpinCore");
        bulletGen = GameObject.Find("BulletGen");
        bulletPrefab = (GameObject)Resources.Load ("Prefab/Bullet");
        // ;
        wheelSpinTest = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale=this.transform.GetChild(1).localScale;//ボディ向き変更
        Vector3 thisScale = this.transform.GetChild(0).GetChild(0).localScale;//アームキャノン向き変更
        var pos = Camera.main.WorldToScreenPoint(transform.localPosition);//ローカル座標を画面座標へ変換
        var rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos);//回転量求める
        var genPos = Camera.main.WorldToScreenPoint(bulletGen.transform.localPosition);//弾の発射座標もうごかす

        var bulletTbuf = Input.GetAxis("Mouse ScrollWheel") * 10;
        bulletType +=(int) bulletTbuf;//弾変更　ある数値以上になったら戻すようにしてやればよさそう
        if (bulletType < 1)
        { bulletType = 5; }
        if (bulletType > 5) bulletType = 1;
        //Rotationｙ0以上以下でBody+αの方向変更
        if (rotation.z<0)
        {
            scale.x = 1;
            thisScale.y =3;
        }
        else if(rotation.z>0)
        {
            scale.x = -1;
            thisScale.y = -3;
        }
        this.transform.GetChild(1).localScale = scale;//変更
        this.transform.GetChild(0).GetChild(0).localScale = thisScale;//反映;
            
        cannon.transform.localRotation = rotation;//回転ッ！
        bulletVec = genPos -pos;//弾の発射方向
        if(Input.GetMouseButtonDown(1))
        {
            rota=rotation;
            switch (bulletType)
            {
                case 0: //けつばん
                    break;

                case 1:
                    instance = (GameObject)Instantiate(bulletPrefab, bulletGen.transform.position, rota);//弾丸
                    break;
                case 2:
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


}
