using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;


public class RewardManager : Singleton<RewardManager>
{
#if UNITY_EDITOR
    private string __appId = "";
    private string __adUnitId = "ca-app-pub-3940256099942544~5224354917";
#elif UNITY_ANDROID
    //APPID
    private string __appId = "ca-app-pub-6019626377565384~1297707240";
#if !__DEBUG
        //UnitID
        private string __adUnitId = "ca-app-pub-6019626377565384/8759463164";
#else
        //Sample
        private string __adUnitId = "ca-app-pub-3940256099942544/5224354917";
#endif
#elif UNITY_IPHONE
    //APPID
    private string __appId = "ca-app-pub-2103257093190700~2369509289";
#if !__DEBUG
        //UnitID
        private string __adUnitId = "ca-app-pub-2103257093190700/8008848778";
#else
        //Sample
        private string __adUnitId = "ca-app-pub-3940256099942544/1712485313";
#endif
#else
    private string __appId = "";
    private string __adUnitId = "unexpected_platform";
#endif

    private RewardedAd rewardedAd;
    public bool isLoadingVideo = false;

    private float _loadStartTime = 0.0f;
    private Action<EventArgs> _callback = null;
    private bool _playing = false;
    private bool _loading = false;
    private bool _successed = false;
    private bool _bgmRestart = false;

    public bool isResume = false;


    private void Start()
    {
        //Debug.Log("####################### __appId :: " + __appId);

        MobileAds.Initialize(initStatus => LoadRewardedAd());
    }


    public static void InitAds()
    {
        //RewardManager.Instance.PrepareReward();
    }


    public void LoadRewardedAd()
    {
        var adRequest = new AdRequest();

        RewardedAd.Load(__adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("リワード広告の読み込みに失敗しました: " + error);
                    return;
                }

                Debug.Log("リワード広告が正常にロードされました！");
                rewardedAd = ad;

                // 広告イベントの登録
                RegisterEventHandlers(rewardedAd);
            });
    }

    public static void ShowRewardedAd(Action<EventArgs> callback = null)
    {
        RewardManager rm = RewardManager.Instance;

        if (rm.rewardedAd != null && rm.rewardedAd.CanShowAd())
        {
            rm.rewardedAd.Show((Reward reward) =>
            {
                Debug.Log("報酬が付与されました: " + reward.Type + " " + reward.Amount);
                // ここで報酬を付与する処理を追加
                callback?.Invoke(reward);
            });
        }
        else
        {
            Debug.LogError("リワード広告がまだロードされていません。");
            rm.LoadRewardedAd();
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("リワード広告が閉じられました。再ロードします。");
            LoadRewardedAd();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("リワード広告の表示中にエラーが発生しました: " + error);
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("リワード広告が表示されました。");
        };
    }



    public static void SetRewardVideo(Action<EventArgs> callback = null)
    {
        RewardManager rm = RewardManager.Instance;

        rm.LoadRewardedAd();

    }

    public static void PlayReward(Action<EventArgs> callback = null)
    {
        RewardManager.ShowRewardedAd(callback);
    }

}
public class AdsEventArgs : EventArgs
{
    public const int RESULT_SUCCESS = 0;
    public const int RESULT_FAILURE = 1;

    public int result = 0;
    public string message = "";
}
