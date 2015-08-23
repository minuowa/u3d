using UnityEngine;
using System.Collections;
using Skill;

public class FlyerMove : MonoBehaviour
{

    public float speed = 0.1f;
    public float miniDistance = 0.7f;
    public Vector3 target;
    Duration _duration;
    public DamageReceiver receiver;
    bool _end = false;
    void Start()
    {
        _duration = new Duration();
        _duration.total = 5f;
    }

    void EndFinding()
    {
        if (receiver)
            receiver.OnEnd();
        _end = true;
        Destroy(gameObject);
    }
    void Update()
    {
        if (_end)
            return;
        if (Vector3.Distance(transform.position, target) < miniDistance)
        {
            EndFinding();
        }
        else
        {
            Debug.DrawLine(target, transform.position, Color.green);

            {
                Vector3 vt = target;
                vt.y = transform.position.y;

                Quaternion qfrom = transform.rotation;
                Quaternion qto = Quaternion.LookRotation(vt - transform.position);
                transform.rotation = Quaternion.Slerp(qfrom, qto, _duration.progress);
                Vector3 dir = target - transform.position;
                dir.Normalize();
                transform.position += dir * speed;
            }
            if (_duration.Advance(Time.deltaTime))
                _duration.Reset();
        }
    }
}
