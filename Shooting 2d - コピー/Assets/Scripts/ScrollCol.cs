using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCol : MonoBehaviour
{
    bool scrFlg;
    bool scrR;
    bool scrL;
    // Start is called before the first frame update
    void Start()
    {
        scrFlg = false;
        scrR = scrL = scrFlg;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            scrFlg = true;
            Debug.Log("当たったで");


        }
    }
    public int GetScrFlg()
    {
        if (scrFlg && this.name == "scrollR")
        {

            return 1;
        }
        else if (scrFlg && this.name == "scrollL")
        {

            return -1;
        }

        return 0;
    }

    public void SetScrFlg(bool flg)
    {
        scrFlg = flg;
    }
}
