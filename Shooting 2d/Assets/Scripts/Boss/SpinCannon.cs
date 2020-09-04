using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCannon : MonoBehaviour
{
    public GameObject Target;//プレイヤーの位置
    public GameObject Laser;
    public GameObject ShotPoint;
    public GameObject BulletPrefab;
    Vector3 rawAngre;
    float radian;
    float setRadian;
    Quaternion rota;
    float shotDelay;
    
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
        shotDelay = 5;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {


        Vector3 pos = Camera.main.WorldToScreenPoint(transform.localPosition);
        
        setRadian = GetAngle(this.transform.position, Target.transform.localPosition);

        //radian = GetAngle(this.transform.position, Target.transform.position);

        //エフェクトレーザーだったが、扱いに困ったため一度廃止
        /*
        laserCounter += Time.deltaTime;
        if(laserCounter>=laserWait)
        {
            Laser.GetComponent<ParticleSystem>().Play();
            laserCounter = 0;
        }        */

        //砲の回転部分
        if (setRadian>=radian)
        {
            radian+=0.2f;
        }
        else if (setRadian <= radian)
        {
            radian-=0.2f;
        }

//        Debug.Log(radian);
        rota = Quaternion.Euler(0, 0, radian);
        this.transform.localRotation = rota;
        if(radian>360)
        { radian = 0; }


        shotDelay -= Time.deltaTime;
        if (shotDelay <= 0)
        {
            var instance = Instantiate(BulletPrefab, ShotPoint.transform.position, this.transform.rotation);
            shotDelay = 5;
            
        }
    }

    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }
}
