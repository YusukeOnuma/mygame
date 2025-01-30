using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Progress : BasePopup
{


    public delegate void WindowDelegate(string type);

    private WindowDelegate _windowDelegate = null;


    /// <summary>
    /// return GameObject
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static Popup_Progress Show(WindowDelegate btnCallBack = null, WindowOpen callBack = null)
    {
        GameObject o = Resources.Load("Prefabs/Common/Popup_Progress") as GameObject;
        GameObject prefab = Instantiate(o);
        prefab.transform.SetParent(GameObject.Find("PopupCanvas").transform, false);
        Popup_Progress script = prefab.GetComponent<Popup_Progress>();
        script._animator = prefab.GetComponent<Animator>();

        script.StartCoroutine(script.PlayOpen());

        return script;
    }

    private IEnumerator PlayOpen()
    {

        yield return new WaitForSeconds(0.3f);


        if (_windowOpen != null) _windowOpen(true);
    }


    public void OnClickEvent(string target)
    {
        if (_windowDelegate != null)
        {
            _windowDelegate(target);
        }
        //Exit(this.gameObject);

    }

    public void Exit()
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
