using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSystem : MonoBehaviour
{
    Animator gateAnim;
    GameObject player;
    AudioSource gateSE;
    // Start is called before the first frame update
    void Start()
    {
        gateAnim = this.GetComponent<Animator>();
        player = GameObject.Find("ShadowBody");
        gateSE = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
                gateSE.Play();
                gateAnim.SetBool("HowOpen", true);//アニメ再生
                this.transform.GetChild(0).GetComponent<BoxCollider2D>().gameObject.SetActive(false);//解放。
            }

        }
    }

}
