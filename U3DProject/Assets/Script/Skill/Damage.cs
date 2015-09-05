using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Skill
{
    public enum DamageType
    {
        None,
        Directive,      ///指向性
    }

    public enum DamageObjectType
    {
        None,
        Normal,
        Bullet,
    }

    public class DamageObject : MonoBehaviour
    {
        public int skillid;
        public int missionid;
        public DamageObjectType type;
        public Being sender;
        public List<Being> targets;

        Clock mDelayTimer;

        public void OnComplete()
        {
            GameObject.Destroy(gameObject);
        }

        public Config.SkillData skillData
        {
            get
            {
                return Config.SkillData.Get(skillid);
            }
        }

        public void OnKill(Clock c)
        {
            switch (type)
            {
                case DamageObjectType.Bullet:
                    {
                    }
                    break;
                case DamageObjectType.Normal:
                    {
                        sender.missionMgr.OnComplate(missionid);
                        OnComplete();
                    }
                    break;
            }
        }
        public void OnDestroy()
        {
            if (mDelayTimer != null)
                mDelayTimer.Destory();
        }
        void Start()
        {
            if (skillData.life > 0)
            {
                mDelayTimer = MS<ClockMgr>.Instance.Require();
                mDelayTimer.interval = 0.1;
                mDelayTimer.Begin(Fun.MillSecondToSecond(skillData.life), OnKill);
            }
        }
    }
}

