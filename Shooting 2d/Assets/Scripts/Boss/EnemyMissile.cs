using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    GameObject Target;
    public GameObject forwardTarget;
    public GameObject Boom;
    float maxSpeed;
    float timer;
    Vector3 pos;
    float life;


    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("ShadowBody");
        timer = 5;
        maxSpeed = 4;
        life = 10;
    }

    // Update is called once per frame
    void Update()
    {
        var diff =Target.transform.position- this.transform.position;//進行方向
        var spinZ =Quaternion.Euler(0,0 ,GetAngle(this.transform.position, Target.transform.position));

        pos = diff.normalized * maxSpeed * Time.deltaTime;
        if (timer > 0)
        {
            if (this.GetComponent<Rigidbody2D>().velocity.magnitude < maxSpeed)
            {
                this.GetComponent<Rigidbody2D>().velocity += new Vector2(pos.x, pos.y);
                
                this.transform.localRotation=spinZ;
            }
        }

        timer -= Time.deltaTime;

        life -= Time.deltaTime;
        if(life<=0)
        {
            BoomGen();
        }
    }

    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            BoomGen();
        }
    }

    void BoomGen()
    {
        GameObject instance = (GameObject)Instantiate(Boom,this.transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }

}
