using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private CarData __cardata;
    private StageData __stageData;
    private SelectStageData __selectStageData;
    private EnemyData __enemyData;
    private EnemyStageData __enemyStageData;
    private PlayerData __playerData;
    private SpikeData __spikeData;
    private ObstacleData __obstacleData;
    private SupportData __supportData;
    private int __nowStageId;

    private int __nowGetScore;
    private int __nowGetCoin;


    public static void SetUp()
    {
        DataManager data = DataManager.Instance;
    }

    public static SpikeData spikeData
    {
        get { return DataManager.Instance.__spikeData; }
    }


    public static CarData carData
    {
        get { return DataManager.Instance.__cardata; }
    }

    public static StageData stageData
    {
        get { return DataManager.Instance.__stageData; }
    }

    public static SelectStageData selectStageData
    {
        get { return DataManager.Instance.__selectStageData; }
    }

    public static EnemyData enemyData
    {
        get { return DataManager.Instance.__enemyData; }
    }

    public static EnemyStageData enemyStageData
    {
        get { return DataManager.Instance.__enemyStageData; }
    }

    public static PlayerData playerData
    {
        get { return DataManager.Instance.__playerData; }
    }

    public static ObstacleData obstacleData
    {
        get { return DataManager.Instance.__obstacleData; }
    }

    public static SupportData supportData
    {
        get { return DataManager.Instance.__supportData; }
    }

    public static int nowStageId
    {
        set { Instance.__nowStageId = value; }
        get { return Instance.__nowStageId; }
    }
    public static int nowGetScore
    {
        set { Instance.__nowGetScore = value; }
        get { return Instance.__nowGetScore; }
    }
    public static int nowGetCoin
    {
        set { Instance.__nowGetCoin = value; }
        get { return Instance.__nowGetCoin; }
    }

    public DataManager()
    {
        __enemyData = new EnemyData();
        __enemyStageData = new EnemyStageData();
        __cardata = new CarData();
        __stageData = new StageData();
        __selectStageData = new SelectStageData();
        __spikeData = new SpikeData();
        __playerData = new PlayerData();
        __obstacleData = new ObstacleData();
        __supportData = new SupportData();
        __nowStageId = 1;
        __nowGetScore = 0;
        __nowGetCoin = 0;
    }

    /// <summary>
    /// JSONLoad
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string readJsonData(string path)
    {
        TextAsset textasset = new TextAsset();
        textasset = Resources.Load(path, typeof(TextAsset)) as TextAsset;
        string data = textasset.text;

        return data;
    }
}
