﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSupply : MonoBehaviour
{   // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(this);
        }

    }
}


