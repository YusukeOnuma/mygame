using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Victory : BasePopup
{
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

    public GameObject three_starts;

    BaseScene temp;
    private bool __isWait = false;
    private StageUnlock __stageUnlock = null;

    private int __nextStageId = 0;
    private int __nextStagePrice = 0;

    private Popup_YesNo __yesNo = null;
    private bool __isWait2 = false;

    private List<SelectStageMaster> __stageItems = null;


    private StageMaster __stage = null;
    private SelectStageMaster __selectStageMaster = null;

    public int _sumScore = 0;

    [SerializeField]
    public Button next;

    [SerializeField]
    public Button Next_Btn;

    public IEnumerator Show(GameObject prefab, int score, int coin)
    {
        _sumScore = score;

        _animator = prefab.GetComponent<Animator>();
        _animator.SetBool("close", false);

        __score.text = score.ToString();
        __coin.text = coin.ToString();
       

        GameObject newPrefab = Instantiate(prefab);
        newPrefab.transform.SetParent(GameObject.Find("PopupCanvas").transform, false);

        return null;
    }


    private void Start()
    {
        string json = DataManager.Instance.readJsonData(Const.SELECTSTAGEJSONDATA);


        DataManager.selectStageData.SetMasterFromJson(json);
        __stageItems = DataManager.selectStageData.GetSelectStageMasters;
        Vector3 pos = new Vector3(0f, 53f, 0);

        GameObject newPrefab2 = Instantiate(three_starts, pos, new Quaternion(0, 0, 0, 0));
        newPrefab2.transform.SetParent(GameObject.Find("PopupCanvas/VictoryPopup(Clone)/Objects").transform, false);

        GameObject s1 = newPrefab2.transform.GetChild(0).gameObject;
        Image star1 = s1.GetComponent<Image>() as Image;

        GameObject s2 = newPrefab2.transform.GetChild(1).gameObject;
        Image star2 = s2.GetComponent<Image>() as Image;

        GameObject s3 = newPrefab2.transform.GetChild(2).gameObject;
        Image star3 = s3.GetComponent<Image>() as Image;

        Sprite lossStar = Resources.Load("Sprites/lose_star", typeof(Sprite)) as Sprite;
        Sprite winStar = Resources.Load("Sprites/win_star", typeof(Sprite)) as Sprite;

        BaseSound.LoadSe("submit", "SE/decision6");
        temp = new BaseScene();

        int i = 0;
        int localIndex = i;

        if (DataManager.nowStageId == __stageItems.Count)
        {

            next.GetComponent<Button>().interactable = false;
            Next_Btn.GetComponent<Button>().interactable = false;
        }

        //Debug.Log(__stageItems[localIndex-1].clearScore3);
        if (_sumScore > __stageItems[localIndex-1].clearScore3)
        {
            
            star1.sprite = winStar;
            star2.sprite = winStar;
            star3.sprite = winStar;

        }
        else if (_sumScore > __stageItems[localIndex-1].clearScore2)
        {
            star1.sprite = winStar;
            star2.sprite = winStar;
            star3.sprite = lossStar;

        }
        else if (_sumScore > __stageItems[localIndex-1].clearScore1)
        {
            star1.sprite = winStar;
            star2.sprite = lossStar;
            star3.sprite = lossStar;

        }
        else
        {
            star1.sprite = lossStar;
            star2.sprite = lossStar;
            star3.sprite = lossStar;
        }


        __nextStageId = (DataManager.nowStageId + 1);
        //NextStageがあれば
        if(__nextStageId <= DataManager.selectStageData.GetMaxStageId)
            __nextStagePrice = DataManager.selectStageData.GetSelectStageMasterById(__nextStageId).unlockCoin;

        __shareText.text = __score.text + "GET!! \n" + Const.APP_STORE;

#if  UNITY_EDITOR

#elif UNITY_ANDROID
        __sns_aBtn.SetActive(true);
        __sns_iBtn.SetActive(false);
#elif UNITY_IPHONE
        __sns_iBtn.SetActive(true);
        __sns_aBtn.SetActive(false);
#endif

    }

    public void OnClickShare()
    {
        string text = "TEST";
        string url = "https://";


        SocialConnector.SocialConnector.Share(text, url);
    }

    public void OnClickRestart()
    {
        BaseSound.PlaySe("submit", 0);
        temp.Transfer("Stage");
        Destroy(temp);
    }

    public void OnClickStageSelect()
    {
        BaseSound.PlaySe("submit", 0);
        temp.Transfer("SelectStage");
        Destroy(temp);
    }

    public void OnClickNext()
    {
       
        BaseSound.PlaySe("submit", 0);
        if (__isWait) return;
        __isWait = true;

        //Debug.Log("DataManager.playerData.GetScoreData(__nextStageId) :::: " + DataManager.playerData.GetScoreData(__nextStageId));
        //Debug.Log("__nextStageId :::: " + __nextStageId);
        DataManager.nowStageId = __nextStageId;
        if (DataManager.playerData.GetScoreData(__nextStageId) >= 0) {
            SceneManager.LoadScene("Stage");
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
                     //動画途中でやめた。
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
                SceneManager.LoadScene("Stage");

            }
            else
            {
                //Debug.Log("clicked no");
            }
        }
        else
        {
            
            Popup_OneBtn.Show("Not Enough ", "To buy this stage " + __nextStageId + " you have to earn " + (__nextStagePrice - oldCoins) + " coins more. ");
        }
        __yesNo.Exit(__yesNo.gameObject, true, (bool flg) => {
            __isWait = false;

        });
    }



}
