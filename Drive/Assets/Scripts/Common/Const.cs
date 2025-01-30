using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const : MonoBehaviour
{
    //AppStoreURL
#if UNITY_ANDROID
    public const string APP_STORE = "https://play.google.com/store/apps/details?id=jp.co.aritmobile.slicemaster.application";
#elif UNITY_IPHONE
    public const string APP_STORE = "https://itunes.apple.com/jp/app/id1471757729?mt=8";
#else
    public const string APP_STORE = "https://play.google.com/store/apps/details?id=jp.co.aritmobile.slicemaster.application";
#endif


    public const string SELECTSTAGEJSONDATA = "Data/selectStageData";
    public const string STAGEJSONDATA = "Data/stageData";
    public const string WEAPONJSONDATA = "Data/carData";
    public const string SPIKEJSONDATA = "Data/spikeData";
	public const string ENEMYJSONDATA = "Data/enemyData";
    public const string ENEMYSTAGEJSONDATA = "Data/enemyStageData";
    public const string OBSTACLEJSONDATA = "Data/obstacleData";
    public const string SUPPORTJSONDATA = "Data/supportData";


    //障害物類
    public const int OBSTACLE_BOMB = 1;
    public const int OBSTACLE_KUNAI = 10;
    public const int OBSTACLE_SYURIKEN = 20;


    //Player
    public const float DEFAULT_WALK = 1f;
    public const float MAX_SPEED  = 7f;


    //PlayerPref Key
    //解放済み、スコア、{0}にはステージIDが入る
    public const string STAGE_NAME_KEY = "__STAGE_{0}__";
    //購入済み,装備中 {0}には武器IDが入る
    public const string WEAPON_NAME_KEY = "__WEAPON_{0}__";
    //現在のコイン
    public const string GET_COIN_NUM_KEY = "__COIN_NUM__";


    public const string SOUND_BGM_KEY = "__SOUND_BGM__";
    public const string SOUND_SE_KEY = "__SOUND_SE__";



    public const string TUTORIAL_FLAG_KEY = "__TUTORIAL_FLAG__";


    public const string CELAR_ADMOB_KEY = "__CELAR_ADMOB__";

}
