using UnityEngine;
using System.Collections;

public class GroundMove : Mission
{
    GameObject mGroundFlag;
    Animator mAnimator;
    NavMeshAgent mPathfinder;
    protected GroundMoveParam moveParam
    {
        get
        {
            return (GroundMoveParam)mParam;
        }
    }

    public override void Restart()
    {
        base.Restart();

        if (!mGroundFlag)
        {
            GameObject preGroundObj = (GameObject)Resources.Load("Prefabs/sceneObjects/groundFlag", typeof(GameObject));
            mGroundFlag = (GameObject)GameObject.Instantiate(preGroundObj);
            mGroundFlag.transform.localScale = preGroundObj.transform.localScale;
            mGroundFlag.transform.parent = Garbage.Instance.root.transform;
        }
        if (mGroundFlag)
        {
            mGroundFlag.SetActive(moveParam.visible);
            mGroundFlag.transform.localPosition = moveParam.target;
        }

        if (mAnimator)
            mAnimator.SetInteger(BeingAnimation.action, BeingAnimation.Run1);

        Rotation rot = param.sender.gameObject.GetComponent<Rotation>();
        if (rot)
            GameObject.Destroy(rot);

        UpdateTargetPosition();

        mCompleted = false;
    }

    public void UpdateTargetPosition()
    {
        if (mGroundFlag && moveParam != null)
            mGroundFlag.transform.localPosition = moveParam.target;
        if (!mPathfinder)
            mPathfinder = param.sender.GetComponent<NavMeshAgent>();

        mPathfinder.SetDestination(moveParam.target);
        mPathfinder.destination = moveParam.target;
        mPathfinder.Resume();
        mPathfinder.updateRotation = true;
        mPathfinder.updatePosition = true;

        if (!mAnimator)
            mAnimator = moveParam.sender.gameObject.GetComponentInChildren<Animator>();
    }
    public void UpdateRotation()
    {
        Rotation rot = param.sender.gameObject.GetComponent<Rotation>();
        if (!rot)
            rot = param.sender.gameObject.AddComponent<Rotation>();
        rot.param = moveParam;
    }
    public override void Discard()
    {
        if (mGroundFlag)
            GameObject.Destroy(mGroundFlag);

        UpdateRotation();

        mAnimator.SetInteger(BeingAnimation.action, BeingAnimation.Idle1);
        mPathfinder.Stop();
    }

    public override bool CheckCompleted()
    {
        UpdateTargetPosition();

        CapsuleCollider collider = moveParam.sender.gameObject.GetComponentInChildren<CapsuleCollider>();
        Vector3 target = moveParam.target;
        if (collider)
            target.y += (collider.height + collider.radius) * 0.5f;

        Vector3 mypos = param.sender.gameObject.transform.position;
        Vector3 v0 = mypos;
        Vector3 v1 = target;
        v0.y = 0;
        v1.y = 0;

        completed = Vector3.Distance(mypos, target) <= moveParam.miniDistance || Vector3.Distance(v0, v1) <= mPathfinder.radius;
        return completed;
        //bool comp = mPathfinder.remainingDistance <= moveParam.miniDistance
        //    || mPathfinder.remainingDistance <= mPathfinder.radius;

        //return comp;
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 mypos = param.sender.gameObject.transform.position;
        Gizmos.DrawWireSphere(mypos, moveParam.miniDistance);
    }
}
