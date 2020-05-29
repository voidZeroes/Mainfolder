using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
   public GameObject energyTank;
    public GameObject player;
    public Text text;
    float offsetX;
    float tankFloat;
    int tankNum;
    int playerLife;//総量
    int sliderCnt;//0～99

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        playerLife = player.GetComponent<MovePlayer>().SetPlayerLife();
        tankFloat = playerLife % 99;
        tankNum = playerLife/99;
        slider.value = 99;
        
    }

    // Update is called once per frame
    void Update()
    {
        playerLife = GetHp();//毎回Player側のHPを取得

        tankFloat = playerLife % 99;//ゲージ内部の量
        tankNum = playerLife/99;//タンクの量

        slider.value = tankFloat;
        text.text =slider.value.ToString();//量をテキストに

//        TankNumの量だけタンクを表示させたい
//タンク1個目の座標 + Offset量 * TankNumでいいんだろうか
//↑増加手順ならそれでOK
//消去を考えたい

//てきとうなObjectの下にぶらさげて、Childのナンバーで管理してみるか ?

        //そうじゃん参考元だと取得済みのやつは透過させるだけじゃん。
        //増えるやつだけ考えればOK



        //if(slider.value==-1)
        //{
        //    tankFloat = playerLife / 99;
        //    tankNum =(int)tankFloat;

        //    slider.value = (int)(tankFloat-tankNum)*99;
        //}
        //else
        //{
        //    slider.value=
        //}
        //99以上の体力の時にゲージを1週させたい。
        //99を割って、その数をストック。下限に達するたびにそれから１減らしてゲージを充填……で大丈夫だろうか。
        //上限に達したらストックを１増やして、そのあと、99以上の部分をゲージに反映したらいい。




    }

    private int GetHp()
    {
        return player.GetComponent<MovePlayer>().SetPlayerLife();
    }
    private int GetMaxHp()
    {
        return player.GetComponent<MovePlayer>().SetPlayerMaxLife();
    }

}
