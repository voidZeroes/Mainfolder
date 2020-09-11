using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reboot : MonoBehaviour
{
    float timer;
    public GameObject panel;
    Color alpha;
    // Start is called before the first frame update
    void Start()
    {
        timer = 3;
        alpha.a = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 3)
        {
            alpha.a -= Time.deltaTime;
        }
        if(alpha.a<=0)
        {
            timer -= Time.deltaTime;
        }

        if(timer<=0)
        {
            alpha.a += Time.deltaTime;
        }
        panel.GetComponent<Image>().color = alpha;

        if(alpha.a>1)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
