using UnityEngine;
using System.Collections;
using Skill;

public class FlyerMove : MonoBehaviour
{
    public DamageObject damageobject;
    public float speed = 0.05f;
    public float miniDistance = 0.7f;
    public Vector3 target;
    public int missionID;
    public Being sender;
    Duration mDuration;
    bool mEnd = false;
    void Start()
    {
        mDuration = new Duration();
        mDuration.total = 5f;
        transform.rotation = Quaternion.LookRotation(target - transform.position); ;
    }

    void EndFinding()
    {
        mEnd = true;
        Destroy(gameObject);
    }
    void Update()
    {
        if (mEnd)
            return;
        if (Vector3.Distance(transform.position, target) < miniDistance)
        {
            EndFinding();
        }
        else if (mDuration != null)
        {
            Debug.DrawLine(target, transform.position, Color.green);
            transform.position += transform.forward * speed;
            if (mDuration.Advance(Time.deltaTime))
                mDuration.Reset();
        }
    }
}
