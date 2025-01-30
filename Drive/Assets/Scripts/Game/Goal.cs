using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    private bool isGoal = false;

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("“–‚½‚Á‚½");
        if (other.name == "Player")
        {
            this.gameObject.SetActive(false);
            other.enabled = false;
            isGoal = true;
        }
    }

    public bool IsGoal()
    {
        return isGoal;
    }

}
