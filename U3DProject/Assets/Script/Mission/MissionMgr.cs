using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class MissionMgr : MonoBehaviour
{
    List<Mission> mList = new List<Mission>();

    Mission mCur = null;

    int mCount = 0;

    void OnDestroy()
    {
    }

    public void OnComplate(int missionid)
    {
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

    public void Add(Mission mission, IMissionParam param, MissionOption option = MissionOption.None)
    {
        if (mission)
        {
            if (option == MissionOption.Recreate)
            {
                ClearSameType(mission.GetType());
                AddInner(mission, param);
            }
            else if (option == MissionOption.SetParam)
            {
                Mission old = GetMission(mission.GetType());
                if (old)
                {
                    old.param = param;
                }
                else
                {
                    AddInner(mission, param);
                }
            }
            else
            {
                AddInner(mission, param);
            }
        }
    }
    void AddInner(Mission mi, IMissionParam param)
    {
        mCount++;
        mi.id = mCount;
        mi.param = param;
        mList.Add(mi);
    }
    void Update()
    {
        if (mCur != null)
        {
            mCur.Update();
            if (mCur.completed)
            {
                mList.Remove(mCur);
                mCur = null;
            }
        }

        Next();
    }
    void Next()
    {
        if (mCur == null && mList.Count > 0)
        {
            mCur = mList[0];
            while (mList.Count > 0 && !mCur)
            {
                mList.RemoveAt(0);
                mCur = mList[0];
            }
            if (!mCur.CheckCompleted())
                mCur.Restart();
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
