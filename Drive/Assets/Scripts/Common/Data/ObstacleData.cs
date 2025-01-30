using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleMaster
{
    /// <summary>
    /// 障害物ID 1:bomb, 10:kunai, 20: syuriken
    /// </summary>
    public int obstacleId;
    /// <summary>
    /// 速度
    /// </summary>
    public int speed;
    /// <summary>
    /// プレイヤーに向かうか
    /// </summary>
    public int orbitType;
    /// <summary>
    /// 対象はスコアか、タイムか
    /// </summary>
    public int scoreOrTime;
    /// <summary>
    /// 加減算ポイント数
    /// </summary>
    public int point;
}


public class ObstacleData : MonoBehaviour
{
    private List<ObstacleMaster> __obstacleMasters = new List<ObstacleMaster>();


    public void SetMasterFromJson(string json)
    {
        __obstacleMasters.Clear();
        __obstacleMasters.AddRange(Jsonhelper.FromJson<ObstacleMaster>(json));
    }

    public List<ObstacleMaster> GetObstacleMasters
    {
        get { return __obstacleMasters; }
    }



    public List<ObstacleMaster> GetObstacleMasterByIds(string ids)
    {
        List<ObstacleMaster> om = new List<ObstacleMaster>();
        List<string> idArr = new List<string>();
        idArr.AddRange(ids.Split(','));

        for (int i = 0; i < __obstacleMasters.Count; i++)
        {
            for (int j = 0; j < idArr.Count; j++)
            {
                if (__obstacleMasters[i].obstacleId == int.Parse(idArr[j]))
                {
                    om.Add(__obstacleMasters[i]);
                }
            }
        }

        return om;
    }
}
