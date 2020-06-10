using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    int supLifeS=20;
    int supLifeM = 50;
    int supLifeL = 100;

    int supMisS = 3;
    int supMisM = 5;
    int supMisL = 10;


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
}
