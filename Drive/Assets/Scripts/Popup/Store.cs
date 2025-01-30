using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : BasePopup{

    public void Start()
    {
        BaseSound.LoadSe("submit", "SE/decision6");
        BaseSound.LoadSe("cancel", "SE/cancel3");

    }

    public void Close()
    {
        BaseSound.PlaySe("cancel", 0);
        Exit(this.gameObject);
    }


    public void BuyCar() {
        BaseSound.PlaySe("submit", 0);
        //coin reduce, car unlock

    }

    public void EquipCar() {
        BaseSound.PlaySe("submit", 0);
        //equip car
    }

}
