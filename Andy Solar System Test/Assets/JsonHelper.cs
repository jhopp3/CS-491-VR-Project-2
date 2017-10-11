﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
		Debug.Log("Wrapping.");
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
		Debug.Log("wrapper.Items.Length:");
		Debug.Log(wrapper.Items.Length);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}