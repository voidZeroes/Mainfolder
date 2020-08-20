using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    const int supLifeS=20;
    const int supLifeM = 50;
    const int supLifeL = 100;

    const int supMisS = 3;
    const int supMisM = 5;
    const int supMisL = 10;

    public GameObject player;




    public int GetSupply(int type/*0=ライフ1=味噌*/, int size/*0~2*/)
    {
        if (type == 0)
        { switch (size)
            {
                case 0:
                    return supLifeS;
                case 1:
                    return supLifeM;
                case 2:
                    return supLifeL;
            }
        }
        if (type == 1)
        {
            switch (size)
            {
                case 0:
                    return supMisS;
                case 1:
                    return supMisM;
                case 2:
                    return supMisL;

            }
        }

        return 0;
            
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (this.gameObject.tag == "SupplyM")
            {
                int num = GetSupply(1, 1);
                player.GetComponent<MovePlayer>().MissileAmmoPlus(num);
                Destroy(this.gameObject);
            }

        }

    }

}
