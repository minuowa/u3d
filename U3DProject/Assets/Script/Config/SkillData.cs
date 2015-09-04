using Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public enum Default
{
    NormalAttack = 1000,
}
public class SkillObjects
{
    public string normal;
    public string bullet;
    public float delay;
}
public enum RangeType
{
    Single,
    Quad,
    Circle,
    Sector,
}
namespace Config
{
    public class SkillData : Record<SkillData>
    {
        public static string filename = "config/SkillData";

        public int animition;
        public float distance;
        public float beginTime;

        public RangeType rangeType;
        public EffectData effect;
        public SkillObjects objects;
        public bool IsInRange(Vector3 center, Vector3 pos)
        {
            if (rangeType == RangeType.Single)
                return Vector3.Distance(center, pos) <= distance;
            return true;
        }

        public void Execute(Being actor, Being victim, int missionid)
        {
            Do(actor, victim, missionid);

            switch (rangeType)
            {
                case RangeType.Single:
                    {

                    }
                    break;
                default:
                    {
                        Do(actor, null, missionid);
                    }
                    break;
            }
        }

        private void Do(Being actor, Being victim, int missionid)
        {
            if (!actor)
                return;

            if (!string.IsNullOrEmpty(effect.name))
            {
                Effector effector = actor.gameObject.AddComponent<Effector>();
                effector.data = effect;
            }

            Animator anim = actor.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                anim.SetInteger(BeingAnimation.action, animition);
            }
            DamageObject dameobj=null;
            if (!string.IsNullOrEmpty(objects.bullet))
            {
                GameObject prefab = (GameObject)Resources.Load(objects.bullet, typeof(GameObject));
                GameObject obj = (GameObject)GameObject.Instantiate(prefab);
                if (!obj)
                    return;
                obj.transform.localPosition = prefab.transform.localPosition;
                obj.transform.localRotation = prefab.transform.localRotation;
                obj.transform.localScale = prefab.transform.localScale;
                obj.transform.parent = Garbage.Instance.root.transform;
                obj.SetActive(false);

                OneDamage data = null;
                if (victim)
                {
                    DamageReceiver receiver = victim.gameObject.GetComponent<DamageReceiver>();
                    data = new OneDamage();
                    data.sender = actor;
                    data.skill = new Skill.Executor();
                    data.skill.skillID = id;
                    data.missionid = missionid;
                    receiver.damageList.Add(data);
                }

                dameobj = obj.AddComponent<DamageObject>();
                dameobj.skillid = id;
                dameobj.sender = actor;
                dameobj.target = victim;
                dameobj.missionid = missionid;
                dameobj.type = DamageObjectType.Bullet;
                dameobj.damage = data;
                dameobj.Take(this.objects.delay);
            }
            else if (!string.IsNullOrEmpty(objects.normal))
            {
                GameObject prefab = (GameObject)Resources.Load(objects.normal, typeof(GameObject));
                GameObject obj = (GameObject)GameObject.Instantiate(prefab);
                if (!obj)
                    return;
                obj.transform.localPosition = prefab.transform.localPosition;
                obj.transform.localRotation = prefab.transform.localRotation;
                obj.transform.localScale = prefab.transform.localScale;
                obj.transform.parent = Garbage.Instance.root.transform;
                obj.SetActive(false);

                OneDamage data = null;
                if (victim)
                {
                    DamageReceiver receiver = victim.gameObject.GetComponent<DamageReceiver>();
                    data = new OneDamage();
                    data.sender = actor;
                    data.skill = new Skill.Executor();
                    data.skill.skillID = id;
                    data.missionid = missionid;
                    receiver.damageList.Add(data);
                }

                dameobj = obj.AddComponent<DamageObject>();
                dameobj.skillid = id;
                dameobj.sender = actor;
                dameobj.target = victim;
                dameobj.missionid = missionid;
                dameobj.damage = data;
                dameobj.type = DamageObjectType.Normal;
            }
            if (dameobj != null)
            {
                dameobj.Take(this.objects.delay);
            }

        }
    }
}
