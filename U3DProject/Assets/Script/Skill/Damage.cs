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

        public void OnKill()
        {
            sender.missionMgr.OnComplate(missionid);
            OnComplete();
            //switch (type)
            //{
            //    case DamageObjectType.Bullet:
            //        {
            //        }
            //        break;
            //    case DamageObjectType.Normal:
            //        {

            //        }
            //        break;
            //}
        }
        void Start()
        {
            if (skillData.life > 0)
                this.StartCoroutine(WaitForKill(Fun.MillSecondToSecond(skillData.life)));
        }
        IEnumerator WaitForKill(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            this.OnKill();
        }
    }
}

