using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StageMaster
{
    /// <summary>
    /// ステージID
    /// </summary>
    public int stageId;
    /// <summary>
    /// ステージの制限時間
    /// </summary>
    public float stageTime;
    /// <summary>
    /// 葉の落ちる間隔
    /// </summary>
    public float leafTime;
    /// <summary>
    /// 一度に落ちる最大葉数
    /// </summary>
    public int leafNum;
    /// <summary>
    /// 葉の落ちる最大スピード
    /// </summary>
    public float leafSpeed;
    /// <summary>
    /// 葉の最大獲得ポイント
    /// </summary>
    public int leafPoint;

    /// <summary>
    /// 障害物の種類
    /// </summary>
    public string obstacleIds;

    /// <summary>
    /// 障害物の落とす割合
    /// </summary>
    public string obstacleRates;
    /// <summary>
    /// 障害物の落ちる間隔
    /// </summary>
    public float obstacleTime;
    /// <summary>
    /// 一度に落ちる最大爆弾数
    /// </summary>
    public int obstacleNum;
    /// <summary>
    /// 爆弾の落ちる最大スピード
    /// </summary>
    public float obstacleSpeed;
    /// <summary>
    /// 爆弾の最大獲得ポイント
    /// </summary>
    public int obstaclePoint;
    /// <summary>
    /// サポートの種類
    /// </summary>
    public string supportIds;
    /// <summary>
    /// サポートの落とす割合
    /// </summary>
    public string supportRates;
    /// <summary>
    /// コインの落ちる間隔
    /// </summary>
    public float supportTime;
    /// <summary>
    /// 一度に落ちる最大コイン数
    /// </summary>
    public int supportNum;
    /// <summary>
    /// コインの落ちる最大スピード
    /// </summary>
    public float supportSpeed;
    /// <summary>
    /// コインの最大獲得ポイント
    /// </summary>
    public int supportPoint;
}

public class StageData
{
    private List<StageMaster> __stageMasters = new List<StageMaster>();


    public void SetMasterFromJson(string json)
    {
        __stageMasters.AddRange(Jsonhelper.FromJson<StageMaster>(json));
    }

    public List<StageMaster> GetStageMasters
    {
        get { return __stageMasters; }
    }



    public StageMaster GetStageMasterById(int id)
    {
        StageMaster wm = new StageMaster();

        for (int i = 0; i < __stageMasters.Count; i++)
        {
            if (__stageMasters[i].stageId == id)
            {
                wm = __stageMasters[i];
                break;
            }
        }

        return wm;
    }
}
