using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSystem : MonoBehaviour
{
    public Text TitleName;
    public Text PlsStart;
    float time;
    float sparkTime;
    bool startFlag;
    bool setMaxAlpha;
    static float fadeOutSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        sparkTime = 0.5f;
        startFlag=false;
        setMaxAlpha = false;
    }

    // Update is called once per frame
    void Update()
    {



        PlsStart.color = CalcAlpha(PlsStart.color);

        if (startFlag && setMaxAlpha)
        {
            TitleName.color = CalcAlpha(PlsStart.color);
        }

         //Start画面遷移処理
        if (Input.GetButtonDown("Start"))
        {
            startFlag = true;

        }
        if(startFlag&&setMaxAlpha&&PlsStart.color.a<=0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private Color CalcAlpha(Color color)//PleaseStartの点滅処理
    {
        if (startFlag&&sparkTime>=0)
        {
            time += Time.deltaTime * 100.0f * 1;
            color.a = Mathf.Sin(time) * 0.5f + 0.5f;
            sparkTime -= Time.deltaTime;
        }
        else if(startFlag && sparkTime <= 0)
        {if(!setMaxAlpha)
            {
                color.a = 1;
                setMaxAlpha = true;
                return color;
            }

            color.a -= fadeOutSpeed;
        }
        else
        {
            time += Time.deltaTime * 3.0f * 1;
            color.a = Mathf.Sin(time) * 0.5f + 0.5f;
        }


        return color;
    }
}
