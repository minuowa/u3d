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
            GameObject.Destroy(mi);
        }
    }

    public void OnComplate(int missionid)
    {
        foreach (IMission mi in _list)
        {
            if (mi.id == missionid)
                mi.SetCompleted();
        }
    }

    public void Add(IMission mission)
    {
        if (mission)
        {
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
            if (_cur.Complete())
            {
                _list.Remove(_cur);
                GameObject.Destroy(_cur);
                _cur = null;
            }
        }

        Next();
    }
    void Next()
    {
        if (_cur == null)
        {
            if (_list.Count > 0)
            {
                _cur = _list[0];
                _cur.Begin();
            }
            else
            {
                GameObject.Destroy(this);
            }
        }
    }
}
