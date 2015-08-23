using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Fun
{
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
}
