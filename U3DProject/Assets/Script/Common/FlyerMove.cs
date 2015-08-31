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
        transform.rotation = Quaternion.LookRotation(target - transform.position); ;
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
            transform.position += transform.forward * speed;
            if (_duration.Advance(Time.deltaTime))
                _duration.Reset();
        }
    }
}
