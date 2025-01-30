using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : BasePopup
{
    public BaseScene temp;

    private void Start()
    {
        temp = new BaseScene();
        BaseSound.LoadSe("submit", "SE/decision6");
        BaseSound.LoadSe("cancel", "SE/cancel3");
    }

    public void OnClickRestart()
    {
        BaseSound.PlaySe("submit", 0);
        temp.Transfer("Stage");
        Destroy(temp);
    }

    public void OnClickStageSelect()
    {
        BaseSound.PlaySe("submit", 0);
        temp.Transfer("SelectStage");
        Destroy(temp);
    }

    


}
