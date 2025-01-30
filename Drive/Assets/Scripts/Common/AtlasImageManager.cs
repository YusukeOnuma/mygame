using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class AtlasImageManager : Image
{
    [SerializeField]
    private SpriteAtlas __spriteAtlas;

    [SerializeField]
    public SpriteAtlas atlas { get { return __spriteAtlas; } set { __spriteAtlas = value; } }

    [SerializeField]
    private string __name;

    public string Name
    {
        get { return __name; }
        set
        {
            __name = value;

            if(atlas != null)
            {
                this.sprite = atlas.GetSprite(__name);
            }
        }
    }

}
