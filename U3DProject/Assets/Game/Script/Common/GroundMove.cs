using UnityEngine;
using System.Collections;

public class GroundMove : IMission
{

    Duration _duration;
    GameObject _goundFlag;
    Animator _animator;
    public GroundMoveParam param;

    private bool _finding = false;
    public int speed = 4;

    public override void Begin()
    {
        base.Begin();

        _finding = true;
        _duration = new Duration();
        _duration.total = 0.3f;

        GameObject preGroundObj = (GameObject)Resources.Load("Prefabs/GameObject/groundFlag", typeof(GameObject));
        _goundFlag = (GameObject)GameObject.Instantiate(preGroundObj);
        _goundFlag.transform.localPosition = param.target;
        _goundFlag.transform.localScale = preGroundObj.transform.localScale;
        _goundFlag.SetActive(true);

        _animator = param.sender.gameObject.GetComponentInChildren<Animator>();
        _animator.SetInteger(BeingAnimation.action, BeingAnimation.Run1);



        _completed = false;
    }

    public override void Destroy()
    {
        if(_goundFlag)
            GameObject.Destroy(_goundFlag);
    }
    // Update is called once per frame
    void EndFinding()
    {
        _completed = true;
        _finding = false;
        _duration.Reset();
        _animator.SetInteger(BeingAnimation.action, BeingAnimation.Idle1);
        Destroy();
    }
    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 mypos = param.sender.gameObject.transform.position;
        Gizmos.DrawWireSphere(mypos, param.miniDistance);
    }
    public override void Update()
    {
        if (!_begin)
            return;

        if (_finding)
        {
            CapsuleCollider collider = param.sender.gameObject.GetComponentInChildren<CapsuleCollider>();
            Vector3 target = param.target;
            if (collider)
                target.y += (collider.height + collider.radius) * 0.5f;

            Vector3 mypos = param.sender.gameObject.transform.position;
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


                bool posOk = Vector3.Distance(mypos, target) <= param.miniDistance || Vector3.Distance(v0, v1) <= 0.1f;


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
                        param.sender.gameObject.transform.rotation = Quaternion.Slerp(qfrom, qto, _duration.progress);
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
