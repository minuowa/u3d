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

public class IMission:IParam
{
    public delegate void EndDelegate(IMission mission);
    public EndDelegate OnEnd;
    public EndDelegate OnBegin;

    public int id;

    protected bool _completed = false;
    protected bool _begin = false;

    public static implicit operator bool (IMission mi)
    {
        return mi!=null;
    }
    public void AddToWorkList(MissionMgr mgr)
    {
        mgr.Add(this);
    }

    public void OnComplete()
    {
        _completed = true;
    }
    public bool completed
    {
        get
        {
            return _completed;
        }
        set
        {
            if (value)
            {
                Destroy();
            }
            _completed = value;
        }
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
            OnBegin(this);
    }
    public virtual MissionTag Tag()
    {
        return MissionTag.None;
    }
    public virtual void Update()
    {

    }
    public virtual void Destroy()
    {

    }
    public virtual void OnDrawGizmos()
    {

    }
}

public class MissionSkill : IMission
{
    public Skill.Executor executor;

    public override void Begin()
    {
        base.Begin();
        executor.Execute();
        completed = true;
    }

    public override MissionTag Tag()
    {
        return MissionTag.Skill;
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