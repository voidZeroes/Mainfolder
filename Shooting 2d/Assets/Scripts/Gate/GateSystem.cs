using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSystem : MonoBehaviour
{
    Animator gateAnim;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gateAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Mathf.Abs(this.transform.position.x - player.transform.position.x));//距離計測
        if (Mathf.Abs(this.transform.position.x - player.transform.position.x)>13)//遠くなったらゲート閉鎖
        {
            gateAnim.SetBool("HowOpen", false);//閉鎖アニメーション再生
            this.transform.GetChild(0).GetComponent<BoxCollider2D>().gameObject.SetActive(true);//コリジョン有効か
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == "BlueGate")//最弱ゲート
        {
            if (collision.tag == "Bullet")//弾が当たったら
              {

                gateAnim.SetBool("HowOpen", true);//アニメ再生
                this.transform.GetChild(0).GetComponent<BoxCollider2D>().gameObject.SetActive(false);//解放。
            }

        }
    }

}
