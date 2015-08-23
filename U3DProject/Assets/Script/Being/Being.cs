﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MissionMgr))]
[RequireComponent(typeof(NameCard))]
[RequireComponent(typeof(StatBeing))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(OnDamage))]
[RequireComponent(typeof(NavMeshAgent))]

[behaviac.TypeMetaInfo("生物","带AI的生物")]
public class Being : behaviac.Agent
{
    protected NameCard mNameCard;
    protected StatBeing mStatBeing;
    protected MissionMgr mMissionMgr;
    protected Animator mAnimator;
    protected NavMeshAgent mPathFinder;

    public float rotateSpeed = 3.0f;
    [HideInInspector]
    public Being mTarget;


    List<Being> mBeings;

    public Being()
    {
        mBeings = new List<Being>();
    }


    void Awake()
    {
    }

    void Update()
    {
        btexec();
    }

    [behaviac.MethodMetaInfo()]
    public behaviac.EBTStatus RandomStart()
    {
        return behaviac.EBTStatus.BT_RUNNING;
    }
    #region AI
    Being Get(int index)
    {
        return mBeings[index];
    }
    public bool IsBeingAround()
    {
        return BeingCountAround() > 0;
    }
    public int BeingCountAround()
    {
        return 0;
    }
    public bool IsEnemy(int idx)
    {
        return GroupManager.IsEnemy(mStatBeing.group, Get(idx).mStatBeing.group);
    }
    public int GetAroundHp(int idx)
    {
        return Get(idx).GetHp();
    }
    public int GetAroundHpPercent(int idx)
    {
        return Get(idx).GetHpPercent();
    }
    public int GetHp()
    {
        return 1;
    }
    public int GetHpPercent()
    {
        return 1;
    }
    public int GetTargetType()
    {
        return 0;
    }
    public string GetTargetName()
    {
        if (mTarget)
            return mTarget.name;
        return string.Empty;
    }
    public int GetTargetNpcID()
    {
        return 0;
    }
    public string GetEnemyName(int idx)
    {
        return Get(idx).name;
    }
    public float GetTargetDistance()
    {
        return Vector3.Distance(mTarget.transform.localPosition, transform.localPosition);
    }
    public float GetEnemyDistance(int idx)
    {
        return Vector3.Distance(Get(idx).gameObject.transform.localPosition, transform.localPosition);
    }
    public int GetState()
    {
        return 0;
    }
    public void SetAction(int id)
    {

    }
    public void Talk(string str)
    {

    }
    public void Move(Vector3 tar)
    {

    }
    public void SetTarget(int idx)
    {
        mTarget = Get(idx);
    }
    public void Attack(int idx)
    {
    }
    #endregion
    public void Do(ActionID action, IParam para)
    {
        para.sender = this;

        switch (action)
        {
            case ActionID.SelectTarget:
                {
                    SelectParam param = (SelectParam)para;
                    if (mTarget != null)
                        mTarget.Unselect();
                    param.receiver.Select();
                    mTarget = param.receiver;
                }
                break;
            case ActionID.MoveTo:
                {
                    GroundMoveParam param = para as GroundMoveParam;
                    GroundMove move = new GroundMove();
                    move.param = param;
                    mMissionMgr.Add(move, true);
                }
                break;
            case ActionID.Skill:
                {
                    SkillParam param = (SkillParam)para;

                    GroundMoveParam moveParam = new GroundMoveParam();
                    moveParam.receiver = param.receiver;
                    moveParam.sender = this;
                    moveParam.miniDistance = param.data.distance;
                    GroundMove move = new GroundMove();
                    move.param = moveParam;
                    mMissionMgr.Add(move, true);

                    Skill.Executor executor = new Skill.Executor();
                    executor.skillid = param.skillID;
                    executor.actor = this;
                    executor.victim = param.receiver;
                    MissionSkill missskill = new MissionSkill();
                    missskill.executor = executor;
                    mMissionMgr.Add(missskill, true);
                }
                break;
        }
    }
    public virtual void Start()
    {
        base.Init();
        MY<AISystem>.Instance.Load(this, MY<AISystem>.Instance.ballAI);

        mNameCard = gameObject.GetComponent<NameCard>();
        mStatBeing = gameObject.GetComponent<StatBeing>();
        mAnimator = GetComponentInChildren<Animator>();
        if (mAnimator == null)
            mAnimator = gameObject.GetComponent<Animator>();
        mMissionMgr = gameObject.GetComponent<MissionMgr>();
        mPathFinder = gameObject.GetComponent<NavMeshAgent>();
    }

    public T GetNewComponent<T>() where T : Component
    {
        T com = gameObject.GetComponent<T>();
        if (com != null)
            GameObject.DestroyImmediate(com);
        com = gameObject.AddComponent<T>();
        return com;
    }
    public T TryGetComponent<T>() where T : Component
    {
        T com = gameObject.GetComponent<T>();
        if (com == null)
            com = gameObject.AddComponent<T>();
        return com;
    }
    public void Unselect()
    {
        var select = GetComponent<SelectFlag>();
        if (select != null)
            GameObject.Destroy(select);
    }
    public void Select()
    {
        GetNewComponent<SelectFlag>();
    }
}