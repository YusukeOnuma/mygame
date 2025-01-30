using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.IO;
using System.Linq;

public class AtlasSpriteReManager : MonoBehaviour
{
    [SerializeField]
    private SpriteAtlas __spriteAtlas;

    [SerializeField]
    public SpriteAtlas atlas { get { return __spriteAtlas; } set { __spriteAtlas = value; } }

    private string __name;

    public string Name
    {
        get { return __name; }
        set
        {
            __name = value;

            if (atlas != null)
            {
                this.GetComponent<SpriteRenderer>().sprite = atlas.GetSprite(__name);
            }
        }
    }
}
