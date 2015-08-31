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
    public interface IReceiver
    {
        void OnEnd();
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

    public class DamageReceiver : MonoBehaviour
    {
        public int skillid;
        public Being sender;
        public int missionid;
        public void OnEnd()
        {
            if (sender)
            {
                MissionMgr mgr = sender.GetComponent<MissionMgr>();
                if (mgr)
                    mgr.OnComplate(missionid);
            }
            Animator anim = gameObject.GetComponent<Animator>();
            if (anim != null)
                anim.SetInteger(BeingAnimation.action, BeingAnimation.BeAttack1);
            transform.position -= transform.forward * 0.2f;
            Destroy(this);
        }
    }

    public class DamageObject : MonoBehaviour
    {
        public int skillid;
        public Being sender;
        public Being target;
        public int missionid;

        public void Shot()
        {
            gameObject.SetActive(true);
            transform.position = sender.GetArcherShotPos();
            //transform.rotation = sender.transform.rotation * transform.localRotation;

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
                mvoe.receiver = target.gameObject.GetComponent<DamageReceiver>();
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

