﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
   public GameObject energyTank;//タンクプレハブ
    public GameObject UI;//UI本体
   public GameObject tankPos1;//設置位置
    GameObject player;
    public Text hpText;
    public Text missleText;
    float offsetX;
    float offsetY;
    float tankFloat;
    int tankMaxNum;
    int tankNum;
    int tankOldNum;
    int tankOldMaxNum;
    int playerLife;//総量


    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ShadowBody");
        playerLife = player.GetComponent<MovePlayer>().GetPlayerLife();
        tankFloat = playerLife % 99;
        tankNum = playerLife/99;
        tankMaxNum = player.GetComponent<MovePlayer>().GetPlayerMaxLife()/99;
        slider.value = 99;
        tankOldMaxNum = 0;
        offsetX = energyTank.GetComponent<RectTransform>().rect.x*(energyTank.GetComponent<RectTransform>().rect.x/3);
        
        offsetY = energyTank.GetComponent<RectTransform>().rect.y * (energyTank.GetComponent<RectTransform>().rect.y / 3);
        //ここまでHP関連

    }

    // Update is called once per frame
    void Update()
    {
        tankMaxNum = player.GetComponent<MovePlayer>().GetPlayerMaxLife() / 99;
        playerLife = GetHp();//毎回Player側のHPを取得

        tankFloat = playerLife % 99;//ゲージ内部の量
        tankNum = playerLife / 99;//タンクの量

        slider.value = tankFloat;
        hpText.text = slider.value.ToString();//量をテキストに

        missleText.text = player.GetComponent<MovePlayer>().GetMissileAmmo().ToString();

        if (tankOldMaxNum < tankMaxNum)
        {
            for (int i = 0; i < tankMaxNum - tankOldMaxNum; i++)
            {
                var pos = tankPos1.transform.position;
                pos.x += offsetX * i;
                pos.z = -10;
                if(i>=10)
                {
                    pos.x -= offsetX*10;
                    pos.y -=offsetY  ;
                }
                var instans=(GameObject)Instantiate(energyTank, pos, Quaternion.identity, UI.transform.GetChild(1).transform);
                
            }
            tankOldMaxNum = tankMaxNum;
        }

        if (tankNum!=tankOldNum)//タンクが1ｆ前より多いか少ないかしたとき、
        {
            for(int i=0;i<tankMaxNum;i++)
            {
                if (i <= tankNum)//エネルギー入りなら表示
                {
                    UI.transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
                else if(tankMaxNum>=i&&i>tankNum)//そうでないなら透過する
                {

                    UI.transform.GetChild(1).transform.GetChild(i).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }

            }

        }



        tankOldNum = tankNum;

    }

    private int GetHp()
    {
        return player.GetComponent<MovePlayer>().GetPlayerLife();
    }
    private int GetMaxHp()
    {
        return player.GetComponent<MovePlayer>().GetPlayerMaxLife();
    }


    public void Init()//Scene遷移したときに見失うからそれ対策
    {
        player = GameObject.Find("ShadowBody");
        playerLife = player.GetComponent<MovePlayer>().GetPlayerLife();
        tankFloat = playerLife % 99;
        tankNum = playerLife / 99;
        tankMaxNum = player.GetComponent<MovePlayer>().GetPlayerMaxLife() / 99;
        slider.value = 99;
        tankOldMaxNum = 0;
        offsetX = energyTank.GetComponent<RectTransform>().rect.x * (energyTank.GetComponent<RectTransform>().rect.x / 3);

        offsetY = energyTank.GetComponent<RectTransform>().rect.y * (energyTank.GetComponent<RectTransform>().rect.y / 3);
    }

}
