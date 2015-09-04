using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    Duration mDuration;
    public GroundMoveParam param;
    bool mCompleted = false;

    void Start()
    {
        mDuration = new Duration();
        mDuration.total = 0.15f;
    }


    public void Update()
    {
        if (!mCompleted && param != null)
        {
            CapsuleCollider collider = param.sender.gameObject.GetComponentInChildren<CapsuleCollider>();
            Vector3 target = param.target;
            if (collider)
                target.y += (collider.height + collider.radius) * 0.5f;

            Vector3 mypos = param.sender.gameObject.transform.position;
            //Quaternion myrotation = Fun.GetRotation(param.sender.gameObject);
            Quaternion myrotation = param.sender.gameObject.transform.rotation;
            Vector3 v0 = mypos;
            Vector3 v1 = target;
            v0.y = 0;
            v1.y = 0;

            CharacterController ctrler = param.sender.gameObject.GetComponentInParent<CharacterController>();
            if (ctrler)
            {
                Vector3 vt = target;
                vt.y = mypos.y;

                if (Vector3.Distance(vt, mypos) == 0)
                    mCompleted = true;

                Quaternion qfrom=Quaternion.identity, qto=Quaternion.identity;
                if (!mCompleted)
                {
                    qfrom = myrotation;
                    qto = Quaternion.LookRotation(vt - mypos);
                    mCompleted = mCompleted || qfrom == qto;
                    mCompleted = mCompleted || Vector3.Distance(qfrom.eulerAngles, qto.eulerAngles) < 0.0001f;
                    mCompleted = mCompleted || qto == Quaternion.identity;
                }

                if (!mCompleted)
                {
                    Debug.DrawLine(target, mypos, Color.green);
                    mDuration.Advance(Time.deltaTime);
                    Quaternion qdest=Quaternion.Slerp(qfrom, qto, mDuration.progress);
                    //Fun.SetRotation(param.sender.gameObject, qdest);
                    param.sender.gameObject.transform.rotation = Quaternion.Slerp(qfrom, qto, mDuration.progress);
                }
            }
        }
        if (mCompleted || param == null)
            GameObject.Destroy(this);
    }
}
