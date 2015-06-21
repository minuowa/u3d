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
                anim.StopPlayback();
            transform.position -= transform.forward * 0.2f;
            Destroy(this);
        }
    }

    public class DamageReceiver : MonoBehaviour
    {
        public int skillid;
        public GameObject sender;
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
                anim.StopPlayback();
            transform.position -= transform.forward * 0.2f;
            Destroy(this);
        }
    }

    public class DamageObject : MonoBehaviour
    {
        public int skillid;
        public GameObject sender;
        public GameObject target;
        public string start;
        public string end;
        public int missionid;

        public static bool Init(int skillID, GameObject sender, GameObject victim, int missionid)
        {
            Data data = Data.Get(skillID);
            if (data == null)
                return false;
            GameObject prefab = (GameObject)Resources.Load(data.damage.prefab, typeof(GameObject));
            GameObject obj = (GameObject)GameObject.Instantiate(prefab);
            if (!obj)
                return false;
            obj.transform.localRotation = prefab.transform.rotation;
            DamageObject dameobj = obj.AddComponent<DamageObject>();
            dameobj.skillid = skillID;
            dameobj.sender = sender;
            dameobj.target = victim;
            dameobj.missionid = missionid;
            return true;
        }

        void Start()
        {

            transform.position = sender.transform.position;
            transform.rotation = sender.transform.rotation;

            if (target)
            {
                DamageReceiver receiver = target.AddComponent<DamageReceiver>();
                receiver.sender = sender;
                receiver.skillid = skillid;
                receiver.missionid = missionid;
                FlyerMove mvoe = gameObject.AddComponent<FlyerMove>();
                Collider co = target.GetComponent<Collider>();
                if (co)
                {
                    mvoe.target = co.bounds.center;
                }
                else
                {
                    mvoe.target = target.transform.position;
                }
                mvoe.receiver = receiver;
            }

            gameObject.SetActive(true);

        }
        void Update()
        {

        }
    }
}

