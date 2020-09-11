using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Goal : MonoBehaviour
{
    bool fadeFlag;
    Color fadeA;
    public GameObject fadePanel;
    // Start is called before the first frame update
    private void Start()
    {
        fadeA.a = 0;
        fadeFlag = false;
    }



    private void Update()
    {
        if (fadeFlag)
        {
            fadeA.a += Time.deltaTime / 2;
            if(fadeA.a>=1)
            {
                SceneManager.LoadScene("TestEnd");
            }

        }

        fadePanel.GetComponent<Image>().color = fadeA;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

            if (collision.gameObject.tag == "Player")
            {
            fadeFlag = true;
            }
        
    }


}

