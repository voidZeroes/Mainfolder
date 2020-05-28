using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObjects : MonoBehaviour
{
    public float speed = 1.0f;
    private int move = 0;
    public float startPos;
    public float endPos;
    GameObject cam;

    GameObject player;
    int defalt;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("ShadowBody");
        cam = GameObject.Find("Camera");
        defalt = 0;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (cam.transform.GetChild(1).GetComponent<ScrollCol>().GetScrFlg() == 1)
        {
            move = cam.transform.GetChild(1).GetComponent<ScrollCol>().GetScrFlg();

            speed = player.transform.GetComponent<MovePlayer>().GetPlayerMove();
        }
        else if (cam.transform.GetChild(1).GetComponent<ScrollCol>().GetScrFlg() == -1)
        {
            move = defalt;

            cam.transform.GetChild(1).GetComponent<ScrollCol>().SetScrFlg(false);
        }


        if (cam.transform.GetChild(2).GetComponent<ScrollCol>().GetScrFlg() == -1)
        {
            move = cam.transform.GetChild(2).GetComponent<ScrollCol>().GetScrFlg();


            speed = player.transform.GetComponent<MovePlayer>().GetPlayerMove();
        }
        else if (cam.transform.GetChild(2).GetComponent<ScrollCol>().GetScrFlg() == 1)
        {
            move = defalt;
            cam.transform.GetChild(2).GetComponent<ScrollCol>().SetScrFlg(false);
        }


        //フレームｽｽﾒｰ
        transform.Translate(move * speed * Time.deltaTime, 0, 0);
        
        if (transform.position.x<=endPos)
        {
            ScrollEnd();
        }
        
    }
    void ScrollEnd()
    {
        //移動分戻す
        transform.Translate(move * (endPos - startPos), 0, 0);
        //同GameObjectにアタッチされてるコンポーネントにメッセージを送る
        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
    }
}
