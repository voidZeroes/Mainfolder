using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rate_of_Fire : MonoBehaviour
{
    int[] rateList = new int[100];
    // Start is called before the first frame update
    void Start()
    {
        rateList[0] = 1;//デフォ
        rateList[0] = 10;//ノーマルショット
        rateList[0] = 50;//味噌
    }

    // Update is called once per frame
public int SetWeaponRate(int type)
    {
        return rateList[type];
    }
}
