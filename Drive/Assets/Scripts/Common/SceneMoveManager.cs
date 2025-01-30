using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneMoveManager : Singleton<SceneMoveManager>
{
    private GameObject __loadingObj = null;

    private bool __isAnimaton = false;

    // Start is called before the first frame update
    public void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public void Transfer(string SceneName)
    {


        StartCoroutine(__Transfer(SceneName));
    }

    private IEnumerator __Transfer(string SceneName)
    {
        if (__isAnimaton == true) yield break;
        __isAnimaton = true;

        float alpha = 0.0f;
        if (__loadingObj == null)
        {
            __LoadingObj();
        }
        else
        {
            __loadingObj.gameObject.SetActive(true);
        }
        Image image = __loadingObj.GetComponentInChildren<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        while (image.color.a < 1.0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            alpha += 0.1f;
            yield return null;
        }

        SceneManager.LoadScene(sceneName: SceneName);

        yield return new WaitForSeconds(1.0f);

        alpha = 1.0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        while (image.color.a > 0.0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            alpha -= 0.1f;
            yield return null;
        }

        __loadingObj.gameObject.SetActive(false);
        __isAnimaton = false;

    }

    private void __LoadingObj()
    {
        GameObject obj = Resources.Load("Prefabs/Common/Loading") as GameObject;
        __loadingObj = Instantiate(obj);
        Vector3 pos = __loadingObj.transform.localPosition;
        Vector3 sclae = __loadingObj.transform.localScale;
        GameObject.DontDestroyOnLoad(__loadingObj);
        //            __loadingObj.transform.SetParent(GameObject.Find("Canvas").transform);
        __loadingObj.transform.localPosition = pos;
        __loadingObj.transform.localScale = sclae;

    }

    public bool IsAnimatin
    {
        get { return __isAnimaton; }
    }
}
