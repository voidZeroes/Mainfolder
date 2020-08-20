using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSystem : MonoBehaviour
{
    Animator gateAnim;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gateAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Mathf.Abs(this.transform.position.x - player.transform.position.x));
        if (Mathf.Abs(this.transform.position.x - player.transform.position.x)>13)
        {
            gateAnim.SetBool("HowOpen", false);
            this.transform.GetChild(0).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == "BlueGate")
        {
            if (collision.tag == "Bullet")
              {

                gateAnim.SetBool("HowOpen", true);
                this.transform.GetChild(0).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
            }

        }
    }

}
