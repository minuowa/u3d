using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class MissionMgr : MonoBehaviour
{
    public delegate void OnCompleteMission(int missionid);
    public OnCompleteMission onComplete;
    List<Mission> mList = new List<Mission>();

    Mission mCur = null;

    int mCount = 0;
    public void Clear()
    {
        if (mCur)
            mCur.Discard();
        mCur = null;
        mList.Clear();
    }
    void OnDestroy()
    {
    }
    public bool IsDoing()
    {
        return mCur != null || mList.Count > 0;
    }
    public void OnComplate(int missionid)
    {
        if (onComplete != null)
            onComplete(missionid);

        if (mCur != null && mCur.id == missionid)
            mCur.completed = true;

        foreach (Mission mi in mList)
        {
            if (mi.id == missionid)
                mi.completed = true;
        }
    }

    public T GetMission<T>() where T : Mission
    {
        Type type = typeof(T);
        foreach (Mission mi in mList)
        {
            if (mi.GetType() == type)
            {
                return (T)mi;
            }
        }
        return (T)GetMission(typeof(T));
    }

    public Mission GetMission(Type type)
    {
        foreach (Mission mi in mList)
        {
            if (mi.GetType() == type)
            {
                return mi;
            }
        }
        return null;
    }

    public void Add(IMissionParam param, MissionOption option = MissionOption.None)
    {
        Mission mission = param.Create();
        switch (option)
        {
            case MissionOption.ClearList:
                {
                    this.Clear();
                    AddInner(mission);
                }
                break;
            case MissionOption.Recreate:
                {
                    ClearSameType(mission.GetType());
                    AddInner(mission);
                }
                break;
            case MissionOption.SetParam:
                {
                    Mission old = GetMission(mission.GetType());
                    if (old)
                    {
                        old.param = param;
                    }
                    else
                    {
                        AddInner(mission);
                    }
                }
                break;
            default:
                {
                    AddInner(mission);
                }
                break;
        }
    }
    void AddInner(Mission mi)
    {
        mCount++;
        mi.id = mCount;
        mList.Add(mi);
    }
    void Update()
    {
        if (mCur)
        {
            if(mCur.state== MissionState.DataReady)
            {
                if (mCur.CheckCompleted())
                    mCur.Discard();
                else
                    mCur.Restart();
            }
            else
            {
                mCur.Update();
            }
            if (mCur.state == MissionState.Completed)
            {
                mList.Remove(mCur);
                mCur = null;
            }
        }
        else
        {
            Next();
        }
    }
    void Next()
    {
        if (mCur == null && mList.Count > 0)
        {
            //递归删除空值
            mCur = mList[0];
            while (mList.Count > 0 && !mCur)
            {
                mList.RemoveAt(0);
                mCur = mList[0];
            }
            if (mCur)
                mCur.InitBaseData();
        }
    }

    protected void ClearSameType(System.Type param1)
    {
        foreach (Mission mi in mList)
        {
            if (mi.GetType() == param1)
            {
                mi.completed = true;
                mList.Remove(mi);
                if (mCur == mi)
                    mCur = null;
                return;
            }
        }
    }


    void OnDrawGizmos()
    {
        if (mCur)
            mCur.OnDrawGizmos();
    }

}
