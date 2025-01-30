using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{

    void Start()
    {
        BaseSound.LoadBgm("bgm", "BGM/n72");
        BaseSound.LoadSe("submit", "SE/decision6");
        BaseSound.LoadSe("bomb", "SE/bomb1");
    }


    // Start is called before the first frame update
    public void ClickBgm()
    {
        BaseSound.PlayBgm("bgm");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSe()
    {
        BaseSound.PlaySe("submit", 0);
    }
    public void ClickSe2()
    {
        BaseSound.PlaySe("bomb", 1);
    }
}
