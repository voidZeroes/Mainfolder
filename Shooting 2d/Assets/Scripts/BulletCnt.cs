using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCnt : MonoBehaviour
{
    Vector2 move;
    GameObject obj;//弾を打った本人
    Renderer render;
    Rigidbody2D rb2;
    bool bulletAwake;
    bool flg;//生存フラグ
    int bulletType;//弾のタイプ
    int bulletlife;//弾の残存時間
    GameObject boomPrefab;//爆発
    GameObject instance;//爆発の召喚位置だと思う。そう理解してる



    /*  Vector3 back;
    Vector3 forward;
    */
    void Start()
    {
        /*forward =Camera.main.WorldToScreenPoint(GameObject.Find("BulletF").transform.localPosition);
        back = Camera.main.WorldToScreenPoint(GameObject.Find("BulletB").transform.localPosition);
        move = forward - back;*/

        obj = GameObject.Find("ShadowBody");
        rb2 = GetComponent<Rigidbody2D>();
        move = this.transform.position - obj.transform.position;
        render=FindObjectOfType<Renderer>();
        flg = false;
        bulletAwake = true;
        boomPrefab = (GameObject)Resources.Load("Prefab/Boom!");

        bulletlife = 500;

       // move =move.normalized;
        
    }


    // Update is called once per frame
    void Update()
    {
        bulletlife--;
        if(bulletlife<=0)
        {
            AwakeDestroyFlg();
        }

        //this.transform.rotation = spin;
        if (bulletAwake)
        {
            rb2.AddForce(move.normalized*80);
            bulletAwake = false;
        }  
        
        //transform.position += new Vector3(move.x/10, move.y/10, 0);
 
        if (!render.isVisible||flg)//画面外に行ったときor何かしらにあたったとき
        {
            if(flg&&bulletType==2)//敵に当たってなおかつミサイル？よし爆ぜろ
            {
                instance= (GameObject)Instantiate(boomPrefab, this.transform.position, this.transform.rotation);
            }
            Destroy(this.gameObject);//ｼﾈｪ!
        }
    }

    public void AwakeDestroyFlg()
    {
        flg = true;
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    public void GetBulletType(int type)//発射時に弾のタイプを取得
    {
        bulletType = type;
    }

    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("敵と接触した！"); Destroy(gameObject);//ｼﾈｪ!
    //    }
    //}

}
