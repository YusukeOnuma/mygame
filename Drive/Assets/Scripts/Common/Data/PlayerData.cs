using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    /// <summary>
    /// Coin
    /// </summary>
    /// <param name="num"></param>
    public void SetCoinData(int num)
    {
        PlayerPrefs.SetInt(Const.GET_COIN_NUM_KEY, num);
    }

    public int GetCoinData()
    {
        return PlayerPrefs.GetInt(Const.GET_COIN_NUM_KEY, 0);
    }

    /// <summary>
    /// Car
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type">購入していない:-1、購入済み:0、装備中:1</param>
    public void SetCarData(int id, int type)
    {
        string key = string.Format(Const.WEAPON_NAME_KEY, id);
        PlayerPrefs.SetInt(key, type);
    }

    public int GetCarData(int id)
    {
        string key = string.Format(Const.WEAPON_NAME_KEY, id);
        return PlayerPrefs.GetInt(key, -1);
    }

    public int NowSelectedCarId()
    {
        var cars = DataManager.carData.GetCarMasters;
        int carId = 1;
        for (int i = 0; i < cars.Count; i++) {
            if (GetCarData(cars[i].carId) == 1)
            {
                carId = cars[i].carId;
                break;
            }

        }

        return carId;
    }

    /// <summary>
    /// Score
    /// </summary>
    /// <param name="id"></param>
    /// <param name="score">アンロックしていない:-1、アンロック済み>=0</param>
    public void SetScoreData(int id, int score)
    {
        string key = string.Format(Const.STAGE_NAME_KEY, id);
        PlayerPrefs.SetInt(key, score);
    }

    public int GetScoreData(int id)
    {
        string key = string.Format(Const.STAGE_NAME_KEY, id);

        int num = PlayerPrefs.GetInt(key, -1);

        return num;
    }

    public void SetBGMVol(float vol)
    {
        PlayerPrefs.SetFloat(Const.SOUND_BGM_KEY, vol);
    }

    public float GetBGMVol()
    {
        return PlayerPrefs.GetFloat(Const.SOUND_BGM_KEY, 0.5f);
    }

    public void SetSEVol(float vol)
    {
        PlayerPrefs.SetFloat(Const.SOUND_SE_KEY, vol);
    }

    public float GetSEVol()
    {
        return PlayerPrefs.GetFloat(Const.SOUND_SE_KEY, 0.5f);
    }


    public void SetTutorial()
    {
        PlayerPrefs.SetInt(Const.TUTORIAL_FLAG_KEY, 1);
    }

    public bool GetTutorial()
    {
        return PlayerPrefs.GetInt(Const.TUTORIAL_FLAG_KEY) == 1 ? true : false;
    }

    public void SetClearCountData(int num)
    {
        PlayerPrefs.SetInt(Const.CELAR_ADMOB_KEY, num);
    }

    public int GetClearCountData()
    {
        return PlayerPrefs.GetInt(Const.CELAR_ADMOB_KEY, 0);
    }

}
