using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IParam
{
    public Being receiver;
    public Being sender;
}
public class SelectParam : IParam
{
}
public class GroundMoveParam : IParam
{
    public Vector3 rawpos;
    public float miniDistance = 1.7f;
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
public class SkillParam : IParam
{
    public int skillID;

    public Skill.Data data
    {
        get
        {
            return Skill.Data.Get(skillID);
        }
    }
}