using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpikeMaster 
{
    /// <summary>
    /// ステージID
    /// </summary>
    public int stageId;
    /// <summary>
    /// ダメージ数
    /// </summary>
    public int spikePoint;
    /// <summary>
    /// 配置個数
    /// </summary>
    public int spikeNo;
}

public class SpikeData
{
    private List<SpikeMaster> __spikeMasters = new List<SpikeMaster>();

    public void SetMasterFromJson(string json)
    {
        __spikeMasters.AddRange(Jsonhelper.FromJson< SpikeMaster>(json));
    }

    public List<SpikeMaster> GetSpikeMasters
    {
        get { return __spikeMasters; }
    }

    public SpikeMaster GetSpikeMasterById(int id)
    {
        SpikeMaster wm = new SpikeMaster();

        for (int i = 0; i < __spikeMasters.Count; i++)
        {
            if (__spikeMasters[i].stageId == id)
            {
                wm = __spikeMasters[i];
                break;
            }
        }

        return wm;
    }

}