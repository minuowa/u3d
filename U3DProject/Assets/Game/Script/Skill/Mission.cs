using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum MissionTag
{
    None,
    FindPos,
    FindObject,
    Skill,
}

public class IMission
{
    public delegate void EndDelegate(IMission mission);
    public EndDelegate OnEnd;
    public EndDelegate OnBegin;

    protected bool _completed = false;
    protected bool _begin = false;

    public virtual bool Complete()
    {
        return _completed;
    }
    public virtual float Progress()
    {
        return 0;
    }
    public virtual bool IsDoing()
    {
        return _begin && !_completed;
    }
    public virtual void Begin()
    {
        _begin = true;
        if (OnBegin != null)
        {
            OnBegin(this);
        }
    }
    public virtual MissionTag Tag()
    {
        return MissionTag.None;
    }
    public virtual void Update()
    {

    }
}
public class MissionFindPos : IMission
{
    public Vector3 target;
    public GameObject onwer;

    public override bool Complete()
    {
        _completed = Vector3.Distance(target, onwer.transform.position) < 0.01f;
        return _completed;
    }
    public override float Progress()
    {
        return 0;
    }
    public override bool IsDoing()
    {
        return false;
    }

    public override MissionTag Tag()
    {
        return MissionTag.FindPos;
    }
    public override void Update()
    {
        if (_completed)
        {
            if (OnEnd != null)
            {
                OnEnd(this);
            }
        }
    }

}

public class MissionSkill : IMission
{
    public Skill.Executor executor;

    public override void Begin()
    {
        base.Begin();

        executor.Execute();
    }

    public override MissionTag Tag()
    {
        return MissionTag.Skill;
    }
    public override void Update()
    {
        Complete();

        if (_completed)
        {
            if (OnEnd != null)
            {
                OnEnd(this);
            }
        }
    }

}