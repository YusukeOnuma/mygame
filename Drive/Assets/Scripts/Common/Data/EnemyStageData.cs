using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyStageMaster
{
    /// <summary>
    /// ステージID
    /// </summary>
    public int stageId;
    /// <summary>
    /// 配置数
    /// </summary>
    public int enemyCount;
    /// <summary>
    /// 配置するの敵ID
    /// </summary>
    public string enemyIds;
    /// <summary>
    /// 敵の割合
    /// </summary>
    public string enemyRates;


}

public class EnemyStageData
{
    private List<EnemyStageMaster> __enemyStageMasters = new List<EnemyStageMaster>();


    public void SetMasterFromJson(string json)
    {
        __enemyStageMasters.AddRange(Jsonhelper.FromJson<EnemyStageMaster>(json));
    }

    public List<EnemyStageMaster> GetEnemyStageMasters
    {
        get { return __enemyStageMasters; }
    }



    public EnemyStageMaster GetEnemyStageMasterById(int id)
    {
        EnemyStageMaster wm = new EnemyStageMaster();

        for (int i = 0; i < __enemyStageMasters.Count; i++)
        {
            if (__enemyStageMasters[i].stageId == id)
            {
                wm = __enemyStageMasters[i];
                break;
            }
        }

        return wm;
    }
}
