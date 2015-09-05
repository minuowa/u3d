using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IReceiver
{
    protected bool mEnd = false;

    public bool end
    {
        get
        {
            return mEnd;
        }
    }
    public virtual void OnEnd()
    {

    }
}
public class OneDamage : IReceiver
{
    public Being sender;
    public Being victim;
    public Skill.Executor skill;
    public int missionid;

    public override void OnEnd()
    {
        if (sender)
        {
            MissionMgr mgr = sender.GetComponent<MissionMgr>();
            if (mgr)
                mgr.OnComplate(missionid);
        }
        if (victim)
        {
            Animator anim = victim.gameObject.GetComponent<Animator>();
            if (anim != null)
                anim.SetInteger(BeingAnimation.action, BeingAnimation.BeAttack1);
            victim.transform.position -= victim.transform.forward * 0.2f;
        }
    }
}
public class DamageReceiver : MonoBehaviour {
    public List<OneDamage> damageList;
	
    DamageReceiver()
    {
        damageList = new List<OneDamage>();
    }
    public void RemoveThreat(Being sender)
    {
        foreach (var d in damageList)
        {
            if (d.sender == sender)
            {
                damageList.Remove(d);
                return;
            }
        }
    }
    public Being firstAttacker
    {
        get
        {
            if (damageList.Count > 0)
                return damageList[0].sender;
            return null;
        }
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        foreach (var d in damageList)
        {
            if (d.end)
            {
                damageList.Remove(d);
            }
        }
	}
}
