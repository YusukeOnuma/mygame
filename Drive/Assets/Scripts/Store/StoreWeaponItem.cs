
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCarItem : MonoBehaviour
{

    public AtlasImageManager sprite = null;

    private StoreScene __storeScene = null;

    private CarMaster __carData;


    public void Init(StoreScene scene,CarMaster carData)
    {
        __storeScene = scene;
        __carData = carData;

        sprite.Name = "car" + __carData.carId.ToString();

        ////Debug.Log("sprite.flexibleWidth :: " + sprite.flexibleWidth);
        ////Debug.Log("sprite.flexibleWidth :: " + sprite.minWidth);
        ////Debug.Log("sprite.preferredWidth :: " + sprite.preferredWidth);

        float x = (sprite.preferredWidth / 2.0f);

        ////Debug.Log("XXXX ::: " + x);


        sprite.transform.localPosition = new Vector3(x, sprite.transform.localPosition.y, sprite.transform.localPosition.z);


    }


    public void ClickButtonEvent()
    {
        if (DataManager.playerData.GetCarData(__carData.carId) < 0)
        {
            __storeScene.OpenPopup(__carData.name.ToString(),"","Buy","Cancel", __carData.price.ToString(), __carData.carId, __carData.price);
        }
        else {
            __storeScene.OpenPopup(__carData.name.ToString(), "", "Equip","Cancel",null, __carData.carId, __carData.price);
        }
       
    }
}
