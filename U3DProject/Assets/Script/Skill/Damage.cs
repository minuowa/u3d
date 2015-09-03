using UnityEngine;
using System.Collections;
namespace Skill
{
    public enum DamageType
    {
        None,
        Directive,      ///指向性
    }
    public class DamageData
    {
        public string prefab;

        public void AddTo(GameObject go)
        {

        }
    }


    public class DamageSender : MonoBehaviour
    {
        public int skillid;
        public GameObject sender;
        public int missionid;
        public void OnEnd()
        {
            if (sender)
            {
                MissionMgr mgr = gameObject.GetComponent<MissionMgr>();
                if (mgr)
                    mgr.OnComplate(missionid);
            }
            Animator anim = gameObject.GetComponent<Animator>();
            if (anim != null)
                anim.SetInteger(BeingAnimation.action, BeingAnimation.Joke1);
            transform.position -= transform.forward * 0.2f;
            Destroy(this);
        }
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
        public Being sender;
        public Being target;
        public int missionid;
        public OneDamage damage;
        public DamageObjectType type;
        Clock mDelayTimer;

        public void Take(float time)
        {
            //尚未激活，不可用协成。。
            mDelayTimer = MS<ClockMgr>.Instance.Require();
            mDelayTimer.interval = 0.1;
            mDelayTimer.Begin(time, OnTimeEnd);
        }
        public void OnTimeEnd(Clock c)
        {
            switch (type)
            {
                case DamageObjectType.Bullet:
                    {
                        Shot();
                    }
                    break;
                case DamageObjectType.Normal:
                    {

                    }
                    break;
            }
        }
        public void Shot()
        {
            gameObject.SetActive(true);
            transform.position = sender.GetArcherShotPos();

            if (target)
            {
                FlyerMove mvoe = gameObject.AddComponent<FlyerMove>();
                Collider co = target.GetComponent<Collider>();
                mvoe.target = target.transform.position;
                if (co)
                {
                    mvoe.target = co.bounds.center;
                }
                else
                {
                    mvoe.target = target.transform.position;
                }
                mvoe.receiver = damage;
            }
            gameObject.SetActive(true);
        }
        void Start()
        {
        }
        void Update()
        {

        }
    }
}

