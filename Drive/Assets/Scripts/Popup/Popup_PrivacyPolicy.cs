using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_PrivacyPolicy : BasePopup
{
    public static Popup_PrivacyPolicy Show()
    {
        GameObject o = Resources.Load("Prefabs/Common/Popup_PrivacyPolicy") as GameObject;
        GameObject prefab = Instantiate(o);
        prefab.transform.SetParent(GameObject.Find("PopupCanvas").transform, false);
        Popup_PrivacyPolicy script = prefab.GetComponent<Popup_PrivacyPolicy>();
        return script;
    }

    public void CloseEvent()
    {

        StartCoroutine(_Exit(this.gameObject));
    }

    protected override IEnumerator _Exit(GameObject prefab, bool isDestory = true)
    {
        //return base._Exit(prefab, isDestory);
        yield return new WaitForSeconds(0.3f);

        Destroy(prefab);
        if (_windowClose != null) _windowClose(true);
    }
}
