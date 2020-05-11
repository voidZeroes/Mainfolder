using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("ShadowBody");
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.GetComponent<MovePlayer>().SetJumpFlg(false);
        }
    }

  

}
