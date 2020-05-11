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
    int bulletType;
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
       // move =move.normalized;
        
    }


    // Update is called once per frame
    void Update()
    {


        //this.transform.rotation = spin;
        if (bulletAwake)
        {
            rb2.AddForce(move.normalized*80);
            bulletAwake = false;
        }  
        
        //transform.position += new Vector3(move.x/10, move.y/10, 0);
 
        if (!render.isVisible||flg==true)//画面外に行ったとき
        {
            Destroy(this.gameObject);//ｼﾈｪ!
        }
    }

    public void AwakeDestroyFlg()
    {
        flg = true;
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{


    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("敵と接触した！"); Destroy(gameObject);//ｼﾈｪ!
    //    }
    //}

}
