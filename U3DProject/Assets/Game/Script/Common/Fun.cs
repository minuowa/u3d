using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Fun
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
}
