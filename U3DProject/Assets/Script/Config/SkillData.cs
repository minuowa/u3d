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

public class EffectData
{
    public string name;
    public int beginTime;
    public int life;
    public BindType type;
}
public enum RangeType
{
    Single,
    Quad,
    Circle,
    Sector,
}
public enum SkillBindType
{
    Head0,
    Head1,
    HandLeft,
    HandRight,
    FootLeft,
    FootRight,
    WeaponLeft,
    WeaponRight,
}
namespace Config
{
    public class SkillData : Record<SkillData>
    {
        public static string filename = "config/SkillData";

        public RangeType rangeType;
        public int animation;
        public int beAnimation;
        public float distance;
        public int life;
        public List<EffectData> attackEffects;
        public List<EffectData> beAttackEffects;
        public EffectData bullet;

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
        void ProcessAni(Being actor, Being victim)
        {
            if (actor)
            {
                Animator anim = actor.GetComponentInChildren<Animator>();
                if (anim != null)
                    anim.SetInteger(BeingAnimation.action, this.animation);
            }
            if (victim)
            {
                Animator anim = victim.GetComponentInChildren<Animator>();
                if (anim != null)
                    anim.SetInteger(BeingAnimation.action, this.beAnimation);
            }
        }
        void GenerateEffectObjects(Being actor, List<Being> victims, int missionid)
        {
            GameObject go = new GameObject();
            DamageObject dameobj = go.AddComponent<DamageObject>();
            go.transform.parent = Garbage.Instance.root.transform;
            go.SetActive(true);

            dameobj.skillid = id;
            dameobj.sender = actor;
            dameobj.targets = victims;
            dameobj.missionid = missionid;

            foreach (var victim in victims)
            {
                DamageReceiver damageReceiver = victim.GetComponent<DamageReceiver>();
                if (damageReceiver)
                {
                    OneDamage data = new OneDamage();
                    data.sender = actor;
                    data.missionid = missionid;
                    data.victim = victim;
                    damageReceiver.damageList.Add(data);
                }
            }

            foreach (var eff in attackEffects)
            {
                GameObject prefab = (GameObject)Resources.Load(eff.name, typeof(GameObject));
                GameObject obj = (GameObject)GameObject.Instantiate(prefab);
                if (!obj)
                    return;
                obj.transform.localPosition = prefab.transform.localPosition;
                obj.transform.localRotation = prefab.transform.localRotation;
                obj.transform.localScale = prefab.transform.localScale;
                obj.transform.parent = Garbage.Instance.root.transform;
                obj.SetActive(false);
                Effector effector = obj.AddComponent<Effector>();
                effector.config = eff;
                effector.damageobj = dameobj;
                effector.target = actor;
                effector.missionID = missionid;
                effector.TakeOn(Fun.MillSecondToSecond(eff.beginTime));
            }

            if (this.bullet != null && !string.IsNullOrEmpty(this.bullet.name))
            {
                dameobj.type = DamageObjectType.Bullet;
                GameObject prefab = (GameObject)Resources.Load(this.bullet.name, typeof(GameObject));
                GameObject obj = (GameObject)GameObject.Instantiate(prefab);
                if (!obj)
                    return;
                obj.transform.localPosition = prefab.transform.localPosition;
                obj.transform.localRotation = prefab.transform.localRotation;
                obj.transform.localScale = prefab.transform.localScale;
                obj.transform.parent = Garbage.Instance.root.transform;
                obj.SetActive(false);
                Effector effector = obj.AddComponent<BulletEffector>();
                effector.config = bullet;
                effector.damageobj = dameobj;
                effector.sender = actor;
                effector.target = victims[0];
                effector.missionID = missionid;
                effector.TakeOn(Fun.MillSecondToSecond(this.bullet.beginTime));
            }
            else
            {
                dameobj.type = DamageObjectType.Normal;

                if (victims != null && victims.Count > 0)
                {
                    foreach (var victim in victims)
                    {
                        foreach (var eff in beAttackEffects)
                        {
                            GameObject prefab = (GameObject)Resources.Load(eff.name, typeof(GameObject));
                            GameObject obj = (GameObject)GameObject.Instantiate(prefab);
                            if (!obj)
                                return;
                            obj.transform.localPosition = prefab.transform.localPosition;
                            obj.transform.localRotation = prefab.transform.localRotation;
                            obj.transform.localScale = prefab.transform.localScale;
                            obj.transform.parent = Garbage.Instance.root.transform;
                            obj.SetActive(false);
                            Effector effector = obj.AddComponent<Effector>();
                            effector.damageobj = dameobj;
                            effector.config = eff;
                            effector.target = victim;
                            effector.missionID = missionid;
                            effector.TakeOn(Fun.MillSecondToSecond(eff.beginTime));
                        }
                    }
                }
            }
        }
        private void Do(Being actor, Being victim, int missionid)
        {
            if (!actor)
                return;
            ProcessAni(actor, victim);
            if (victim)
            {
                List<Being> victims = new List<Being>();
                victims.Add(victim);
                GenerateEffectObjects(actor, victims, missionid);
            }
        }
    }
}
