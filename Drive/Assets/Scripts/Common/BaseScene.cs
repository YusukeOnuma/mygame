using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BaseScene :Singleton<MonoBehaviour>
{
    private GameObject __loadingObj = null;


    // Start is called before the first frame update
    public void Transfer(string SceneName)
    {
        SceneMoveManager.Instance.Transfer(SceneName);
    }


}

   
