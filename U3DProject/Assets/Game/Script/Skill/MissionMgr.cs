using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MissionMgr : MonoBehaviour
{
    List<IMission> _list = new List<IMission>();

    IMission _cur = null;

    public void Add(IMission mission)
    {
        _list.Add(mission);
    }

    void Update()
    {
        Next();

        if (_cur != null)
        {
            _cur.Update();
        }
        if (_cur.Complete())
        {
            _list.Remove(_cur);
        }
    }
    void Next()
    {
        if (_cur == null && _list.Count > 0)
        {
            _cur = _list[0];
            _cur.Begin();
        }
    }
}
