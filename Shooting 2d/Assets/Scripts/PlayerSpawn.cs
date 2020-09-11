using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] GatePair;
    GameObject buffer;


    // Start is called before the first frame update
    void Start()
    {
        buffer = this.gameObject;
        if (Data.GetScneneChangeFlag())
        {
            for (int i = 0; i < GatePair.Length; i++)
            {
                if (GatePair[i].transform.GetComponent<GatePlayer>().GetGatePairNumber()
                    == this.gameObject.GetComponent<Data>().LoadGateNumber())
                {
                    var instance = (GameObject)Instantiate(Player, GatePair[i].transform.GetChild(0).transform.position, Quaternion.identity);

                    instance.GetComponent<MovePlayer>().LoadPlayerCharactor(buffer.GetComponent<Data>().LoadPlayerRb2(), buffer.GetComponent<Data>().LoadPlayerLife(),
                        buffer.GetComponent<Data>().LoadPlayerMLife(), buffer.GetComponent<Data>().LoadPlayerMMissile(), buffer.GetComponent<Data>().LoadPlayerMissile());
                    instance.name = "ShadowBody";
                    buffer.GetComponent<Data>().SetScneneChangeFlag(false);
                }
            }
        }

    }

    private void Update()
    {
      

    }
}
