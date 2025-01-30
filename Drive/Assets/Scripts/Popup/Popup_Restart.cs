using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Popup_Restart : BasePopup
{

    public delegate void WindowDelegate(string type);

    private WindowDelegate _windowDelegate = null;


    /// <summary>
    /// return GameObject
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static Popup_Restart Show(string t, SelectStageMaster stageMaster, WindowDelegate btnCallBack = null, WindowOpen callBack = null)
    {
        GameObject o = Resources.Load("Prefabs/Common/Popup_Restart") as GameObject;
        GameObject prefab = Instantiate(o);
        prefab.transform.SetParent(GameObject.Find("PopupCanvas").transform, false);
        Popup_Restart script = prefab.GetComponent<Popup_Restart>();
        script._windowDelegate = btnCallBack;
        script._windowOpen = callBack;

        script._animator = prefab.GetComponent<Animator>();

        script.StartCoroutine(script.PlayOpen());

        return script;
    }

    private IEnumerator PlayOpen()
    {
        _animator.SetBool("close", false);
        _animator.Play("Popup_open");

        yield return new WaitForSeconds(0.3f);

        while (_animator.GetCurrentAnimatorStateInfo(0).length > _animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            yield return null;
        }

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

    public void CloseEvent()
    {
        StartCoroutine(_Exit(this.gameObject));
    }

    protected override IEnumerator _Exit(GameObject prefab, bool isDestory = true)
    {
        //return base._Exit(prefab, isDestory);

        _animator = prefab.GetComponent<Animator>();
        _animator.SetBool("close", true);
        _animator.Play("Popup_close");

        yield return new WaitForSeconds(0.3f);

        while (_animator.GetCurrentAnimatorStateInfo(0).length > _animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            yield return null;
        }


        Destroy(prefab);
        if (_windowClose != null) _windowClose(true);
    }
}
