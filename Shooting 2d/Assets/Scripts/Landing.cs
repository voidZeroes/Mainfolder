using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
          this.gameObject.GetComponent<MovePlayer>().SetJumpFlg(false);
        }
    }

  

}
