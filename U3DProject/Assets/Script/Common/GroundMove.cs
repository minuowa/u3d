using UnityEngine;
using System.Collections;

public class GroundMove : Mission
{
    GameObject mGroundFlag;
    Animator mAnimator;
    NavMeshAgent mPathfinder;

    protected GroundMoveParam mMoveParam
    {
        get
        {
            return (GroundMoveParam)mParam;
        }
    }

    public static GameObject garbage;

    public override void Restart()
    {
        base.Restart();

        if (!mGroundFlag)
        {
            GameObject preGroundObj = (GameObject)Resources.Load("Prefabs/GameObject/groundFlag", typeof(GameObject));
            mGroundFlag = (GameObject)GameObject.Instantiate(preGroundObj);
            mGroundFlag.transform.localScale = preGroundObj.transform.localScale;
            mGroundFlag.transform.parent = Garbage.Instance.root.transform;
        }

        mGroundFlag.transform.localPosition = mMoveParam.target;
        mGroundFlag.SetActive(true);

        if (!mAnimator)
            mAnimator = mMoveParam.sender.gameObject.GetComponentInChildren<Animator>();
        if (!mPathfinder)
            mPathfinder = param.sender.GetComponent<NavMeshAgent>();
        if (mAnimator)
            mAnimator.SetInteger(BeingAnimation.action, BeingAnimation.Run1);
        if(mPathfinder)
            mPathfinder.SetDestination(mMoveParam.target);

        mCompleted = false;
    }

    public override void Destroy()
    {
        if (mGroundFlag)
            GameObject.Destroy(mGroundFlag);

        if (mAnimator)
            mAnimator.SetInteger(BeingAnimation.action, BeingAnimation.Idle1);
        if (mPathfinder)
            mPathfinder.Stop();

        mBegin = false;
    }
    void EndMove()
    {
        Destroy();
    }

    public override bool CheckCompleted()
    {


        CapsuleCollider collider = mMoveParam.sender.gameObject.GetComponentInChildren<CapsuleCollider>();
        Vector3 target = mMoveParam.target;
        if (collider)
            target.y += (collider.height + collider.radius) * 0.5f;

        Vector3 mypos = param.sender.gameObject.transform.position;
        Quaternion myrotation = param.sender.gameObject.transform.rotation;
        Vector3 v0 = mypos;
        Vector3 v1 = target;
        v0.y = 0;
        v1.y = 0;

        completed = Vector3.Distance(mypos, target) <= mMoveParam.miniDistance || Vector3.Distance(v0, v1) <= 0.1f;

        if (completed)
        {
            Rotation rot = param.sender.gameObject.GetComponent<Rotation>();
            if (!rot)
                rot = param.sender.gameObject.AddComponent<Rotation>();
            rot.param = mMoveParam;
        }

        return completed;
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 mypos = param.sender.gameObject.transform.position;
        Gizmos.DrawWireSphere(mypos, mMoveParam.miniDistance);
    }
    public override void Update()
    {
        if (!mBegin)
            return;

        if (!mCompleted)
            CheckCompleted();
        else
            this.Destroy();
    }
}
