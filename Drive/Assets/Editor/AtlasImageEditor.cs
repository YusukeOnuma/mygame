using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.U2D;
using System.Linq;
using UnityEditor.AnimatedValues;
using UnityEngine.UI;

[CustomEditor(typeof(AtlasImageManager), true)]
[CanEditMultipleObjects]
public class AtlasEditor : ImageEditor
{
    SerializedProperty __atlas;
    SerializedProperty __name;

    AnimBool __isShowName;

    string[] __names;

    int __index = 0;

    protected override void OnEnable()
    {
        __atlas = serializedObject.FindProperty("__spriteAtlas");
        __name = serializedObject.FindProperty("__name");
        __isShowName = new AnimBool(__atlas.objectReferenceValue != null);

        

        base.OnEnable();
    }

    protected override void OnDisable()
    {
        __isShowName.valueChanged.RemoveListener(Repaint);
        base.OnDisable();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        AtlasGUI();

        __isShowName.target = __atlas.objectReferenceValue != null;
        if (EditorGUILayout.BeginFadeGroup(__isShowName.faded))
            _SpriteNameGUI();
        EditorGUILayout.EndFadeGroup();

        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }

    protected virtual void AtlasGUI()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(__atlas);

        if (EditorGUI.EndChangeCheck())
        {
            __ResetAtlasSpriteNames();
            __ResetSpriteNameIndex();
        }
    }

    private void __ResetSpriteNameIndex()
    {
        if(__names == null || __names.Length == 0)
            return;

        string crtName = __name.stringValue;
        int tempIndx = 0;
        for(int i = 0; i < __names.Length; i++)
        {
            if(crtName == __names[i])
            {
                tempIndx = i;
                break;
            }
        }

        __index = tempIndx;
        __name.stringValue = __names[__index];

    }

    private void __ResetAtlasSpriteNames()
    {
        var newAtlas = __atlas.objectReferenceValue as SpriteAtlas;
        if (newAtlas)
        {
            __names = GetAllSprite(newAtlas)
            .Select(x => x.name.Replace("(Clone)", ""))
            .ToArray();
        }
    }

    protected virtual void _SpriteNameGUI()
    {
        EditorGUI.BeginChangeCheck();
        if (__names != null)
            __index = EditorGUILayout.Popup("SpriteName", __index, __names);

        if (EditorGUI.EndChangeCheck())
        {
            __name.stringValue = __names[__index];
            _UpdateSourceImage();
        }
    }


    protected virtual void _UpdateSourceImage()
    {
        SerializedProperty m_Type = serializedObject.FindProperty("m_Type");
        SerializedProperty m_Sprite = serializedObject.FindProperty("m_Sprite");

        var currentAtlas = __atlas.objectReferenceValue as SpriteAtlas;

        if (currentAtlas == null)
            return;

        var newSprite = currentAtlas.GetSprite(__name.stringValue);

        m_Sprite.objectReferenceValue = newSprite;
        if (newSprite)
        {
            Image.Type oldType = (Image.Type)m_Type.enumValueIndex;
            if (newSprite.border.SqrMagnitude() > 0)
            {
                m_Type.enumValueIndex = (int)Image.Type.Sliced;
            }
            else if (oldType == Image.Type.Sliced)
            {
                m_Type.enumValueIndex = (int)Image.Type.Simple;
            }
        }
    }

    static IEnumerable<Sprite> GetAllSprite(SpriteAtlas spriteAtlas)
    {
        //spriteの空の配列を作成、サイズはAtlasに含まれるSpriteの数
        Sprite[] spriteArray = new Sprite[spriteAtlas.spriteCount];

        //spriteArrayに全Spriteを設定
        spriteAtlas.GetSprites(spriteArray);
        foreach (var sprite in spriteArray)
        {
            yield return sprite;
        }
    }


}
