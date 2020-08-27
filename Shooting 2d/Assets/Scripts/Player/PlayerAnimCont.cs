using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCont : MonoBehaviour
{

    Animator anim;//アニメーター入れ
    bool backMove;
    bool forwardMove;
    int flg;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        backMove = false;
        forwardMove = false;
        flg = 0;
    }

    private void Update()
    {

        if (flg == 0)
        {
            anim.SetBool("ForwardMove", false);//右
            anim.SetBool("BackMove", false);//左

        }

        if (flg == 1)
        {
            anim.SetBool("ForwardMove", true);
            anim.SetBool("BackMove", false);
        }

        if (flg == 2)
        {
            anim.SetBool("ForwardMove", false);
            anim.SetBool("BackMove", true);
        }
    }

    // Update is called once per frame
    //void SetAnim()
    //{
    //}
    public void SetLRFlug(int inport)
    {

        flg = inport;
    }
}
