using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonus : BasePopup
{
    public GameObject failPopup;

    private GameObject __prefab=null;

    //private Stage __stage = null;

    public TimeBonus Show(GameObject prefab/*, Stage stage*/)
    {
        __prefab = prefab;
        _animator = prefab.GetComponent<Animator>();
        _animator.SetBool("close", false);
        GameObject newPrefab = Instantiate(prefab);
        newPrefab.transform.SetParent(GameObject.Find("PopupCanvas").transform, false);
        return newPrefab.GetComponent<TimeBonus>();
    }

    public void OnClickWatchVideo()
    {
        BaseSound.PlaySe("submit", 0);
        StartCoroutine(__PlayReward());
    }

    private IEnumerator __PlayReward()
    {

        Popup_Progress progress = Popup_Progress.Show();

        yield return new WaitForSeconds(0.5f);
        RewardManager.SetRewardVideo((EventArgs args) => { });


        yield return new WaitForSeconds(0.5f);

        RewardManager.PlayReward(
             (EventArgs args) => {
                 AdsEventArgs arg = args as AdsEventArgs;
                 if (arg.result == 0)
                 {
                     //__stage.SetRestart();
                     Exit(this.gameObject);
                 }
                 else
                 {
                 }
                 progress.Exit();
                 
             });

        yield return null;
    }

    public void OnClickBuy()
    {
        BaseSound.PlaySe("submit", 0);
        int coin = DataManager.playerData.GetCoinData();
        if (coin >= 500)
        {
            DataManager.playerData.SetCoinData(coin - 500);
            //__stage.SetRestart();
            Exit(this.gameObject);

        }
        else
        {
            Popup_OneBtn.Show("NOT COIN", (500 - coin) + " not enough");
        }

    }
    public void OnClickSkip()
    {
        //fail popup
        BaseSound.PlaySe("submit", 0);
        //Exit(this.gameObject);
        //Open(failPopup);
        SceneMoveManager.Instance.Transfer("Result");
    }

    //public Stage parent
    //{
    //    set { __stage = value; }
    //}
}
