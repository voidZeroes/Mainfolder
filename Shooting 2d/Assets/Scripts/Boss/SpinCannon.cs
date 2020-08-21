using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCannon : MonoBehaviour
{
    public GameObject Target;//プレイヤーの位置
    public GameObject Laser;
    Vector3 rawAngre;
    float radian;
    float setRadian;
    Quaternion rota;

    float laserCounter;
    int laserWait;
    
    // Start is called before the first frame update
    void Start()
    {
        radian = 0;
        setRadian = 0;
        laserWait = 5;
        laserCounter = 0;
        rota = this.transform.rotation;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {


        Vector3 pos = Camera.main.WorldToScreenPoint(transform.localPosition);
        
        setRadian = GetAngle(this.transform.localPosition, Target.transform.localPosition);

        laserCounter += Time.deltaTime;
        if(laserCounter>=laserWait)
        {
            Laser.GetComponent<ParticleSystem>().Play();
            laserCounter = 0;
        }
        if (setRadian>radian)
        {
            radian+=3;
        }
        else
        {
            radian-=3;
        }

        //rota= Quaternion.Euler(0, 0,radian);


        radian = GetAngle(this.transform.position, Target.transform.position);
//        Debug.Log(radian);
        rota = Quaternion.Euler(0, 0, radian);
        this.transform.localRotation = rota;
        if(radian>360)
        { radian = 0; }
    }

    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }
}
