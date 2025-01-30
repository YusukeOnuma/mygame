using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings: BaseScene
{
    private float __bgmVolume;
    private float __seVolume;
    [SerializeField]
    private Slider __bgmSlider;

    [SerializeField]
    private Slider __seSlider;
    [SerializeField]
    private Image __bgImg;

    private IEnumerator Start()
    {
        __bgmSlider.value = DataManager.playerData.GetBGMVol();
        __seSlider.value = DataManager.playerData.GetSEVol();

        __bgImg.SetNativeSize();

        while (SceneMoveManager.Instance.IsAnimatin == true)
        {
            yield return null;
        }

        //fade out end
        //__coin.text = string.Format("{0:#,0}", DataManager.playerData.GetCoinData());
    }

    public void SetBGMVolume(float volume)
    {
        __bgmVolume = volume;
        BaseSound.SetValBgm(volume);
        //volume control
        
        
    }
    public void SetSEVolume(float volume)
    {
        __seVolume = volume;
        BaseSound.SetValSe(volume);
        //volume control
    }

    public void SetLanguage() {

        //set language
    }

    public void Close() {
        DataManager.playerData.SetBGMVol(__bgmVolume);
        DataManager.playerData.SetSEVol(__seVolume);
        BaseSound.PlaySe("cancel", 0);
        SceneMoveManager.Instance.Transfer("MainMenu");
    }

    public void Tutorial()
    {
        BaseSound.PlaySe("submit", 0);
        Popup_Tutorial.Show((string type) =>
        {
        });
    }
    public void PrivacyPolicy() {
        BaseSound.PlaySe("submit", 0);
        Popup_PrivacyPolicy.Show();
        
    }


}

   


