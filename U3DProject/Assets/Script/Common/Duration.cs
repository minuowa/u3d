using UnityEngine;
using System.Collections;
public class Duration 
{
    public float total = 1f;

    float _cur = 0f;

    public void Reset()
    {
        _cur = 0;
    }
    public bool Advance(float delta)
    {
        _cur += delta;
        _cur = Mathf.Clamp(_cur, 0f, total);
        return progress >= 1f;
    }
    public float progress
    {
        get
        {
            return Mathf.Clamp(_cur/total,0f,1f);
        }
    }
}
