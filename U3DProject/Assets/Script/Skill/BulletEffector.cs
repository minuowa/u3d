using UnityEngine;
using System.Collections;

public class BulletEffector : Effector
{
    public override void OnBegin()
    {
        base.OnBegin();

        transform.position = sender.GetArcherShotPos();

        FlyerMove flayer = gameObject.AddComponent<FlyerMove>();
        Collider co = target.GetComponent<Collider>();
        flayer.damageobject = damageobj;
        flayer.target = target.transform.position;
        flayer.missionID = this.missionID;
        flayer.sender = sender;
        if (co)
        {
            flayer.target = co.bounds.center;
        }
        else
        {
            flayer.target = target.transform.position;
        }
    }

    public override void OnEnd()
    {
    }
}
