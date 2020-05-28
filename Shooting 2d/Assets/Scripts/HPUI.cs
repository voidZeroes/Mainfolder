using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
   public GameObject energyTank;
    public GameObject player;
    float offsetX;
    int tankInt;
    int playerLife;//総量
    int sliderCnt;//0～99

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        playerLife = player.GetComponent<MovePlayer>().SetPlayerLife();

        slider.value = 99;
        
    }

    // Update is called once per frame
    void Update()
    {
        //99以上の体力の時にゲージを1週させたい。
        //99を割って、その数をストック。下限に達するたびにそれから１減らしてゲージを充填……で大丈夫だろうか。
        //上限に達したらストックを１増やして、そのあと、99以上の部分をゲージに反映したらいい。
        



    }

    private void GetHp()
    {
        
    }

}
