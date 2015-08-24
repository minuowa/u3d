using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MissionMgr : MonoBehaviour
{
    List<IMission> _list = new List<IMission>();

    IMission _cur = null;

    int _count = 0;

    void OnDestroy()
    {
        foreach (IMission mi in _list)
        {

        }
    }

    public void OnComplate(int missionid)
    {
        foreach (IMission mi in _list)
        {
            if (mi.id == missionid)
                mi.completed=true;
        }
    }

    public T GetMission<T>() where T : IMission
    {
        Type type = typeof(T);
        foreach (IMission mi in _list)
        {
            if (mi.GetType() == type)
            {
                return (T)mi;
            }
        }
        return null;
    }

    public void Add(IMission mission,bool removeSameType=false)
    {
        if (mission)
        {
            if (removeSameType)
            {
                ClearSameType(mission.GetType());
            }
            _list.Add(mission);
            _count++;
            mission.id = _count;
        }
    }

    void Update()
    {
        if (_cur != null)
        {
            _cur.Update();
            if (_cur.completed)
            {
                _list.Remove(_cur);
                _cur = null;
            }
        }

        Next();
    }
    void Next()
    {
        if (_cur == null && _list.Count > 0)
        {
            _cur = _list[0];
            while (_list.Count > 0 && !_cur)
            {
                _list.RemoveAt(0);
                _cur = _list[0];
            }
            if (_cur)
                _cur.Begin();
        }
    }

    protected void ClearSameType(System.Type param1)
    {
        foreach (IMission mi in _list)
        {
            if (mi.GetType() == param1)
            {
                mi.completed = true;
                _list.Remove(mi);
                if (_cur == mi)
                    _cur = null;
                return;
            }
        }
    }


    void OnDrawGizmos()
    {
        if (_cur)
            _cur.OnDrawGizmos();
    }

}
