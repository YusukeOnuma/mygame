using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_StartGame : BasePopup
{
    public Text _title = null;

    public Text _score1Text = null;
    public Text _score1Coin = null;
    public Text _score2Text = null;
    public Text _score3Text = null;

    private int _levelNo = 0;
    private int _score1Star = 0;
    private int _score2Star = 0;
    private int _score3Star = 0;

    public delegate void WindowDelegate(string type);

    private WindowDelegate _windowDelegate = null;
    private SelectStageMaster _selectStageMaster = null;
    private SelectStage __parent = null;




    /// <summary>
    /// return GameObject
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>

    public static Popup_StartGame Show(string t, SelectStageMaster stageMaster, WindowDelegate btnCallBack = null, WindowOpen callBack = null)
    {
        GameObject o = Resources.Load("Prefabs/Common/Popup_StartGame") as GameObject;
        GameObject prefab = Instantiate(o);
        prefab.transform.SetParent(GameObject.Find("PopupCanvas").transform, false);
        Popup_StartGame script = prefab.GetComponent<Popup_StartGame>();
        script._windowDelegate = btnCallBack;
        script._windowOpen = callBack;

        script._selectStageMaster = stageMaster;

        script._levelNo = (int)stageMaster.stageId;
        script._title.text = "STAGE " + script._levelNo;

        script._score1Star = (int)stageMaster.clearScore1;
        script._score1Text.text = "" + script._score1Star + "m";

        script._score2Star = (int)stageMaster.clearScore2;
        script._score2Text.text = "" + script._score2Star + "m";

        //script._score3Star = (int)stageMaster.clearScore3;
        script._score3Text.text = "Clear";
        script._score1Coin.text = "100";




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
