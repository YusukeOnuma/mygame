using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Jsonhelper {
    public static T[] FromJson<T>(string json)
    {

        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);

        return wrapper.items;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}
