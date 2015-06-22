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
        if (!_cur && _list.Count == 0)
            GameObject.DestroyImmediate(this);
    }

    public void ClearSameType(System.Type param1)
    {
        foreach (IMission mi in _list)
        {
            if (mi.GetType() == param1)
            {
                GameObject.DestroyImmediate(mi);
                return;
            }
        }
    }

}
