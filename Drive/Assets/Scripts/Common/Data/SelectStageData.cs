using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SelectStageMaster
{
    /// <summary>
    /// ステージID
    /// </summary>
    public int stageId;
    /// <summary>
    /// 星1獲得するために必要なスコア
    /// </summary>
    public int clearScore1;
    /// <summary>
    /// 星2獲得するために必要なスコア
    /// </summary>
    public int clearScore2;
    /// <summary>
    /// 星3獲得するために必要なスコア
    /// </summary>
    public int clearScore3;
    /// <summary>
    /// アンロックに必要なコイン
    /// </summary>
    public int unlockCoin;

}

public class SelectStageData
{
    private List<SelectStageMaster> __selectStageMasters = new List<SelectStageMaster>();


    public void SetMasterFromJson(string json)
    {
        __selectStageMasters.Clear();
        __selectStageMasters.AddRange(Jsonhelper.FromJson<SelectStageMaster>(json));
    }


    public int GetMaxStageId
    {
        get
        {
            int id = __selectStageMasters[(__selectStageMasters.Count - 1)].stageId;
            return id;
        }

    }

    public List<SelectStageMaster> GetSelectStageMasters
    {
        get { return __selectStageMasters; }
    }



    public SelectStageMaster GetSelectStageMasterById(int id)
    {
        SelectStageMaster wm = null;

        for (int i = 0; i < __selectStageMasters.Count; i++)
        {
            if (__selectStageMasters[i].stageId == id)
            {
                wm = __selectStageMasters[i];
                break;
            }
        }


        return wm;
    }

}
