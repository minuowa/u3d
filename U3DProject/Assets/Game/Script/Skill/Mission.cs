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

public class IMission:MonoBehaviour
{
    public delegate void EndDelegate(IMission mission);
    public EndDelegate OnEnd;
    public EndDelegate OnBegin;

    public int id;

    protected bool _completed = false;
    protected bool _begin = false;

    public MissionMgr Manager()
    {
        MissionMgr mgr = gameObject.GetComponent<MissionMgr>();
        if (!mgr)
            mgr = gameObject.AddComponent<MissionMgr>();
        return mgr;
    }

    public void Work()
    {
        Manager().Add(this);
    }

    public void OnComplete()
    {
        _completed = true;
    }
    public virtual bool Complete()
    {
        return _completed;
    }
    public void SetCompleted()
    {
        _completed = true;
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