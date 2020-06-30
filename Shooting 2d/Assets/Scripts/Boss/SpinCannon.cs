using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCannon : MonoBehaviour
{
    public GameObject Target;//プレイヤーの位置
    Vector3 rawAngre;
    float radian;
    float setRadian;
    Quaternion rota;
    
    // Start is called before the first frame update
    void Start()
    {
        radian = 0;
        setRadian = 0;
        rota = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.localPosition);
        
        setRadian = Vector3.Angle(transform.position,pos);

        if (setRadian>radian)
        {
            radian+=3;
        }
        else
        {
            radian-=3;
        }

        rota= Quaternion.Euler(0, 0, radian);
        this.transform.localRotation = rota;
        if(radian>366)
        { radian = 0; }
    }
}
