using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Tutorial : BasePopup
{
    private const float SCROLL_NUM = 280f;


    [SerializeField]
    private GameObject __content = null;


    [SerializeField]
    private GameObject __rightBtn = null;
    [SerializeField]
    private GameObject __leftBtn = null;
    [SerializeField]
    private GameObject __closeBtn = null;



    public delegate void WindowDelegate(string type);

    private WindowDelegate _windowDelegate = null;
    private SelectStageMaster _selectStageMaster = null;
    private SelectStage __parent = null;

    private bool __isMove = false;
    private bool __isDirection = false;
    private int __nowNum = 0;

    private bool __isFlick = false;
    private bool __isTap = false;

    private Vector3 __touchStartPos;
    private Vector3 __touchEndPos;

    private int __direction = 0;

    /// <summary>
    /// return GameObject
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>

    public static Popup_Tutorial Show(WindowDelegate btnCallBack = null, WindowOpen callBack = null)
    {
        GameObject o = Resources.Load("Prefabs/Common/Popup_Tutorial") as GameObject;
        GameObject prefab = Instantiate(o);
        prefab.transform.SetParent(GameObject.Find("PopupCanvas").transform, false);
        Popup_Tutorial script = prefab.GetComponent<Popup_Tutorial>();
        script._windowDelegate = btnCallBack;
        script._windowOpen = callBack;
        script._animator = prefab.GetComponent<Animator>();
        script.StartCoroutine(script.PlayOpen());
        return script;
    }


    private IEnumerator PlayOpen()
    {
        __leftBtn.SetActive(false);


        __closeBtn.SetActive(DataManager.playerData.GetTutorial());
        yield return new WaitForSeconds(0.3f);

        if (_windowOpen != null) _windowOpen(true);
    }


    public void ClickRight()
    {
        if (__isMove) return;

        if (__nowNum == -5)
        {
            __closeBtn.SetActive(true);
            __rightBtn.SetActive(false);
        }
        if (__nowNum < -5) {
            return;
        }
        __leftBtn.SetActive(true);

        __nowNum--;
        __isDirection = true;
        StartCoroutine(__Move());
    }

    public void ClickLeft()
    {
        if (__isMove) return;
        if (__nowNum == -1)
            __leftBtn.SetActive(false);

        if (__nowNum > -1) {
            return;
        }
        __rightBtn.SetActive(true);
        __nowNum++;
        __isDirection = false;

        StartCoroutine(__Move());

    }

    private IEnumerator __Move()
    {

        if (__isMove) yield break;

        __isMove = true;



        RectTransform self = __content.GetComponent<RectTransform>();
        var size = self.localPosition;

        float nextPoint = SCROLL_NUM * __nowNum;

        if (__isDirection)
        {
            while (size.x > nextPoint)
            {
                size = self.localPosition;
                size.x -= (Time.deltaTime * 1000f);
                self.localPosition = size;
                yield return null;
            }
            size.x = nextPoint;
            self.localPosition = size;
        }
        else
        {
            while (size.x < nextPoint)
            {
                size = self.localPosition;
                size.x += (Time.deltaTime * 1000f);
                self.localPosition = size;
                yield return null;
            }

            size.x = nextPoint;
            self.localPosition = size;

        }


        __isMove = false;
        yield return null;
    }




    public void OnClickEvent(string target)
    {
        BaseSound.PlaySe("cancel", 0);
        if (_windowDelegate != null)
        {
            _windowDelegate(target);
        }
        Exit(this.gameObject);

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            __isFlick = true;
            __touchStartPos = new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        Input.mousePosition.z);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            __touchEndPos = new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        Input.mousePosition.z);
            if (__touchStartPos != __touchEndPos)
            {
                TapOff();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            __touchEndPos = new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        Input.mousePosition.z);
            //Debug.Log(__touchEndPos);
            if (IsFlick())
            {
                float directionX = __touchEndPos.x - __touchStartPos.x;
                float directionY = __touchEndPos.y - __touchStartPos.y;
                if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
                {
                    if (0 < directionX)
                    {
                        ClickLeft();
                        __direction = 6;
                    }
                    else
                    {
                        ClickRight();
                        __direction = 4;
                    }
                }
                else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
                {
                    if (0 < directionY)
                    {
                        __direction = 8;
                    }
                    else
                    {
                        __direction = 2;
                    }
                }
                else
                {
                    FlickOff();
                }
            }
            else
            {
                __direction = 5;
            }
        }
    }

    public void FlickOff()
    {
        __direction = 5;
        __isFlick = false;
    }

    public bool IsFlick()
    {
        return __isFlick;
    }

    public void TapOn()
    {
        __isTap = true;
        Invoke("TapOff", 0.2f);
    }

    public void TapOff()
    {
        __isTap = false;
    }

    public bool IsTap()
    {
        return __isTap;
    }
}
