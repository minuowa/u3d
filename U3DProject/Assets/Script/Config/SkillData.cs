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
namespace Config
{
    public class SkillData : Record<SkillData>
    {
        public static string filename = "config/SkillData";

        public float distance;
        public int animition;

        public RangeData range;
        public EffectData effect;
        public SkillObjects objects;

        public bool IsInRange(Vector3 center, Vector3 pos)
        {
            if (range.type == RangeType.Single)
                return Vector3.Distance(center, pos) <= distance;
            return true;
        }

        public void Execute(Being actor, Being victim)
        {
            Do(actor, victim, 0);

            switch (range.type)
            {
                case RangeType.Single:
                    {

                    }
                    break;
                default:
                    {
                        Do(actor, null, 0);
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
                dameobj.Take(this.objects.delay);
                //actor.GetComponent<AnimationCallBack>().bullet = obj;
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
                dameobj.type = DamageObjectType.Normal;
            }
            if (dameobj != null)
            {
                dameobj.Take(this.objects.delay);
            }

        }
    }
}
