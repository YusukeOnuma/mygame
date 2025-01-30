using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

public class Popup_EquipConfirm : BasePopup
{
    public Text title = null;

    public Text description = null;

    public TextMeshProUGUI yesBtn = null;

    public TextMeshProUGUI noBtn = null;

    public delegate void WindowDelegate(string type);

    private WindowDelegate _windowDelegate = null;


    /// <summary>
    /// return GameObject
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static Popup_EquipConfirm Show(string t, string d, string yes = "Yes", string no = "No", WindowDelegate btnCallBack = null, WindowOpen callBack = null)
    {
        GameObject o = Resources.Load("Prefabs/Common/Popup_EquipConfirm") as GameObject;
        GameObject prefab = Instantiate(o);
        prefab.transform.SetParent(GameObject.Find("PopupCanvas").transform, false);
        Popup_EquipConfirm script = prefab.GetComponent<Popup_EquipConfirm>();
        script._windowDelegate = btnCallBack;
        script._windowOpen = callBack;
        script.title.text = t;
        script.description.text = d;
        script.yesBtn.text = yes;
        script.noBtn.text = no;
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
