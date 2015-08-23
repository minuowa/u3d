using UnityEngine;
using System.Collections;

public class GroundMove : IMission
{

    Duration mDuration;
    GameObject mGroundFlag;
    Animator mAnimator;
    NavMeshAgent mPathfinder;
    public GroundMoveParam param;

    public int speed = 4;

    bool mMoving = false;
    bool mRotating = false;

    public override void Begin()
    {
        base.Begin();

        mDuration = new Duration();
        mDuration.total = 0.3f;

        GameObject preGroundObj = (GameObject)Resources.Load("Prefabs/GameObject/groundFlag", typeof(GameObject));
        mGroundFlag = (GameObject)GameObject.Instantiate(preGroundObj);
        mGroundFlag.transform.localPosition = param.target;
        mGroundFlag.transform.localScale = preGroundObj.transform.localScale;
        mGroundFlag.SetActive(true);

        mAnimator = param.sender.gameObject.GetComponentInChildren<Animator>();
        mAnimator.SetInteger(BeingAnimation.action, BeingAnimation.Run1);

        mPathfinder = param.sender.GetComponent<NavMeshAgent>();
        if(mPathfinder)
            mPathfinder.SetDestination(param.target);

        mCompleted = false;
        mMoving = true;
        mRotating = true;
    }

    public override void Destroy()
    {
        if(mGroundFlag)
            GameObject.Destroy(mGroundFlag);
    }
    // Update is called once per frame
    void EndRotate()
    {
        mRotating = false;
        Check();
    }
    void EndMove()
    {
        mMoving = false;
        if (mPathfinder)
            mPathfinder.Stop();
        Check();
    }
    void Check()
    {
        mCompleted = !mRotating && !mMoving;
        if (mCompleted && mBegin)
        {
            mDuration.Reset();
            mAnimator.SetInteger(BeingAnimation.action, BeingAnimation.Idle1);
            Destroy();
        }
    }
    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 mypos = param.sender.gameObject.transform.position;
        Gizmos.DrawWireSphere(mypos, param.miniDistance);
    }
    public override void Update()
    {
        if (!mBegin)
            return;

        if (!mCompleted)
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

                if (Vector3.Distance(vt, mypos) == 0)
                    mRotating = false;

                Quaternion qfrom=Quaternion.identity, qto=Quaternion.identity;
                if (mRotating)
                {
                    qfrom = myrotation;
                    qto = Quaternion.LookRotation(vt - mypos);
                    mRotating = mRotating && qfrom != qto;
                    mRotating = mRotating && Vector3.Distance(qfrom.eulerAngles, qto.eulerAngles) > 0.0001f;
                    mRotating = mRotating && qto != Quaternion.identity;
                }

                mMoving = !(Vector3.Distance(mypos, target) <= param.miniDistance || Vector3.Distance(v0, v1) <= 0.1f);

                if (!mMoving)
                {
                    EndMove();
                }
                if (!mRotating)
                {
                    EndRotate();
                }
                else
                {
                    Debug.DrawLine(target, mypos, Color.green);
                    mDuration.Advance(Time.deltaTime);
                    param.sender.gameObject.transform.rotation = Quaternion.Slerp(qfrom, qto, mDuration.progress);
                }
            }
        }
    }
}
