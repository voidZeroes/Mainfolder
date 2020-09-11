using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    Rigidbody2D playerRb2;
    int playerMaxMissile;
    int playerMissile;
    int playerMaxLife;
    int playerLife;

    bool SceneChangeFlag;
    int gatePairNumber;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        gatePairNumber = 9999999;
        SceneChangeFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SavePlayerData(GameObject player)
    {
        playerMaxMissile = player.GetComponent<MovePlayer>().GetMissileAmmoMax();
        playerMissile = player.GetComponent<MovePlayer>().GetMissileAmmo();
        playerMaxLife = player.GetComponent<MovePlayer>().GetPlayerMaxLife();
        playerLife = player.GetComponent<MovePlayer>().GetPlayerLife();

    }
    public void SaveGateNumber(int number)
    {
        gatePairNumber = number;
    }

    public Rigidbody2D LoadPlayerRb2()
    {
        return playerRb2;
    }
    public int LoadPlayerMMissile()
    {
        return playerMaxMissile;
    }
    public int LoadPlayerMissile()
    {
        return playerMissile;
    }
    public int LoadPlayerMLife()
    {
        return playerMaxLife;
    }
    public int LoadPlayerLife()
    {
        return playerLife;
    }



    public int LoadGateNumber()
    {
        return gatePairNumber;
    }

    public bool GetScneneChangeFlag()
    {
        return SceneChangeFlag;
    }
    public void SetScneneChangeFlag(bool flag)
    {
        SceneChangeFlag=flag;
    }
}
