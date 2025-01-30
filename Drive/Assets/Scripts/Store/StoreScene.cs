using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StoreScene : BaseScene
{

    const int car_DUMMY_CONT = 10;

    //[SerializeField]
    //private AtlasImageManager __window = null;
    //[SerializeField]
    //private AtlasImageManager __car = null;
    [SerializeField]
    private AtlasImageManager __coin = null;
    [SerializeField]
    private GameObject __content = null;
    [SerializeField]
    private Text __coins = null;

    [SerializeField]
    private GameObject __originObj = null;
    [SerializeField]
    private GameObject __SphereObj = null;

    [SerializeField]
    private TextMeshProUGUI _carName = null;

    [SerializeField]
    private Scrollbar _durabilityBar = null;
    [SerializeField]
    private Scrollbar _speedBar = null;
    [SerializeField]
    private GameObject _nowSelectedCar = null;
    [SerializeField]
    private GameObject _buyButton = null;



    private Popup_Car __wpopup = null;
    private bool __isWait = false;

    private List<CarMaster> __cardata = null;

    private Dictionary<int, GameObject> __carObjs = null;
    private Popup_EquipConfirm __yesNo = null;
    private bool __isWait2 = false;

    private int __carId;
    private int __carPrice;


    private List<GameObject> Cars = new List<GameObject>();
    private int __nowCarNumver = 0;


    
    
    private IEnumerator Start()
    {
        //Debug.Log(DataManager.playerData.GetCoinData());


        //__cam[1].gameObject.SetActive(false);

        //BaseSound.LoadSe("submit", "SE/decision6");
        //BaseSound.LoadSe("cancel", "SE/cancel3");
        //Sprite equip = Resources.Load("Sprites/yes", typeof(Sprite)) as Sprite;

        //GameObject o = Resources.Load("Prefabs/Common/Popup_YesNo") as GameObject;
        //__popup = Instantiate(o);
        //__popup.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        //__yesNo = __popup.GetComponent<Popup_YesNo>();
        //__popup.SetActive(false);


        foreach (Transform child in __SphereObj.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.name.StartsWith("Car")) // 親オブジェクト自体を除外
            {
                //Debug.Log(child.gameObject.name);
                Cars.Add(child.gameObject);
            }
        }




        //__window.Name = "makimono1_op2";
        //__car.Name = "makimono1_op";
        //__car.SetNativeSize();
        __coin.Name = "makimono2";
        __coin.SetNativeSize();




        __cardata = DataManager.carData.GetCarMasters;
        __carObjs = new Dictionary<int, GameObject>();

        __coins.text = string.Format("{0:#,0}", DataManager.playerData.GetCoinData());

        //CarMaster master = DataManager.carData.GetCarMasterById(__cardata[i].carId);

        RectTransform self = __content.GetComponent<RectTransform>();
        var size = self.sizeDelta;
        size.y = (90f * __cardata.Count);
        self.sizeDelta = size;

        StartCoroutine(RotateContinuously());
        


        Debug.Log(__cardata.Count);
        //for (int i=0;i<__cardata.Count;i++) {
        //    GameObject obj = Instantiate(__originObj);
        //    GameObject description = obj.transform.Find("carName").gameObject;
        //    description.GetComponent<Text>().text = __cardata[i].name.ToString();
        //    GameObject car = obj.transform.Find("Car1").gameObject;
        //    GameObject carBox = obj.transform.Find("carBox").gameObject;
        //    Image carImage = carBox.transform.Find("carImage").GetComponent<Image>();
        //    carImage.SetNativeSize(); 
        //    float mSpeed = __cardata[i].movingSpeed;
        //    float aSpeed = __cardata[i].atkSpeed;


        //    int num1 = (int)Mathf.RoundToInt(mSpeed + aSpeed) / 40;
        //    int num = __cardata[i].satrPoints;
        //    Sprite StrPoints = Resources.Load("Sprites/win_star", typeof(Sprite)) as Sprite;
        //    carImage.SetNativeSize();
        //    for (int q = 1; q < num; q++)
        //    {
        //        Image star_q = obj.transform.Find("star_"+q).GetComponent<Image>();
        //        star_q.GetComponent<Image>().sprite = StrPoints;
        //    }


        //    Vector2 pos = __originObj.GetComponent<RectTransform>().anchoredPosition;
        //    Vector3 scale = __originObj.transform.localScale * 0.7f;
        //    obj.transform.SetParent(__content.transform);
           
        //    //obj.transform.localPosition = pos;
        //    //pos.x = 0f;
        //    //pos.y = (size.y / 2 - 40f) - (90f * i);
            

        //    obj.transform.localPosition= new Vector3(obj.transform.position.x, obj.transform.position.y, 0f);
        //    obj.transform.localScale = scale;
        //   // pos = obj.transform.localPosition;
        //    obj.name = "Car_" + (i + 1);
        //   // obj.transform.localPosition = __originObj.transform.position;
        //    //Debug.Log(obj.transform.localPosition);
        //    __carObjs.Add(__cardata[i].carId ,obj);


        //    GameObject eqp= obj.transform.Find("Equiped").gameObject;
        //    eqp.SetActive(false);
        //    carImage.SetNativeSize();



        //    if (DataManager.playerData.GetCarData(__cardata[i].carId) == 0)
        //    {
        //        GameObject btn;
        //        btn = obj.transform.Find("Button").gameObject;
        //        Image img = btn.GetComponent<Image>();
        //        //Image coinImg = btn.transform.Find("CoinImg").GetComponent<Image>();
        //        Image coinImg = obj.transform.Find("CoinImg").GetComponent<Image>();
        //        carImage.SetNativeSize();

        //        img.color = new Color(img.color.r, img.color.g, img.color.b, 0.01f);
        //        coinImg.color = new Color(coinImg.color.r, coinImg.color.g, coinImg.color.b, 0.01f);
        //        //Destroy(coinImg);
        //        //Destroy(img);
        //        // btn.GetComponentInChildren<Text>().text = "";
        //        obj.transform.Find("Text").GetComponent<Text>().text = "";
        //        carImage.SetNativeSize();
        //    }

        //    else if (DataManager.playerData.GetCarData(__cardata[i].carId) > 0) {
        //        GameObject btn;
        //        btn = obj.transform.Find("Button").gameObject;

        //        Image img = btn.GetComponent<Image>();
        //        Image coinImg = obj.transform.Find("CoinImg").GetComponent<Image>();
        //        carImage.SetNativeSize();

        //        img.color = new Color(img.color.r, img.color.g, img.color.b, 0.01f);
        //        coinImg.color = new Color(coinImg.color.r, coinImg.color.g, coinImg.color.b, 0.01f);
        //        //Destroy(coinImg);
        //        //Destroy(img);
        //        //btn.GetComponentInChildren<Text>().text = "";
        //        obj.transform.Find("Text").GetComponent<Text>().text = "";



        //        eqp.SetActive(true);
        //        carImage.SetNativeSize();
        //    }

        //    else if(DataManager.playerData.GetCarData(__cardata[i].carId) < 0)
        //    {
        //        GameObject btn1;
        //        btn1 = obj.transform.Find("Button").gameObject;
        //        GameObject textBtn = obj.transform.Find("Text").gameObject;
        //        textBtn.GetComponent<Text>().text = __cardata[i].price.ToString();
        //        carImage.SetNativeSize();

        //        //if (__cardata[i].price > DataManager.playerData.GetCoinData())
        //        //{

        //        //    btn1.GetComponent<Button>().interactable = false;

        //        //}

        //    }
        //        obj.GetComponent<StoreCarItem>().Init(this,__cardata[i]);
        //    carImage.SetNativeSize();
        //}

        //__originObj.SetActive(false);
        

        while (SceneMoveManager.Instance.IsAnimatin == true)
        {
            yield return null;
        }

        //fade out end

    }
   


    public void OnClickHome()
    {
        

        BaseSound.PlaySe("cancel", 0);
        Transfer("MainMenu");
    }

    public void OnClickCar()
    {
       
    }

    public void OnClickCoin()
    {
        //__window.Name = "makimono2_op2";
        //__car.Name = "makimono1";
        //__car.SetNativeSize();
        __coin.Name = "makimono2_op";
        __coin.SetNativeSize();

    }


    public void OnClickLockedStage()
    {
        BaseSound.PlaySe("submit", 0);
    }

    //popup display
    public void OpenPopup(string t, string d,string yes, string no,string priceDisplay,int carId,int carPrice)
    {
        if (__isWait) return;
        //__cam[0].enabled = false;
        //__cam[1].gameObject.SetActive(true);
        __isWait = true;
        __carId = carId;
        __carPrice = carPrice;
        //__wpopup = Popup_Car.Show(t, d,priceDisplay,carId,yes,no, CallBack, (bool flg)=> {
        //    __isWait = false;
            
        //});
    }

    



    public void CallBack(string type, int carId, int price)
    {
        if (__isWait) return;
        __isWait = true;
        if (type == "Yes") {
            if (DataManager.playerData.GetCarData(carId) < 0)
            {
                if (DataManager.playerData.GetCoinData() >= price)
                {
                    __yesNo = Popup_EquipConfirm.Show("Equip Car", "Do you want to equip this car ?", "Equip", "Cancel", CallBack2, (bool flg) => {
                        __isWait2 = false;
                    });
                    DataManager.playerData.SetCarData(carId, 0);
                    DataManager.playerData.SetCoinData(DataManager.playerData.GetCoinData() - price);
                    //SceneManager.LoadScene("Store");
                    __ReloadCar();
                }
                else { 
                    Popup_OneBtn.Show("NOT ENOUGH COIN", (price - DataManager.playerData.GetCoinData()) + " not enough");
                }
            }
               
            else if (DataManager.playerData.GetCarData(carId)==0)
            {
                foreach (var item in __cardata) {
                    if (DataManager.playerData.GetCarData(item.carId) == 1) {
                        DataManager.playerData.SetCarData(item.carId, 0);
                    }
                }
                DataManager.playerData.SetCarData(carId, 1);
                //__cam[0].enabled = true;
                //__cam[1].gameObject.SetActive(false);
                //SceneManager.LoadScene("Store");
                __ReloadCar();
            }
            
            //else if(DataManager.playerData.GetCarData(__carId) ==0)
            //{
            //    foreach (var item in __cardata) {
            //        if (item.carId==1) {
            //            DataManager.playerData.SetCarData(item.carId, 0);
            //        }
     
            //    }
            //    DataManager.playerData.SetCarData(__carId, 1);
            //}

            ////Debug.Log(__carId);
            ////Debug.Log(__carPrice);
            
            //SceneManager.LoadScene("Store");
            
        }
        else {
            //__cam[0].enabled = true;
            //__cam[1].gameObject.SetActive(false);
            //Debug.Log("pressed no");

       
        }
        //__wpopup.Exit(__wpopup.gameObject, true, (bool flg) => {
         __isWait = false;
        //});
    }

    public void CallBack2(string type)
    {
        if (__isWait2) return;
        __isWait2 = true;
    
            if (type == "Yes")
            {
                //Debug.Log("clicked yes");


            foreach (var item in __cardata)
            {
                if (DataManager.playerData.GetCarData(item.carId) == 1)
                {
                    DataManager.playerData.SetCarData(item.carId, 0);
                }
            }
            DataManager.playerData.SetCarData(__carId, 1);
            //SceneManager.LoadScene("Store");
            __ReloadCar();

        }
            else
            {
            
                //Debug.Log("clicked no");
            }
        
       
        __yesNo.Exit(__yesNo.gameObject, true, (bool flg) => {
            __isWait2 = false;

        });
    }





    private void __ReloadCar()
    {
        __coins.text = string.Format("{0:#,0}", DataManager.playerData.GetCoinData());
        StartCoroutine(RotateContinuously());
    }



    private bool isRotating = false; // 回転中かどうかのフラグ
    private float rotationAngle = 90f; // 回転させる角度
    private float rotationDuration = 0.2f; // 回転にかかる時間（秒）


    public void RightRotatte()
    {
        if (isRotating) return;

        StartCoroutine(Rotate90Degrees(90f));
        __nowCarNumver++;
        if(Cars.Count - 1 < __nowCarNumver)
        {
            __nowCarNumver = 0;
        }
    }
    public void LeftRotatte()
    {
        if (isRotating) return;

        StartCoroutine(Rotate90Degrees(-90f));
        __nowCarNumver--;
        if (__nowCarNumver < 0) {
            __nowCarNumver = Cars.Count - 1;
        }
    }



    // 指定された角度で回転させるコルーチン
    private IEnumerator Rotate90Degrees(float angle)
    {
        isRotating = true;
        float elapsedTime = 0f;

        Quaternion startRotation = __SphereObj.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0); // Y軸周りに指定された角度で回転

        while (elapsedTime < rotationDuration)
        {
            __SphereObj.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        __SphereObj.transform.rotation = endRotation;
        isRotating = false;
        StartCoroutine(RotateContinuously());
    }

    // ボタンから右回りの回転を呼び出せるようにするメソッド
    public void RotateRight()
    {
        if (!isRotating)
        {
            StartCoroutine(Rotate90Degrees(90f));
        }
    }

    private IEnumerator RotateContinuously()
    {
        var car = Cars[__nowCarNumver];
        var nowCar = __cardata[__nowCarNumver];
        _carName.text = nowCar.name;

        _durabilityBar.size = nowCar.durability / 1000f;
        _speedBar.size = nowCar.movingSpeed / 1000f;

        var state = DataManager.playerData.GetCarData(nowCar.carId);

        _nowSelectedCar.SetActive(false);
        _buyButton.SetActive(false);
        _buyButton.GetComponent<Button>().onClick.RemoveAllListeners();
        if (state == 1)
        {
            _nowSelectedCar.SetActive(true);
        }else if(state == 0)
        {
            _buyButton.SetActive(true);
            var text = _buyButton.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "Equip";
            _buyButton.GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log("carId ::: " + nowCar.carId);
                CallBack("Yes", nowCar.carId, nowCar.price); 
            });

        }
        else
        {
            _buyButton?.SetActive(true);
            var text = _buyButton.GetComponentInChildren<TextMeshProUGUI>();
            text.text = nowCar.price.ToString();
            _buyButton.GetComponent<Button>().onClick.AddListener(() => { CallBack("Yes", nowCar.carId, nowCar.price); });
        }


        Debug.Log(__cardata[__nowCarNumver].name);
        yield return new WaitForSeconds(0.2f);
        while (true) // 永遠に回転する
        {
            if (isRotating) yield break;
            float elapsedTime = 0f;
            Quaternion startRotation = car.transform.rotation;
            Quaternion endRotation = startRotation * Quaternion.Euler(0, 90f * rotationDuration, 0);

            while (elapsedTime < rotationDuration)
            {
                car.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            car.transform.rotation = endRotation;

            // 一瞬だけ待つ
            yield return null;
        }
    }
}
