using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyMaster
{
    public int enemyId;
    public string name;
    public int damage;
    public int speed;
    public int hitpoint;
}

public class EnemyData
{
    private List<EnemyMaster> __enemyMasters = new List<EnemyMaster>();


    public void SetMasterFromJson(string json)
    {
        __enemyMasters.AddRange(Jsonhelper.FromJson<EnemyMaster>(json));
    }

    public List<EnemyMaster> GetenemyMasters
    {
        get { return __enemyMasters; }
    }



    public EnemyMaster GetenemyMasterById(int id)
    {
        EnemyMaster wm = new EnemyMaster();

        for (int i = 0; i < __enemyMasters.Count; i++)
        {
            if (__enemyMasters[i].enemyId == id)
            {
                wm = __enemyMasters[i];
                break;
            }
        }

        return wm;
    }
}
