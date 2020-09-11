using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulleElace : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            collision.gameObject.GetComponent<BulletCnt>().AwakeDestroyFlg();
        }
        if (collision.gameObject.tag == "Missile")
        {

            collision.gameObject.GetComponent<BulletCnt>().AwakeDestroyFlg();
        }


    }
}
