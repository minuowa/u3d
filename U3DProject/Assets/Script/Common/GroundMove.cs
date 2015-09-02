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

    public static GameObject garbage;

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

        mGroundFlag.transform.localPosition = moveParam.target;
        mGroundFlag.SetActive(moveParam.visible);

        if (!mAnimator)
            mAnimator = moveParam.sender.gameObject.GetComponentInChildren<Animator>();
        if (!mPathfinder)
            mPathfinder = param.sender.GetComponent<NavMeshAgent>();
        if (mAnimator)
            mAnimator.SetInteger(BeingAnimation.action, BeingAnimation.Run1);
        if (mPathfinder)
        {
            mPathfinder.Resume();
            mPathfinder.SetDestination(moveParam.target);
        }

        mCompleted = false;
    }

    public void UpdateTargetPosition()
    {
        if (mGroundFlag && moveParam != null)
            mGroundFlag.transform.localPosition = moveParam.target;
        if (mPathfinder)
            mPathfinder.SetDestination(moveParam.target);
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
        CapsuleCollider collider = moveParam.sender.gameObject.GetComponentInChildren<CapsuleCollider>();
        Vector3 target = moveParam.target;
        if (collider)
            target.y += (collider.height + collider.radius) * 0.5f;

        Vector3 mypos = param.sender.gameObject.transform.position;
        Vector3 v0 = mypos;
        Vector3 v1 = target;
        v0.y = 0;
        v1.y = 0;

        completed = Vector3.Distance(mypos, target) <= moveParam.miniDistance || Vector3.Distance(v0, v1) <= 0.2f;

        if (completed)
        {
            Rotation rot = param.sender.gameObject.GetComponent<Rotation>();
            if (!rot)
                rot = param.sender.gameObject.AddComponent<Rotation>();
            rot.param = moveParam;
        }

        return completed;
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 mypos = param.sender.gameObject.transform.position;
        Gizmos.DrawWireSphere(mypos, moveParam.miniDistance);
    }
    public override void Update()
    {
        if (!mBegin)
            return;
        UpdateTargetPosition();
        if (!mCompleted)
            CheckCompleted();
        else
            this.Destroy();
    }
}
