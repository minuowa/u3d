using UnityEngine;
using System.Collections;

public class GroundMove : Mission
{
    GameObject mGroundFlag;
    Animator mAnimator;
    NavMeshAgent mPathfinder;
    public GroundMoveParam param;

    public static GameObject garbage;

    public override void Begin()
    {
        if (!NeedMove())
            return;

        base.Begin();

        if (!mGroundFlag)
        {
            GameObject preGroundObj = (GameObject)Resources.Load("Prefabs/GameObject/groundFlag", typeof(GameObject));
            mGroundFlag = (GameObject)GameObject.Instantiate(preGroundObj);
            mGroundFlag.transform.localScale = preGroundObj.transform.localScale;
            mGroundFlag.transform.parent = Garbage.Instance.root.transform;
        }

        mGroundFlag.transform.localPosition = param.target;
        mGroundFlag.SetActive(true);

        if (!mAnimator)
            mAnimator = param.sender.gameObject.GetComponentInChildren<Animator>();
        if (!mPathfinder)
            mPathfinder = param.sender.GetComponent<NavMeshAgent>();
        if (mAnimator)
            mAnimator.SetInteger(BeingAnimation.action, BeingAnimation.Run1);
        if(mPathfinder)
            mPathfinder.SetDestination(param.target);

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
    }
    void EndMove()
    {
        Destroy();
    }

    bool NeedMove()
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

        completed = Vector3.Distance(mypos, target) <= param.miniDistance || Vector3.Distance(v0, v1) <= 0.1f;
        return !completed;
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
            if (!NeedMove())
                EndMove();
        }
    }

    public override void OnParam(IMissionParam param)
    {
        bool start = this.param != null;
        this.param = (GroundMoveParam)param;
        if (start)
            this.Begin();
    }
}
