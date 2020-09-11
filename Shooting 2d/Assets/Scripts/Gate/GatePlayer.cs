using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatePlayer : MonoBehaviour
{
    public int gatePairNumber;//ペアになってるゲートのスポーン座標に飛ばす
    GameObject Player;
    GameObject dataBox;
    // Start is called before the first frame update
    private void Start()
    {
        Player = GameObject.Find("ShadowBody");
        dataBox = GameObject.Find("DataBox");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")//データ退避
        {
            dataBox.GetComponent<Data>().SavePlayerData(Player);
            dataBox.GetComponent<Data>().SaveGateNumber(gatePairNumber);
            LoadArea(gatePairNumber);
        
        }
    }

    public int GetGatePairNumber()
    {
        return gatePairNumber;
    }

    void LoadArea(int pairNumber)
    {
        switch (pairNumber)
        {
            case 0:
                if(SceneManager.GetActiveScene().name=="SampleScene")
                {
                    SceneManager.LoadScene("ShaftScene");
                }
                else if(SceneManager.GetActiveScene().name == "ShaftScene")
                {
                    SceneManager.LoadScene("SampleScene");
                }
                dataBox.GetComponent<Data>().SetScneneChangeFlag(true);
                    break;

            case 1:
                if (SceneManager.GetActiveScene().name == "ShaftScene")
                {
                    SceneManager.LoadScene("BossArea");
                }
                else if (SceneManager.GetActiveScene().name == "BossArea")
                {
                    SceneManager.LoadScene("ShaftScene");
                }
                dataBox.GetComponent<Data>().SetScneneChangeFlag(true);
                break;

        
        }
    }



}
