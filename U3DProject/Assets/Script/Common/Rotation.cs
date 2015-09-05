using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    Duration mDuration;
    public GroundMoveParam param;
    Quaternion from = Quaternion.identity;
    Quaternion to = Quaternion.identity;
    bool mCompleted = false;

    void Start()
    {
        mDuration = new Duration();
        mDuration.total = 0.15f;
        from = param.sender.gameObject.transform.rotation;
    }


    public void Update()
    {
        if (!mCompleted && param != null)
        {
            Vector3 target = param.target;
            Vector3 mypos = param.sender.gameObject.transform.position;
            Vector3 v0 = mypos;
            Vector3 v1 = target;
            v0.y = 0;
            v1.y = 0;

            if (Vector3.Distance(target, mypos) == 0)
                mCompleted = true;

            if (!mCompleted)
            {
                to = Quaternion.LookRotation(v1 - v0);
                mCompleted = mCompleted || from == to;
                mCompleted = mCompleted || Vector3.Distance(from.eulerAngles, to.eulerAngles) < 0.0001f;
                mCompleted = mCompleted || to == Quaternion.identity;
            }

            if (!mCompleted)
            {
                Debug.DrawLine(target, mypos, Color.green);
                mDuration.Advance(Time.deltaTime);
                Quaternion qdest = Quaternion.Slerp(from, to, mDuration.progress);
                param.sender.gameObject.transform.rotation = Quaternion.Slerp(from, to, mDuration.progress);
            }
        }
        if (mCompleted || param == null)
            GameObject.Destroy(this);
    }
}
