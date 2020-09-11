using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] GatePair;
    GameObject buffer;
    public GameObject[] Gate;
    GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        buffer = GameObject.Find("DataBox");
        UI = GameObject.Find("EnergyGui");
        if (buffer.GetComponent<Data>().GetScneneChangeFlag())
        {
            for (int i = 0; i < GatePair.Length; i++)
            {
                if (GatePair[i].transform.GetComponent<GatePlayer>().GetGatePairNumber()
                    == buffer.GetComponent<Data>().LoadGateNumber())
                {
                    var instance = (GameObject)Instantiate(Player, GatePair[i].transform.GetChild(0).transform.position, Quaternion.identity);

                    instance.GetComponent<MovePlayer>().LoadPlayerCharactor(buffer.GetComponent<Data>().LoadPlayerRb2(), buffer.GetComponent<Data>().LoadPlayerLife(),
                        buffer.GetComponent<Data>().LoadPlayerMLife(), buffer.GetComponent<Data>().LoadPlayerMMissile(), buffer.GetComponent<Data>().LoadPlayerMissile());
                    instance.name = "ShadowBody";
                    buffer.GetComponent<Data>().SetScneneChangeFlag(false);
                    Gate[i].GetComponent<GateSystem>().GateOpen();
                    UI.GetComponent<HPUI>().Init();
                }
            }
        }

    }

    private void Update()
    {
      

    }
}
