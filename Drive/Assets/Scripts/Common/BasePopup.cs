using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{

    protected Animator _animator = null;

    public delegate void WindowOpen(bool flg);
    public delegate void WindowClose(bool flg);

    protected WindowOpen _windowOpen = null;
    protected WindowClose _windowClose = null;

    public IEnumerator Open(GameObject prefab) {
        _animator = prefab.GetComponent<Animator>();
        if(_animator != null)
            _animator.SetBool("close", false);

        GameObject newPrefab=Instantiate(prefab);
        newPrefab.transform.SetParent(GameObject.Find("PopupCanvas").transform, false);
        return null;
    }

    


    public void Exit(GameObject prefab, bool isDestory = true, WindowClose callBack = null) {
        _windowClose = callBack;
        StartCoroutine(_Exit(prefab, isDestory));
    }

    protected virtual IEnumerator _Exit(GameObject prefab, bool isDestory = true)
    {
        _animator = prefab.GetComponent<Animator>();
        if (_animator != null)
        {
            _animator.SetBool("close", true);
            _animator.Play("Popup_close");

            yield return new WaitForSeconds(0.3f);

            while (_animator.GetCurrentAnimatorStateInfo(0).length > _animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                yield return null;
            }
        }

        if (isDestory)
        {
            Destroy(prefab);
        }
        else
        {
            if (_windowClose != null) _windowClose(true);

        }

    }

    public WindowOpen windowOpen
    {
        set { _windowOpen = value; }
        get { return _windowOpen; }
    }
    public WindowClose windowClose
    {
        set { _windowClose = value; }
        get { return _windowClose; }
    }

    public Animator animator
    {
        set { _animator = value; }
        get { return _animator; }
    }
}
