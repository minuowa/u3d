using UnityEngine;
using System.Collections;

public class GroundMove : IMission
{
    public int speed = 4;
    public float miniDistance = 0f;


    public Vector3 target;

    private bool _finding = false;
    Duration _duration;
    GameObject _goundFlag;
    Animator animator;
    // Use this for initialization
    void Start()
    {
        base.Begin();

        _finding = true;
        _duration = new Duration();
        _duration.total = 0.5f;

        GameObject preGroundObj = (GameObject)Resources.Load("Prefabs/GameObject/groundFlag", typeof(GameObject));
        _goundFlag = (GameObject)GameObject.Instantiate(preGroundObj);
        _goundFlag.transform.localPosition = target;
        _goundFlag.transform.localScale = preGroundObj.transform.localScale;
        _goundFlag.SetActive(true);

        animator = GetComponentInChildren<Animator>();
        animator.SetInteger(BeingAction.action, BeingAction.Run1);

        CapsuleCollider collider = GetComponentInChildren<CapsuleCollider>();
        if (collider)
            target.y += (collider.height + collider.radius) * 0.5f;

        _completed = false;
    }

    void OnDestroy()
    {
        Destroy(_goundFlag);
    }
    // Update is called once per frame
    void EndFinding()
    {
        _completed = true;
        _finding = false;
        _duration.Reset();
        animator.SetInteger(BeingAction.action, BeingAction.Idle1);
    }
    public override void Update()
    {
        if (_finding)
        {
            Vector3 mypos = transform.position;
            Quaternion myrotation = transform.rotation;
            Vector3 v0 = mypos;
            Vector3 v1 = target;
            v0.y = 0;
            v1.y = 0;

            CharacterController ctrler = GetComponentInParent<CharacterController>();
            if (ctrler)
            {
                Vector3 vt = target;
                vt.y = mypos.y;

                bool rotateOk = false;

                if (Vector3.Distance(vt, mypos) == 0)
                    rotateOk = true;

                Quaternion qfrom=Quaternion.identity, qto=Quaternion.identity;
                if (!rotateOk)
                {
                    qfrom = myrotation;
                    qto = Quaternion.LookRotation(vt - mypos);
                    rotateOk = qfrom == qto || qfrom.eulerAngles == qto.eulerAngles || qto == Quaternion.identity;
                }


                bool posOk = Vector3.Distance(mypos, target) <= miniDistance || v0 == v1;

                if (posOk && rotateOk)
                {
                    EndFinding();
                }
                else
                {
                    Debug.DrawLine(target, mypos, Color.green);
                    if (!rotateOk)
                    {
                        _duration.Advance(Time.deltaTime);
                        transform.rotation = Quaternion.Slerp(qfrom, qto, _duration.progress);
                    }

                    if (!posOk)
                    {
                        Vector3 dir = target - mypos;
                        dir.Normalize();
                        ctrler.SimpleMove(dir * speed);
                    }
                }
            }
        }
    }
}
