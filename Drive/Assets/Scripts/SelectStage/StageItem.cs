using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageItem : MonoBehaviour
{

    private List<SelectStageAnimation> __selectStageAnimations = null;

    public void Init()
    {
        __selectStageAnimations = new List<SelectStageAnimation>();
        __selectStageAnimations.AddRange(this.gameObject.GetComponentsInChildren<SelectStageAnimation>());

        foreach (SelectStageAnimation s in __selectStageAnimations)
        {
            StartCoroutine(s.playAnim());
        }
    }
}
