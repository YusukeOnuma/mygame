using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SupportMaster
{
    /// <summary>
    /// 障害物ID
    /// </summary>
    public int supportId;
    /// <summary>
    /// 速度
    /// </summary>
    public int speed;
    /// <summary>
    /// どの項目がUPするか
    /// </summary>
    public string statusUpType;
    /// <summary>
    /// 加減算ポイント数
    /// </summary>
    public int point;


}

public class SupportData : MonoBehaviour
{
    private List<SupportMaster> __supportMasters = new List<SupportMaster>();


    public void SetMasterFromJson(string json)
    {
        __supportMasters.Clear();
        __supportMasters.AddRange(Jsonhelper.FromJson<SupportMaster>(json));
    }

    public List<SupportMaster> GetSupportMasters
    {
        get { return __supportMasters; }
    }



    public List<SupportMaster> GetSupportMasterByIds(string ids)
    {
        List<SupportMaster> sm = new List<SupportMaster>();
        List<string> idArr = new List<string>();
        idArr.AddRange(ids.Split(','));

        for (int i = 0; i < __supportMasters.Count; i++)
        {

            for (int j = 0; j < idArr.Count; j++)
            {
                if (__supportMasters[i].supportId == int.Parse(idArr[j]))
                {
                    sm.Add(__supportMasters[i]);
                }
            }
        }

        return sm;
    }
}
