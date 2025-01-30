using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStage : BaseScene
{
    const int STAGE_HORIZONTAL_NUM = 4;
    const int STAGE_VERTICAL_NUM = 4;

    public GameObject unlockPopup;
    public GameObject level_unlocked;

    [SerializeField]
    public Button buttonHome;

    private bool islocked = false;

    [SerializeField]
    private GameObject __content = null;

    [SerializeField]
    private Text __coin = null;

    private PlayerData __playerData;


    private List<SelectStageMaster> __stageItems = null;


    private StageMaster __stage = null;
    private SelectStageMaster __selectStageMaster = null;
    private int _levelState = 0;
    private int _oldCoins = 0;
    private int _newCoins = 0;

    private bool __isWait = false;
    private StageUnlock __stageUnlock = null;

    private int __stageId;
    private int __stagePrice;

    private Popup_YesNo __yesNo = null;
    private bool __isWait2 = false;

    private Dictionary<int,Button> __stageLists = null;
    private Dictionary<int, GameObject> __stageLists2 = null;

    private Sprite __unlocked = null;
    private Sprite __locked = null;

    private Sprite __lossStar = null;
    private Sprite __winStar = null;
    private Sprite __light = null;

    private Text levelNo = null;

    private void Awake()
    {
        __coin.text = string.Format("{0:#,0}", DataManager.playerData.GetCoinData());
    }


    private IEnumerator Start()

    {
        _oldCoins = DataManager.playerData.GetCoinData();
        //Debug.Log(_oldCoins);

        //json load

        string json = DataManager.Instance.readJsonData(Const.SELECTSTAGEJSONDATA);


        DataManager.selectStageData.SetMasterFromJson(json);
        __stageItems = DataManager.selectStageData.GetSelectStageMasters;

        var old_score = PlayerPrefs.GetInt("player_score");
        RectTransform self = __content.GetComponent<RectTransform>();
        var size = self.sizeDelta;
        size.y = (90f * STAGE_VERTICAL_NUM) + 20f;
        self.sizeDelta = size;

        

        Vector3 pos = new Vector3(-155f, (size.y / 2 - 55f), 0);


        __unlocked = Resources.Load("Sprites/unlocked_level", typeof(Sprite)) as Sprite;
        __locked = Resources.Load("Sprites/lockedStage", typeof(Sprite)) as Sprite;

        __lossStar = Resources.Load("Sprites/st_loss_star", typeof(Sprite)) as Sprite;
        __winStar = Resources.Load("Sprites/I-002", typeof(Sprite)) as Sprite;
        __light = Resources.Load("Sprites/light", typeof(Sprite)) as Sprite;



        //Stage1だけはアンロック状態にしておく。すでにアンロックしていれば処理しない。
        if (DataManager.playerData.GetScoreData(1) == -1)
        {
            DataManager.playerData.SetScoreData(1, 0);
        }


        __stageLists = new Dictionary<int, Button>();

        __stageLists2 = new Dictionary<int, GameObject>();

        for (int i = 0; i < __stageItems.Count; i++)
        {


            pos.x += 60;
            GameObject newPrefab = Instantiate(level_unlocked, pos, new Quaternion(0, 0, 0, 0));

            GameObject s1 = newPrefab.transform.GetChild(1).gameObject;
            Image star1 = s1.GetComponent<Image>() as Image;

            GameObject s2 = newPrefab.transform.GetChild(2).gameObject;
            Image star2 = s2.GetComponent<Image>() as Image;

            GameObject s3 = newPrefab.transform.GetChild(3).gameObject;
            Image star3 = s3.GetComponent<Image>() as Image;

            GameObject x = newPrefab.transform.GetChild(0).gameObject;
            Button btn = x.GetComponent<Button>();

            GameObject y = x.transform.GetChild(0).gameObject;


            //GameObject y = newPrefab.transform.GetChild(4).gameObject;
            levelNo = y.GetComponent<Text>();
            y.GetComponent<Text>().text = __stageItems[i].stageId.ToString();

            __stageLists.Add(__stageItems[i].stageId, btn);
            __stageLists2.Add(__stageItems[i].stageId, newPrefab);

            int localIndex = i;

            if (DataManager.playerData.GetScoreData(__stageItems[i].stageId) == -1)
            {
                //btn.image.sprite = __locked;
                levelNo.color = Color.white;

                star1.sprite = __light;
                star2.sprite = __light;
                star3.sprite = __light;


                btn.onClick.AddListener(() => OnClickLockedStage(__stageItems[localIndex].stageId, __stageItems[localIndex].unlockCoin));


            }

            else
            {
                //levelNo.color = Color.black;
                if (DataManager.playerData.GetScoreData(__stageItems[i].stageId) > __stageItems[localIndex].clearScore3)
                {
                    star1.sprite = __winStar;
                    star2.sprite = __winStar;
                    star3.sprite = __winStar;

                }
                else if (DataManager.playerData.GetScoreData(__stageItems[i].stageId) > __stageItems[localIndex].clearScore2)
                {
                    star1.sprite = __winStar;
                    star2.sprite = __winStar;
                    star3.sprite = __lossStar;

                }
                else if (DataManager.playerData.GetScoreData(__stageItems[i].stageId) > __stageItems[localIndex].clearScore1)
                {
                    star1.sprite = __winStar;
                    star2.sprite = __lossStar;
                    star3.sprite = __lossStar;

                }
                else
                {
                    star1.sprite = __lossStar;
                    star2.sprite = __lossStar;
                    star3.sprite = __lossStar;
                }

                btn.onClick.AddListener(() => OnClickStage(__stageItems[localIndex].stageId));

            }

            level_unlocked.transform.localScale = new Vector3(1, 1, 1);
            newPrefab.transform.SetParent(__content.transform, false);
            //__stageItems.Add(newPrefab.GetComponent<StageItem>());
            int StarOne = __stageItems[localIndex].clearScore1;



        }

        BaseSound.LoadSe("submit", "SE/decision6");
        BaseSound.LoadSe("cancel", "SE/cancel3");




        while (SceneMoveManager.Instance.IsAnimatin == true)
        {
            yield return null;
        }

    }

    public void OnClickHome()
    {
     
        BaseSound.PlaySe("cancel", 0);
        Transfer("MainMenu");
    }

    

    public void OnCLickwin()
    {
        BaseSound.PlaySe("submit", 0);
        Transfer("SelectStage");


    }


    /*    public void Update()
        {
            Instantiate(level_unlocked,transform.position, transform.rotation);
        } */

    public void OnClickStage(int playStageId)
    {
        //BaseSound.PlaySe("submit", 0);

        DataManager.nowStageId = playStageId;

        __selectStageMaster = DataManager.selectStageData.GetSelectStageMasterById(playStageId);

        Popup_StartGame.Show("", __selectStageMaster, (a) =>
        {
            SceneMoveManager.Instance.Transfer("Stage");
        }, (b) =>
        {
        });

       
    }

    public void OnClickLockedStage(int id,int price)
    {
        __stageId = id;
        __stagePrice = price;
        //BaseSound.PlaySe("submit", 0);
        //unlockPopup.GetComponent<StageUnlock>().Open(unlockPopup);
        if (__isWait) return;
        __isWait = true;

        if (DataManager.playerData.GetScoreData(id) == -1)
        {
            __stageUnlock = StageUnlock.Show(__stagePrice, CallBack, (bool flg) => {
                __isWait = false;
            });
        }

       

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
            __yesNo = Popup_YesNo.Show("Confirmation", "Do you want to buy this stage " + __stageId+ " for "+ __stagePrice+ " coin. Is it OK?" , "Buy","Cancel",CallBack2, (bool flg) => {
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
        //先読み
        RewardManager.SetRewardVideo((EventArgs args) => { });

        yield return new WaitForSeconds(0.5f);

        RewardManager.PlayReward(
             (EventArgs args) => {
                 AdsEventArgs arg = args as AdsEventArgs;
                 if (arg.result == 0)
                 {
                     DataManager.playerData.SetScoreData(__stageId, 0);
                     __ReLoad(__stageId);
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

        if (_oldCoins >= __stagePrice)
        {
            if (type == "Yes")
            {
                //Debug.Log("clicked yes");
                DataManager.playerData.SetScoreData(__stageId, 0);
                _newCoins = (_oldCoins - __stagePrice);
                _oldCoins = _newCoins;
                //Debug.Log(_oldCoins);
                DataManager.playerData.SetCoinData(_newCoins);
                __coin.text = string.Format("{0:#,0}", DataManager.playerData.GetCoinData());
                __ReLoad(__stageId);

            }
            else
            {
                //Debug.Log("clicked no");
            }
        }
        else
        if (type == "Yes")
        {
         Popup_OneBtn.Show("Not Enough ", "To buy this stage "+ __stageId + " you have to earn " + (__stagePrice - _oldCoins) + " coins more. ");
        }
        __yesNo.Exit(__yesNo.gameObject, true, (bool flg) => {
            __isWait = false;

        });
    }


    private void __ReLoad(int id)
    {
        
        Button button = __stageLists[__stageId];
        GameObject go = __stageLists2[__stageId];
        button.onClick.AddListener(() => OnClickStage(id));
        

        if (DataManager.playerData.GetScoreData(__stageId) != -1)
        {
            button.image.sprite = __unlocked;
            
            GameObject s1= go.transform.GetChild(1).gameObject;
            Image star1= s1.GetComponent<Image>() as Image;
            star1.sprite = __lossStar;

            GameObject s2= go.transform.GetChild(2).gameObject;
            Image star2= s2.GetComponent<Image>() as Image;
            star2.sprite = __lossStar;

            GameObject s3 = go.transform.GetChild(3).gameObject;
            Image star3 = s3.GetComponent<Image>() as Image;
            star3.sprite = __lossStar;

            GameObject s4 = button.transform.GetChild(0).gameObject;
            Text levelNo = s4.GetComponent<Text>();
            levelNo.color = Color.black;



        }
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