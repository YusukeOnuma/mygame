using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadAnimation : MonoBehaviour {


    private float speed = 1f;

	// Use this for initialization
	void Start () {
        var objs = transform.GetComponentsInChildren<Transform>();

        var nameIndex = 0;
        var i = 0f;
        foreach (var obj in objs)
        {
            obj.transform.localPosition = new Vector3 (obj.transform.localPosition.x, i, obj.transform.localPosition.z);
            obj.name = "Road_" + nameIndex.ToString("000");
            nameIndex++;
            i += 17.5f;
        }
	}

    
	
    public float Speed{
        set { speed = value; }
        get { return speed; }
    }
}
