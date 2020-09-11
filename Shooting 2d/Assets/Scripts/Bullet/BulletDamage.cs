using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{

    static int beamDamage=3;
    static int missleDamage = 10;

    public int GetDamage(string name)
    {

        if (name == "Bullet")
        {
            return beamDamage;
        }
        if (name == "Missile")
        {
            return missleDamage;
        }
        return 0;
    }
}
