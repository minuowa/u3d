using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Clock
{
    public delegate void TimeDelegate(Clock clock);
    public TimeDelegate OnTimer;
    public double interval=0.05;
    public bool canPause = true;

    ~Clock()
    {

    }
    public TimeSpan formatTime
    {
        get
        {
            return TimeSpan.FromSeconds(mTimerEnd - Time.time);
        }
    }

    public int id
    {
        get
        {
            return mID;
        }
    }
    public bool doing

    {
        get
        {
            return mBegin && !mEnd;
        }
    }
    public bool begin
    {
        get
        {
            return mBegin;
        }
    }
    public bool end
    {
        get
        {
            return mEnd;
        }
    }
    int mID = 0;
    bool mBegin = false;
    bool mEnd = false;
    bool mPause = false;

    double mTimeBegin = 0;
    double mTimerEnd = 0;
    double mPauseTime = 0;

    int mFrame = 0;
    TimeDelegate mOnEnd;


    public void Begin(uint seconds,TimeDelegate onEnd)
    {
        Begin((double)seconds,onEnd);
    }
    public void Stop(bool triggerEvent)
    {
        if (triggerEvent && mOnEnd != null)
            mOnEnd(this);
        mEnd = true;
        mBegin = false;
        Reset();
    }
    void Reset()
    {
        mFrame = 0;
        mTimeBegin = 0;
        mTimerEnd = 0;
    }
    public void Begin(double seconds, TimeDelegate onEnd)
    {
        mBegin = true;
        mEnd = false;
        mTimeBegin = Time.time;
        mTimerEnd = mTimeBegin + seconds;
        mOnEnd = onEnd;
    }
    public void Update()
    {
        if (!mBegin || mEnd )
            return;

        if (mPause)
        {
            mPauseTime += Time.deltaTime;
            return;
        }

        int frame = (int)((Time.time - mTimeBegin - mPauseTime) / interval);

        if (frame != mFrame)
        {
            if (OnTimer != null)
                OnTimer(this);
            mFrame = frame;
        }

        if (Time.time > mTimerEnd)
        {
            if (mOnEnd != null)
                mOnEnd(this);
            mBegin = false;
            mEnd = true;
        }
    }

    public void Pause()
    {
        if (canPause)
            mPause = true;
    }
    public void Resume()
    {
        mPause = false;
    }
    public static Clock Create(int id)
    {
        Clock p = new Clock();
        p.mID = id;
        return p;
    }
    public void Destory()
    {
        mEnd = true;
        MS<ClockMgr>.Instance.Remove(this);
    }
}
public class ClockMgr : MonoBehaviour
{
    Dictionary<int, Clock> mTimerList;

    int mCount;

    public ClockMgr()
    {
        mTimerList = new Dictionary<int, Clock>();
        mCount = 0;
    }
    public Clock Require()
    {
        Clock p = null;
        p = Clock.Create(mCount);
        mCount++;
        mTimerList.Add(mCount, p);
        return p;
    }
    void Update()
    {
        List<Clock> clocklist = new List<Clock>();
        foreach (var p in mTimerList)
        {
            clocklist.Add(p.Value);
        }
        foreach (var c in clocklist)
        {
            c.Update();
        }
    }
    public void Remove(Clock c)
    {
        mTimerList.Remove(c.id);
        c = null;
    }
    public void Resume()
    {
        foreach (var p in mTimerList)
        {
            p.Value.Resume();
        }
    }
    public void Pause()
    {
        foreach (var p in mTimerList)
        {
            p.Value.Pause();
        }
    }
}
