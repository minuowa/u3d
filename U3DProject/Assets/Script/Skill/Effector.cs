using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Effector : MonoBehaviour
{
    public Skill.DamageObject damageobj;
    public EffectData config;
    public Being sender;
    public Being target;
    public int missionID;
    Clock mBeginTimer;
    Clock mEndTimer;

    public void TakeOn(float time)
    {
        mBeginTimer = MS<ClockMgr>.Instance.Require();
        mBeginTimer.interval = 0.1;
        mBeginTimer.Begin(time, OnEffectBegin);
    }
    public virtual void OnEffectBegin(Clock c)
    {
        gameObject.SetActive(true);
        transform.position = target.GetArcherShotPos();
        mBeginTimer.Destory();

        mEndTimer = MS<ClockMgr>.Instance.Require();
        mEndTimer.interval = 0.1;
        mEndTimer.Begin(Fun.MillSecondToSecond(config.life), OnEffectEnd);
    }
    public void OnEffectEnd(Clock c)
    {
        Destroy(gameObject);
    }
    public virtual void OnDestroy()
    {
    }
}
