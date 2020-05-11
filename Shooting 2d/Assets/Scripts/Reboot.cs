using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reboot : MonoBehaviour
{
    int timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        timer--;

        if(timer<0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
