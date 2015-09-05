using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Fun
{
    public static float  MillSecondToSecond(float ms)
    {
        return ms*0.001f;
    }

    public static void DoSthAfterTime(float time, Action act)
    {
        Clock c = MS<ClockMgr>.Instance.Require();
        c.Begin(time, (Clock clock) =>
        {
            if (act != null)
                act();
            clock.Destory();
        }
        );
    }
    public static Vector3 GetPostion(GameObject go)
    {
        Animator animator = go.GetComponentInChildren<Animator>();
        if (animator)
            return animator.bodyPosition;
        return go.transform.position;
    }
    public static void SetPosition(GameObject go, Vector3 pos)
    {
        Animator animator = go.GetComponentInChildren<Animator>();
        if (animator)
            animator.bodyPosition = pos;
        else
            go.transform.position = pos;
    }
    public static Quaternion GetRotation(GameObject go)
    {
        Animator animator = go.GetComponentInChildren<Animator>();
        if (animator)
            return animator.bodyRotation;
        return go.transform.rotation;
    }
    public static void SetRotation(GameObject go, Quaternion rot)
    {
        Animator animator = go.GetComponentInChildren<Animator>();
        if (animator)
            animator.bodyRotation = rot;
        else
            go.transform.rotation = rot;
    }
    public static void DestoryChildren(GameObject obj)
    {
        if (obj)
        {
            for (int i = 0; i < obj.transform.childCount; ++i)
            {
                if (Application.isPlaying)
                    UnityEngine.Object.Destroy(obj.transform.GetChild(i).gameObject);
                else
                    UnityEngine.Object.DestroyImmediate(obj.transform.GetChild(i).gameObject);
            }
        }
    }

    public static void SetLayer(GameObject go, int layer, bool changeChildren = true)
    {
        if (go)
        {
            go.layer = layer;

            if (changeChildren)
            {
                for (int i = 0; i < go.transform.childCount; ++i)
                {
                    SetLayer(go.transform.GetChild(i).gameObject, layer, changeChildren);
                }
            }
        }
    }
    public static Dictionary<string, string> ParseMap(this string strMap, char keyValueSpriter = ':', char mapSpriter = ',')
    {
        return new Dictionary<string, string>();
    }
    public static string LoadFile(string file)
    {
        return string.Empty;
    }
    public static string GetDirectoryName(string file)
    {
        return string.Empty;
    }

    public static void SetFirstChild(GameObject go, string name, bool vis)
    {
        GameObject ch = FindFirstChild(go, name);
        if (ch)
            ch.SetActive(vis);
    }
    public static void SetFirstChild(GameObject go, string name, string text)
    {
        GameObject ch = FindFirstChild(go, name);
        if (ch)
        {
            var v = ch.GetComponent<UILabel>();
            if (v != null)
                v.text = text;
        }
    }
    public static void SetFirstChild(GameObject go, string name, Texture text)
    {
        GameObject ch = FindFirstChild(go, name);
        if (ch)
        {
            var v = ch.GetComponent<UITexture>();
            if (v != null)
                v.mainTexture = text;
        }
    }
    public static GameObject FindFirstChild(GameObject parent, string name)
    {
        if (parent.name == name)
            return parent;
        if (parent)
        {
            Int32 cnt = parent.transform.childCount;
            for (int i = 0; i < cnt; ++i)
            {
                Transform c = parent.transform.GetChild(i);
                GameObject tar = FindFirstChild(c.gameObject, name);
                if (tar != null)
                    return tar.gameObject;
            }
        }
        return null;
    }
}
