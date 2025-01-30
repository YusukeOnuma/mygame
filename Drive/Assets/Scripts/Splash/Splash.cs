using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : BaseScene
{
    private List<CarMaster> __cardata = null;

    private int __defaultCar=1;

    private bool __isEquipped=false;


    [SerializeField]
    public Button skipButton;

    bool disableFiring = false;
    float firingDisableDuration = 5.0f;

    private IEnumerator Start()
    {
        Application.targetFrameRate = 60;

        StartCoroutine("Disablebutton");
        //skipButton.interactable = false;
        //Application.targetFrameRate = 60;
        //QualitySettings.vSyncCount = 0;

        string json = DataManager.Instance.readJsonData(Const.WEAPONJSONDATA);

        DataManager.carData.SetMasterFromJson(json);

        __cardata = DataManager.carData.GetCarMasters;


        //default car 
        foreach (var item in __cardata) {
            if (DataManager.playerData.GetCarData(item.carId)>0) {
                __isEquipped = true;
            }
        }

        if (!__isEquipped) {
            DataManager.playerData.SetCarData(__defaultCar,1);
        }
        ////

        //動画リワード初期化
        RewardManager.InitAds();

        //BaseSound.SetValBgm(DataManager.playerData.GetBGMVol());
        //BaseSound.SetValSe(DataManager.playerData.GetSEVol());


        //BGM重そうなので、開始時にロードしておく
        //BaseSound.LoadBgm("bgm", "BGM/n72");
        //BaseSound.LoadBgm("bgm2", "BGM/n82");

        yield return new WaitForSeconds(7f);
        SceneMoveManager.Instance.Transfer("MainMenu");

       
        

    }
     public IEnumerator Disablebutton()
    {
        skipButton.enabled = false;

        yield return new WaitForSeconds(1f);

        skipButton.enabled = true;
    }

    public void Update()
    {
       // skipButton.interactable = true;
    }


    public void OnClick1()
    {
        
        Transfer("MainMenu");
    }
}