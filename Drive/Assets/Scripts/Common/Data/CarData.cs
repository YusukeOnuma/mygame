using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class CarMaster
{
    /// <summary>
    /// 武器ID
    /// </summary>
    public int carId;
    /// <summary>
    /// 武器名
    /// </summary>
    public string name;
    /// <summary>
    /// 武器の説明
    /// </summary>
    public string description;
    /// <summary>
    /// 武器の値段
    /// </summary>
    public int price;
    /// <summary>
    /// 武器装備による移動速度
    /// </summary>
    public float movingSpeed;
    /// <summary>
    /// 武器にスペシャルがあるか
    /// </summary>
    public int special;
    /// <summary>
    /// 武器によるコイン獲得時の乗数
    /// </summary>
    public float coinMultiplier;
    /// <summary>
    /// 武器による攻撃スピード
    /// </summary>
    public float atkSpeed;

    public int durability;

    public int satrPoints;
}

public class CarData
{
    private List<CarMaster> __carMasters = new List<CarMaster>();


    public void SetMasterFromJson(string json)
    {
        __carMasters.AddRange(Jsonhelper.FromJson<CarMaster>(json));
    }

    public List<CarMaster> GetCarMasters
    {
        get { return __carMasters; }
    }



    public CarMaster GetCarMasterById(int id)
    {
        CarMaster cm = new CarMaster();

        for(int i = 0; i < __carMasters.Count; i++)
        {
            if(__carMasters[i].carId == id)
            {
                cm = __carMasters[i];
                break;
            }
        }

        return cm;
    }
}
