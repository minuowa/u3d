﻿using UnityEngine;
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
    public override bool InitBaseData()
    {
        base.InitBaseData();
        mPathfinder = param.sender.GetComponent<NavMeshAgent>();
        if(mPathfinder)
        {
            mPathfinder.stoppingDistance = moveParam.miniDistance;
            mPathfinder.SetDestination(moveParam.target);
            mPathfinder.updateRotation = true;
            mPathfinder.updatePosition = true;
            mPathfinder.Stop();
        }

        mAnimator = moveParam.sender.gameObject.GetComponentInChildren<Animator>();
        return true;
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

        mPathfinder.Resume();

        Rotation rot = param.sender.gameObject.GetComponent<Rotation>();
        if (rot)
            GameObject.DestroyImmediate(rot);

        UpdateTargetPosition();
    }

    public void UpdateTargetPosition()
    {
        if (mGroundFlag && moveParam != null)
            mGroundFlag.transform.localPosition = moveParam.target;
        mPathfinder.SetDestination(moveParam.target);
    }
    void CorrectRotation()
    {
        Rotation rot = param.sender.gameObject.GetComponent<Rotation>();
        if (!rot)
            rot = param.sender.gameObject.AddComponent<Rotation>();
        rot.param = moveParam;
    }
    void StopAni()
    {
        if(mAnimator)
            mAnimator.SetInteger(BeingAnimation.action, BeingAnimation.Idle1);
        mPathfinder.Stop();
    }
    public override void Discard()
    {
        base.Discard();

        if (mGroundFlag)
            GameObject.Destroy(mGroundFlag);

        CorrectRotation();
        StopAni();
    }

    public override bool CheckCompleted()
    {
        bool comp = mPathfinder.remainingDistance <= moveParam.miniDistance
            || mPathfinder.remainingDistance <= mPathfinder.radius;

        return comp;
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (mPathfinder && mPathfinder.updatePosition)
            Gizmos.DrawWireSphere(mPathfinder.destination, 0.25f);
    }

    public override bool Update()
    {
        if (!base.Update())
            return false;
        UpdateTargetPosition();
        return true;
    }
}
