using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class Popup_Car : BasePopup
{
    public Text title = null;

    public Text description = null;

    public Text price = null;

    public Text noBtn = null;

    public Text movingSpeed = null;

    public Text coinMulti = null;

    public Text atkSpeed = null;

    public GameObject moveSpeedBricks=null;

    public GameObject attackSpeedBricks = null;

    public GameObject character = null;

    //public AtlasImageManager carImage=null;

    public delegate void WindowDelegate(string type);

    public Button btnYes = null;

    private WindowDelegate _windowDelegate = null;


    private GameObject __mainCam=null;
    private GameObject __cam = null;

    /// <summary>
    /// return GameObject
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static Popup_Car Show(string t, string d, string p,int carId,string yes = "Yes", string no = "No", WindowDelegate btnCallBack = null, WindowOpen callBack = null)
    {
        GameObject o = Resources.Load("Prefabs/Common/Popup_Car") as GameObject;

        Sprite carSprite = Resources.Load("Sprites/"+carId, typeof(Sprite)) as Sprite;
        Sprite blockSprite = Resources.Load("Sprites/brickFill", typeof(Sprite)) as Sprite;


        GameObject prefab = Instantiate(o);
        prefab.transform.SetParent(GameObject.Find("PopupCanvas").transform, false);
        Popup_Car script = prefab.GetComponent<Popup_Car>();
        script._windowDelegate = btnCallBack;
        script._windowOpen = callBack;
        //script.carImage.Name = "car" + carId.ToString();

        CarMaster master = DataManager.carData.GetCarMasterById(carId);

        script.__mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        script.__cam = GameObject.FindGameObjectWithTag("Camera2");

        
        script.character.SetActive(false);
        script.StartCoroutine(script.__SetCharacter(carId));


        script.title.text = t;
        script.description.text = master.description;
        script.movingSpeed.text = master.movingSpeed.ToString();
        script.atkSpeed.text = master.atkSpeed.ToString();
        script.coinMulti.text = master.coinMultiplier.ToString("F1");



        int moveSpeedCount =(int)Mathf.RoundToInt(master.movingSpeed / 10);
        int attackSpeedCount= (int)Mathf.RoundToInt(master.atkSpeed / 10);

        for (int i=0; i<moveSpeedCount;i++) {
            script.moveSpeedBricks.transform.GetChild(i).GetComponent<Image>().sprite = blockSprite;           
        }
        for (int i = 0; i < attackSpeedCount; i++)
        {
            script.attackSpeedBricks.transform.GetChild(i).GetComponent<Image>().sprite = blockSprite;
        }


        if (p != null)
        {
            script.price.text = p;

            if(int.Parse(p) > DataManager.playerData.GetCoinData())
            {
                script.btnYes.interactable = false;
                script.price.color = new Color32(222,222,222,255);
            }

        }
        else {
            script.price.text = "Equip";
            if(DataManager.playerData.GetCarData(carId) == 1)
            {
                script.btnYes.interactable = false;
                script.price.color = new Color32(222,222,222,255);

            }
        }
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

    private IEnumerator __SetCharacter(int carId) {
        yield return new WaitForSeconds(.5f);
        character.SetActive(true);
        __SetCar(carId);
    }


    private void __SetCar(int carId)
    {
        GameObject o = Resources.Load("Prefabs/Store/Cars/" + carId) as GameObject;
        GameObject prefab = Instantiate(o);
        prefab.name = carId.ToString();
        prefab.transform.SetParent(character.transform.Find("Armature/Hips/Spine/Chest/Shoulder.R/Upper Arm.R/Lower Arm.R/Hand.R/Fingers01.R/Fingers02.R").transform, false);
    }



    protected override IEnumerator _Exit(GameObject prefab, bool isDestory = true)
    {
        //return base._Exit(prefab, isDestory);


        //__cam.SetActive(false);
        //__mainCam.GetComponent<Camera>().enabled = true;
        character.SetActive(false);

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
