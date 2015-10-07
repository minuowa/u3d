using System;
using System.Collections;
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

    public virtual void OnBegin()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = target.transform.position;
    }
    public virtual void OnEnd()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public void TakeOn(float time)
    {
        MS<ClockMgr>.Instance.StartCoroutine(WaitForSkillBegin(time));
    }
    IEnumerator WaitForSkillBegin(float time)
    {
        yield return new WaitForSeconds(time);
        this.OnBegin();
        MS<ClockMgr>.Instance.StartCoroutine(WaitForSkillEnd(Fun.MillSecondToSecond(config.life)));
    }
    IEnumerator WaitForSkillEnd(float time)
    {
        yield return new WaitForSeconds(time);
        this.OnEnd();
    }
}
