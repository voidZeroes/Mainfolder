using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fadepanel;
    Color col;
    bool startFlag;
    AudioSource push;
    void Start()
    {
        col.a = 1;
        startFlag = false;
        push = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {if(col.a>0&&!startFlag)
        {
            col.a -= Time.deltaTime;
        }
        else if(startFlag)
        {
            col.a += Time.deltaTime;
        }
        
    if(Input.GetButtonDown("Start"))
        {
            startFlag = true;
            push.Play();
        }

    if(startFlag&&col.a>=1)
        {
            SceneManager.LoadScene("SampleScene");
        }

        fadepanel.GetComponent<Image>().color = col;
    }
}
