using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu :BaseScene
{

    [SerializeField]
    private Text __coin = null;

    [SerializeField]
    private GameObject __debugObj = null;

    private void Awake()
    {
        __coin.text = string.Format("{0:#,0}", DataManager.playerData.GetCoinData());
    }



    private IEnumerator Start()
    {
#if __DEBUG
        __debugObj.SetActive(false);
#endif

        BaseSound.LoadSe("submit", "SE/decision6");
        BaseSound.LoadSe("cancel", "SE/cancel3");
        BaseSound.PlayBgm("bgm");

        while(SceneMoveManager.Instance.IsAnimatin == true)
        {
            yield return null;
        }

        string json = DataManager.Instance.readJsonData(Const.WEAPONJSONDATA);

        DataManager.carData.SetMasterFromJson(json);

        //fade out end

    }

    public void OnCLickSelectStage()
    {
        //BaseSound.PlaySe("submit", 0);
        Transfer("SelectStage");
    }

    public void OnClickSettings() {
        //BaseSound.PlaySe("submit", 0);
        Transfer("Settings");
    }

    public void OnClickStore() {
        //BaseSound.PlaySe("submit", 0);
        Transfer("Store");
    }


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        ScreenCapture.CaptureScreenshot("image.png");
    //        //Debug.Log("screenshot");
    //    }
    //}

}