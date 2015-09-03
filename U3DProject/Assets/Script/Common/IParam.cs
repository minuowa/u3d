using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IMissionParam
{
    public Being receiver;
    public Being sender;
    public virtual Mission Create()
    {
        return null;
    }
}
public class SelectParam : IMissionParam
{
}
public class GroundMoveParam : IMissionParam
{
    public Vector3 rawpos;
    public float miniDistance = 0.55f;
    public bool visible = false;
    public Vector3 target
    {
        get
        {
            if (receiver)
                return receiver.transform.position;
            return rawpos;
        }
    }

    public override Mission Create()
    {
        var mission = new GroundMove();
        mission.param = this;
        return mission;
    }
}
