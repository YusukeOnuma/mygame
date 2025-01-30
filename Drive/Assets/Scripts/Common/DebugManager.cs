using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{

    [SerializeField]
    private GameObject __button = null;
    [SerializeField]
    private GameObject __winodw = null;


    public void ClickOpen()
    {
        __button.SetActive(false);
        __winodw.SetActive(true);
    }

    public void ClickCoinAdd()
    {
        int coin =  DataManager.playerData.GetCoinData();

        DataManager.playerData.SetCoinData(coin + 1000);
    }

    public void ClickDeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ClickClose()
    {
        __button.SetActive(true);
        __winodw.SetActive(false);
    }
}
