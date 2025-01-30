using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Pause : BasePopup
{
    public BaseScene temp;

    private void Start()
    {
        BaseSound.LoadSe("submit", "SE/decision6");
        temp = new BaseScene();
    }

    /// <summary>
    /// return GameObject
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static Popup_Pause Show(WindowOpen callBack = null)
    {
        GameObject o = Resources.Load("Prefabs/Common/Popup_Pause") as GameObject;
        GameObject prefab = Instantiate(o);
        prefab.transform.SetParent(GameObject.Find("PopupCanvas").transform, false);
        Popup_Pause script = prefab.GetComponent<Popup_Pause>();
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

    public void OnClickNext()
    {
        BaseSound.PlaySe("submit", 0);
    }

    public void Close()
    {

        BaseSound.PlaySe("cancel", 0);
        StartCoroutine(_Exit(this.gameObject));
    }

}