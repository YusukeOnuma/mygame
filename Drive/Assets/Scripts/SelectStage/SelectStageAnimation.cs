using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageAnimation : MonoBehaviour
{
    public float delay = 3f;
    private Animator _anim;


    public IEnumerator playAnim()
    {
        //yield return new WaitForSeconds(delay);
        _anim = GetComponent<Animator>();
        _anim.Play("StageButton", 0, 0);

        yield return null;

    }

}
