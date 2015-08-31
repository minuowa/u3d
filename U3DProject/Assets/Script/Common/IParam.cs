using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IMissionParam
{
    public Being receiver;
    public Being sender;
}
public class SelectParam : IMissionParam
{
}
public class GroundMoveParam : IMissionParam
{
    public Vector3 rawpos;
    public float miniDistance = 1.7f;
    public bool auto = false;
    public Vector3 target
    {
        get
        {
            if (receiver)
                return receiver.transform.position;
            return rawpos;
        }
    }
}
public class SkillParam : IMissionParam
{
    public int skillID;

    public Config.SkillData data
    {
        get
        {
            return Config.SkillData.Get(skillID);
        }
    }
}