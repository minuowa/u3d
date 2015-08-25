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
public enum MissionOption
{
    None,
    Recreate,
    SetParam,
}

public class Mission 
{
    public delegate void EndDelegate(Mission mission);
    public EndDelegate OnEnd;
    public EndDelegate OnBegin;
    public IMissionParam param
    {
        get
        {
            return mParam;
        }
        set
        {
            bool hasbegin = mBegin;
            mParam = value;
            if (CheckCompleted())
            {
                this.Destroy();
                return;
            }
            if (hasbegin)
                this.Restart();
        }
    }
    protected IMissionParam mParam;

    public int id;

    protected bool mCompleted = false;
    protected bool mBegin = false;

    public static implicit operator bool (Mission mi)
    {
        return mi!=null;
    }

    public void OnComplete()
    {
        mCompleted = true;
    }
    public virtual bool CheckCompleted()
    {
        return false;
    }
    public bool completed
    {
        get
        {
            return mCompleted;
        }
        set
        {
            if (value)
            {
                Destroy();
            }
            mCompleted = value;
        }
    }
    public virtual float Progress()
    {
        return 0;
    }
    public virtual bool IsDoing()
    {
        return mBegin && !mCompleted;
    }
    public virtual void Restart()
    {
        mBegin = true;
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

public class MissionSkill : Mission
{
    public Skill.Executor executor
    {
        get
        {
            return mParam as Skill.Executor;
        }
    }

    public override void Restart()
    {
        base.Restart();
        executor.Execute();
        completed = true;
    }

    public override MissionTag Tag()
    {
        return MissionTag.Skill;
    }
    public override void Update()
    {
        if (mCompleted)
        {
            if (OnEnd != null)
            {
                OnEnd(this);
            }
        }
    }
}