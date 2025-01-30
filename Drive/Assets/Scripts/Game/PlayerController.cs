using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private int car_speed = 0;


    [SerializeField]
    float[] pos;

    private GameController gameController = null;

    private bool _isFlag = false;
    private bool _isTap = false;

    private float spped = 0;
    private int hp = 0;
    private CarMaster nowCar = null;

    private GameObject cameraObj = null;

    //Assets/Resources/Prefabs/Game/PlayerCar/Car_1.prefab

    // Use this for initialization
    public void SetUp (GameController controller) {
        spped = Time.deltaTime * 10f;
        gameController = controller;

        int carId = DataManager.playerData.NowSelectedCarId();

        nowCar = DataManager.carData.GetCarMasterById(carId);

        car_speed = (int)nowCar.movingSpeed;

        cameraObj = transform.Find("Main Camera").gameObject;


        GameObject o = Resources.Load("Prefabs/Game/PlayerCar/Car_" + carId) as GameObject;
        GameObject prefab = Instantiate(o);
        prefab.transform.SetParent(transform, false);
        prefab.name = "Player";


    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(OnTapEvent());
        }
        if (gameController != null)
        {
            // スピードを徐々に上げる処理
            spped += Time.deltaTime * 0.01f; // 0.5f は加速度（調整可能）
        }
        //Debug.Log("speed :::: " + spped);

    }

    public void UpdateDistance(float distance)
    {
        this.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + distance);
    }


    public IEnumerator OnTapEvent()
    {
        if (_isTap) yield break;
        _isTap = true;

        float startX = transform.localPosition.x;
        float targetX = _isFlag ? pos[0] : pos[1];
        _isFlag = !_isFlag;

        float duration = 0.3f; // 0.3秒で移動
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            t = Mathf.SmoothStep(0, 1, t); // Ease-out: 最初は速く、終わりにゆっくり

            transform.localPosition = new Vector3(
                Mathf.Lerp(startX, targetX, t), // X軸をスムーズに補間
                transform.localPosition.y,     // Yはそのまま
                transform.localPosition.z      // Zもそのまま
            );

            yield return null;
        }

        // 最後にターゲット位置を正確にセット
        transform.localPosition = new Vector3(targetX, transform.localPosition.y, transform.localPosition.z);
        _isTap = false;
    }


    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <returns></returns>
    public void Damage(int value)
    {
        gameController.DamageBody(value);
    }

    public void Explosions(Transform tm)
    {
        gameController.Explosions(tm);

    }


    public float Speed{
        get { return spped; }
    }

    public int Maxbody
    {
        get { return nowCar.durability; }
    }

    public void Goal()
    {
        cameraObj.transform.SetParent(transform.parent);
    }
}
