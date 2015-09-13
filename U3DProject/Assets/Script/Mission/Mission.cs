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
    ClearList,
    Recreate,
    SetParam,
}
public enum MissionState
{
    None,
    DataReady,
    Begined,
    Completed,
}
public class Mission 
{
    public delegate void EndDelegate(Mission mission);
    public EndDelegate OnEnd;
    public EndDelegate OnBegin;
    public MissionState state = MissionState.None;
    public IMissionParam param
    {
        get
        {
            return mParam;
        }
        set
        {
            mParam = value;
            this.InitBaseData();
        }
    }
    protected IMissionParam mParam;

    public int id;

    public static implicit operator bool (Mission mi)
    {
        return mi!=null;
    }
    public virtual bool InitBaseData()
    {
        state = MissionState.DataReady;
        return true;
    }
    public virtual bool CheckCompleted()
    {
        return false;
    }
    public bool completed
    {
        get
        {
            return state== MissionState.Completed;
        }
        set
        {
            this.Discard();
        }
    }
    public virtual float Progress()
    {
        return 0;
    }
    public virtual bool IsDoing()
    {
        return state == MissionState.Begined;
    }
    public virtual void Restart()
    {
        state = MissionState.Begined;
        if (OnBegin != null)
            OnBegin(this);
    }
    public virtual MissionTag Tag()
    {
        return MissionTag.None;
    }
    public virtual bool Update()
    {
        if(state== MissionState.Begined)
        {
            if(CheckCompleted())
            {
                if (OnEnd != null)
                    OnEnd(this);
                this.Discard();
                return false;
            }
        }
        return true;
    }
    public virtual void Discard()
    {
        state = MissionState.Completed;
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
        executor.Execute(this.id);
    }

    public override MissionTag Tag()
    {
        return MissionTag.Skill;
    }

}