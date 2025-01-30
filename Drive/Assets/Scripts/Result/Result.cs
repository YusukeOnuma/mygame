using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : BaseScene
{
    [SerializeField]
    private GameObject __victoryObj = null;
    [SerializeField]
    private GameObject __lossObj = null;


    [SerializeField]
    private Text __score = null;
    [SerializeField]
    private Text __coin = null;

    [SerializeField]
    private GameObject __sns_aBtn = null;
    [SerializeField]
    private GameObject __sns_iBtn = null;

    [SerializeField]
    private Text __shareText = null;

    [SerializeField]
    private Text __nowCoin = null;

    public GameObject three_starts;

    private bool __isWait = false;
    private StageUnlock __stageUnlock = null;

    private int __nowStageId = 0;
    private int __nextStageId = 0;
    private int __nextStagePrice = 0;

    private Popup_YesNo __yesNo = null;
    private bool __isWait2 = false;

    private SelectStageMaster __stageItem = null;


    private StageMaster __stage = null;
    private SelectStageMaster __selectStageMaster = null;

    private Animator __victoryText = null;
    private Transform __victoryCover = null;

    private GameObject __stars = null;
    private List<AtlasImageManager> __atlasStars = null;
    private Animator __victoryStar = null;

    private Animator __LossText = null;

    public int _sumScore = 0;

    [SerializeField]
    public Button Next_Btn;

    Popup_GetCoin __getcoin = null;

    private bool __isGetCoin = false;

    // Start is called before the first frame update
    private IEnumerator Start()
    {

        __nowStageId = DataManager.nowStageId;

        BaseSound.LoadSe("submit", "SE/decision6");

        string json = DataManager.Instance.readJsonData(Const.SELECTSTAGEJSONDATA);


        DataManager.selectStageData.SetMasterFromJson(json);
        __stageItem = DataManager.selectStageData.GetSelectStageMasterById(DataManager.nowStageId);

        _sumScore = DataManager.nowGetScore;


        bool isStarShow = false;
        if (_sumScore > __stageItem.clearScore1)
        {
            __victoryObj.SetActive(true);
            __lossObj.SetActive(false);
            isStarShow = true;
        }
        else
        {
            __victoryObj.SetActive(false);
            __lossObj.SetActive(true);

        }

        __score.text = string.Format("SCORE:{0}", 0);
        __coin.text = string.Format("COIN:{0}", 0);

        __nowCoin.text = string.Format("{0:#,0}", DataManager.playerData.GetCoinData());

        while (SceneMoveManager.Instance.IsAnimatin == true)
        {
            yield return null;
        }

        //fade out end

        //Lossなら実行しない
        if (isStarShow)
        {
            Vector3 pos = new Vector3(0f, 25f, 0);

            if (DataManager.nowStageId == DataManager.selectStageData.GetSelectStageMasters.Count)
            {
                Next_Btn.GetComponent<Button>().interactable = false;
            }


            __stars = __victoryObj.transform.Find("three_stars").gameObject;
            __atlasStars = new List<AtlasImageManager>();
            __atlasStars.AddRange(__stars.GetComponentsInChildren<AtlasImageManager>());

            for(int i = 0; i < __atlasStars.Count; i++)
            {
                __atlasStars[i].GetComponent<Animator>().enabled = false;
                __atlasStars[i].Name = "lose_star";
            }


            __victoryText = __victoryObj.transform.Find("Victory").GetComponent<Animator> ();
            __victoryText.enabled = false;

            __victoryCover = __victoryObj.transform.Find("VictoryCover").GetComponent<Transform>();

            //__victoryStar = newPrefab2.GetComponent<Animator>();
            //__victoryStar.enabled = false;

            StartCoroutine(__StartVictoryAnim());

            

            __nextStageId = (DataManager.nowStageId + 1);
            //NextStageがあれば
            if (__nextStageId <= DataManager.selectStageData.GetMaxStageId)
                __nextStagePrice = DataManager.selectStageData.GetSelectStageMasterById(__nextStageId).unlockCoin;

            //__shareText.text = __score.text + " GET!! \n" + Const.APP_STORE;


            //__sns_aBtn.SetActive(true);
            //__sns_iBtn.SetActive(false);

            yield break;
        }
        //loss

        __LossText = __lossObj.transform.Find("Loss").GetComponent<Animator>();
        __LossText.enabled = false;

        StartCoroutine(__StartLossAnim());
    }

    private IEnumerator __StartVictoryAnim()
    {
        yield return new WaitForSeconds(0.5f);

        __victoryText.enabled = true;
        yield return new WaitForSeconds(1f);

        if (_sumScore > __stageItem.clearScore1)
        {
            __atlasStars[0].Name = "win_star";
            __atlasStars[0].GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        if (_sumScore > __stageItem.clearScore2)
        {
            __atlasStars[1].Name = "win_star";
            __atlasStars[1].GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        if (_sumScore > __stageItem.clearScore3)
        {
            __atlasStars[2].Name = "win_star";
            __atlasStars[2].GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        //__victoryStar.enabled = true;
        DataManager.playerData.SetScoreData(__nowStageId, _sumScore);

        __score.text = string.Format("SCORE:{0}", DataManager.nowGetScore.ToString());
        //__score.text = string.Format("SCORE:{0}", 10000);
        yield return new WaitForSeconds(0.2f);
        __coin.text = string.Format("COIN:{0}", DataManager.nowGetCoin.ToString());
        //__coin.text = string.Format("COIN:{0}", 210);

        DataManager.playerData.SetClearCountData(DataManager.playerData.GetClearCountData() + 1);

        int clearCount = DataManager.playerData.GetClearCountData();
        int rand = UnityEngine.Random.Range(1, 5);
        
        if (clearCount >= rand)
        {

            __getcoin = Popup_GetCoin.Show("", "Get 300 Coin", (string name) => {
                if(name == "Video")
                {
                    StartCoroutine(__WacthVideo());
                }
                __getcoin.Exit();
            });


            
            DataManager.playerData.SetClearCountData(0);
        }


        yield return null;
    }

    private IEnumerator __WacthVideo()
    {
        if (__isGetCoin) yield break;
        __isGetCoin = true;


        Popup_Progress progress = Popup_Progress.Show();

        yield return new WaitForSeconds(0.5f);
        RewardManager.SetRewardVideo((EventArgs args) => { });
        ////Debug.Log("Wacth :: 1");
        yield return new WaitForSeconds(0.5f);
        ////Debug.Log("Wacth :: 2");

        RewardManager.PlayReward(
         (EventArgs args) => {
             AdsEventArgs arg = args as AdsEventArgs;
             if (arg.result == 0)
             {
                 ////Debug.Log("Wacth :: 4");

                 StartCoroutine(__GetCoinWindow());

             }
             else
             {
             }
             ////Debug.Log("Wacth :: 3");
             progress.Exit();
             __isGetCoin = false;
         });

    }

    private IEnumerator __GetCoinWindow()
    {
        DataManager.playerData.SetCoinData(DataManager.playerData.GetCoinData() + 300);
        __nowCoin.text = string.Format("{0:#,0}", DataManager.playerData.GetCoinData());

        yield return new WaitForSeconds(0.5f);
        Popup_OneBtn.Show("Congrats!!", "GET 300 COIN");
    }



    private IEnumerator __StartLossAnim()
    {
        yield return new WaitForSeconds(0.5f);
        __LossText.enabled = true;
    }

    public void OnClickShare()
    {
        string text = string.Format("Score {0} in Stage {1}!", _sumScore, __nowStageId);
        string url = Const.APP_STORE;


        SocialConnector.SocialConnector.Share(text, url);
    }

    public void OnClickRestart()
    {
        BaseSound.PlaySe("submit", 0);
        SceneMoveManager.Instance.Transfer("Stage");
        //Destroy(temp);
    }

    public void OnClickStageSelect()
    {
        BaseSound.PlaySe("submit", 0);
        SceneMoveManager.Instance.Transfer("SelectStage");
        //Destroy(temp);
    }

    public void OnClickNext()
    {

        BaseSound.PlaySe("submit", 0);
        if (__isWait) return;
        __isWait = true;

        //Debug.Log("DataManager.playerData.GetScoreData(__nextStageId) :::: " + DataManager.playerData.GetScoreData(__nextStageId));
        //Debug.Log("__nextStageId :::: " + __nextStageId);
        DataManager.nowStageId = __nextStageId;
        if (DataManager.playerData.GetScoreData(__nextStageId) >= 0)
        {
            SceneMoveManager.Instance.Transfer("Stage");
            //SceneManager.LoadScene("Stage");
            return;
        }



        __stageUnlock = StageUnlock.Show(__nextStagePrice, CallBack, (bool flg) => {
            __isWait = false;
        });
    }

    public void CallBack(string type)
    {
        if (__isWait) return;
        __isWait = true;
        if (type == "watch")
        {

            StartCoroutine(__PlayReward());
        }
        else
        {
            __yesNo = Popup_YesNo.Show("Confirmation", "Do you want to buy this stage " + __nextStageId + " for " + __nextStagePrice + " coin. Is it OK?", "Buy", "Cancel", CallBack2, (bool flg) => {
                __isWait2 = false;
            });
        }
        __stageUnlock.Exit(__stageUnlock.gameObject, true, (bool flg) => {
            __isWait = false;
        });
        
    }

    private IEnumerator __PlayReward()
    {

        Popup_Progress progress = Popup_Progress.Show();

        yield return new WaitForSeconds(0.5f);
        RewardManager.SetRewardVideo((EventArgs args) => { });


        yield return new WaitForSeconds(0.5f);

        RewardManager.PlayReward(
             (EventArgs args) => {
                 AdsEventArgs arg = args as AdsEventArgs;
                 if (arg.result == 0)
                 {
                     DataManager.playerData.SetScoreData(__nextStageId, 0);
                     SceneManager.LoadScene("Stage");
                 }
                 else
                 {
                     
                     DataManager.nowStageId = __nextStageId - 1;
                 }
                     progress.Exit();
                 
             });

        yield return null;
    }

    public void CallBack2(string type)
    {
        if (__isWait2) return;
        __isWait2 = true;

        int oldCoins = DataManager.playerData.GetCoinData();
        int newCoins = 0;


        if (oldCoins >= __nextStagePrice)
        {
            if (type == "Yes")
            {
                //Debug.Log("clicked yes");
                DataManager.playerData.SetScoreData(__nextStageId, 0);
                newCoins = (oldCoins - __nextStagePrice);
                oldCoins = newCoins;
                //Debug.Log(oldCoins);
                DataManager.playerData.SetCoinData(newCoins);
                __nowCoin.text = string.Format("{0:#,0}", DataManager.playerData.GetCoinData());
                SceneMoveManager.Instance.Transfer("Stage");

            }
            else
            {
                //Debug.Log("clicked no");
            }
        }
        else
         if (type == "Yes")
        {
            Popup_OneBtn.Show("Not Enough ", "To buy this stage " + __nextStageId + " you have to earn " + (__nextStagePrice - oldCoins) + " coins more. ");
        }
        __yesNo.Exit(__yesNo.gameObject, true, (bool flg) => {
            __isWait = false;

        });
    }
}
