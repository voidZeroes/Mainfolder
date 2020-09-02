using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShooter : MonoBehaviour
{
    public GameObject[] ShootUnit;
    public GameObject Missle;
    float reloadTime;
    float shotSpan;
    bool []shotFlag;
    // Start is called before the first frame update
    void Start()
    {
        reloadTime = 6;
        shotSpan = 0.5f;
        shotFlag = new bool[ShootUnit.Length];

        for(int i=0;i<ShootUnit.Length;i++)
        {
            shotFlag[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(reloadTime<=0)
        {
            if (shotSpan <= 0)
            {
                for (int i = 0; i < ShootUnit.Length; i++)
                {
                    if (shotFlag[i] == false)
                    {
                        GameObject missile = (GameObject)Instantiate(Missle, ShootUnit[i].transform.position, Quaternion.identity);
                        shotFlag[i] = true;
                        shotSpan = 0.5f;
                        break;
                    }
                }
            }
            shotSpan -= Time.deltaTime;

            if(shotFlag[ShootUnit.Length-1]==true)
            {
                reloadTime = 6;

                for (int i = 0; i < ShootUnit.Length; i++)
                {
                    shotFlag[i] = false;
                }
            }
        }
        reloadTime -= Time.deltaTime;
    }
}
